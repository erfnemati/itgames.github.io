using GameData;
using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
namespace LevelDesign
{
    [ExecuteInEditMode]
    public class LevelDesignBoard : MonoBehaviour
    {
        public Action<int,Color> OnColorAdded;
        public Action<int,Color> OnColorRemoved;

        public static LevelDesignBoard _instance;
        public LevelConfig level { get; set; }
        public List<EditorShapeManager> shapeManagerList;
        public List<EditorShapeManager> referenceShapeManagerList;
        public List<EditorPinPoint> pinPointList;
        public List<EditorPin> pinList;

        public GameObject GuideReference;
        public List<bool> pinsChecker;
        public GuideCanvasName selectedGuideCanvasName;
        // window properties
        public bool playMode = false;
        public LevelDesignPhase phase = LevelDesignPhase.Phase1;
        public bool isBoardgenerated=false;

        private string directory;
        public Vector2 gameBoardSize = new Vector2(3, 3);
        private Vector3 InitialPinPlaceHolderPosition=new Vector3(1.64f,1.8f,0);
        private Vector3 InitialGuideCanvasPosition = new Vector3(-1.4f,-1.11f,0);

        public GameObject referenceBoard;
        public GameObject guideCanvas;
        public GameObject pinPlaceholder;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
                DestroyImmediate(_instance.gameObject);
            level = LevelConfig.CreateInstance<LevelConfig>();
            shapeManagerList = new List<EditorShapeManager>();
            pinPointList= new List<EditorPinPoint>();
            pinsChecker = new List<bool>();
            for (int i = 0;i< EditorDataManager._instance.GetData<PinConfig>().pins.Count;i++)
            {
                pinsChecker.Add(false);
            }
        }
        public void CreateNewLevel()
        {
            level=new LevelConfig();
            directory = EditorUtility.OpenFolderPanel("Select Directory", "Assets/BMS+ NewFace/Configs", "");
            string path = "Assets" + directory.Split("Assets")[1] + "/level.asset";
            AssetDatabase.CreateAsset(level, path);
        }
        public void LoadLevel() => level = (LevelConfig)EditorGUILayout.ObjectField("My Asset", level, typeof(LevelConfig), false);

        public void InstantiateLevelWindowObjects()
        {
            CreateGuideCanvas();
            InstantiateSelectedPins();
        }
        private void CreateGuideCanvas()
        {
            GameObject guideCanvasPrefab = EditorDataManager._instance.GetData<GuideCanvasDataa>((int)selectedGuideCanvasName).prefab;
            guideCanvas = Instantiate(guideCanvasPrefab, InitialGuideCanvasPosition, Quaternion.identity);
            guideCanvas.name = "GuideCanvas";
        }
        private void InstantiateSelectedPins()
        {
            pinPlaceholder = new GameObject();
            pinPlaceholder.transform.position = InitialPinPlaceHolderPosition;
            pinPlaceholder.name = "PinPlaceHolder";
            SetGridFeatures(pinPlaceholder.AddComponent<GridLayoutGroup>());
            for (int i=0; i<pinsChecker.Count;i++)
            {
                if (pinsChecker[i])
                {
                    GameObject pinPrefab = EditorDataManager._instance.GetData<PrefabConfig>().PinPrefab;
                    GameObject pinObject = Instantiate(pinPrefab, pinPlaceholder.transform);
                    EditorPin pin = pinObject.AddComponent<EditorPin>();
                    PinColorData data = EditorDataManager._instance.GetData<PinColorData>(i);
                    pin.SetPinColorData(data);
                    Debug.Log(data.color);
                    Debug.Log(pin);
                    pin.SetPinColor(data.color);
                    pinList.Add(pin);                   
                    pinObject.GetComponentInChildren<UnityEngine.UI.Image>().sprite = data.sprite;
                }

            }
        }
        public void SetGridFeatures(GridLayoutGroup grid)
        {
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 1;
            grid.childAlignment = TextAnchor.MiddleCenter;
            grid.cellSize = new Vector2(0.7f, 0.7f);
        }
        #region SaveRegion
        public void SaveToConfig(RectTransform gameBoardCanvas,List<EditorShapeManager> boardShapes)
        {
            level.GameBoardCanvas = gameBoardCanvas;
            level.shapes = boardShapes.Select(shape => shape.shapeData).ToList();
        }
        public void SaveToConfig( GameObject pinPlaceHolder, List<EditorPin> pins)
        {
            level.PinPlaceholder = pinPlaceHolder.transform.position;
            level.pins=pins.Select(pinData=>pinData.pinData).ToList();
        }
        public void SaveToConfig(Transform referenceGameBoard, List<EditorShapeManager> referenceShapes )
        {
            level.refrenceShapes = referenceShapes.Select(shape => shape.shapeData).ToList();
            level.ReferenceGameBoard=referenceGameBoard;

        }
        public void SaveToConfig(List<EditorPinPoint> gameBoardPinPoints)
        {
            level.gameBoardPinPoints= gameBoardPinPoints.Select(pinPoint => pinPoint.pinPointData).ToList();
        }
        public void SaveToConfig(GameObject guidCanvas)
        {
            GuideCanvasData data = new GuideCanvasData();
            data.position = guidCanvas.transform.position;
            data.GuidCanvasPrefab= guidCanvas;
            level.GuideCanvas = data;
        }
        public void SaveToConfig(List<EventData> events)
        {
            level.events= events;
        }
        #endregion

    }

}