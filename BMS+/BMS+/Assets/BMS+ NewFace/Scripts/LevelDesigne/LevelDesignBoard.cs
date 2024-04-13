using GameData;
using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        public Action<int, VectorInt> OnColorAdded;
        public Action<int, VectorInt> OnColorRemoved;

        public static LevelDesignBoard _instance;
        public LevelConfigData level { get; set; }
        public List<EditorShapeManager> shapeManagerList;
        public List<EditorShapeManager> referenceShapeManagerList;
        public List<EditorPinPoint> pinPointList;
        public List<EditorPin> pinList;
        public List<GameData.EventData> eventList;

        public List<bool> pinsChecker;
        public GuideCanvasName selectedGuideCanvasName;
        // window properties
        public bool playMode = false;
        public LevelDesignPhase phase = LevelDesignPhase.Phase1;
        public bool isBoardgenerated=false;

        private string directory;
        public string levelName="level";
        public Vector2 gameBoardSize = new Vector2(3, 3);
        private Vector3 InitialPinPlaceHolderPosition=new Vector3(1.64f,1.8f,0);
        private Vector3 InitialGuideCanvasPosition = new Vector3(-1.4f,-1.11f,0);

        public GameObject referenceBoard;
        public GameObject guideCanvas;
        public GameObject pinPlaceholder;

        void Start()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
                DestroyImmediate(_instance.gameObject);
            shapeManagerList = new List<EditorShapeManager>();
            referenceShapeManagerList= new List<EditorShapeManager>();
            pinPointList = new List<EditorPinPoint>();
            pinsChecker = new List<bool>();
            for (int i = 0;i< EditorDataManager._instance.GetData<PinConfig>().pins.Count;i++)
            {
                pinsChecker.Add(false);
            }
        }
        public void CreateNewLevel()
        {
            level = new LevelConfigData();
            string path = EditorUtility.SaveFilePanelInProject(
            "Save New Asset",
            "NewAsset",
            "asset",
            "enter level name",
            "assets/BMS+ NewFace/Configs/Levels"
        );
            AssetDatabase.CreateAsset(level, path);
        }
        public void LoadLevel() => level = (LevelConfigData)EditorGUILayout.ObjectField("My Asset", level, typeof(LevelConfigData), false);

        public void InstantiateLevelWindowObjects()
        {
            CreateGuideCanvas();
            InstantiateSelectedPins();
        }
        private void CreateGuideCanvas()
        {
            GameObject guideCanvasPrefab = EditorDataManager._instance.GetData<ConfigData.GuideCanvasConfigData>((int)selectedGuideCanvasName).prefab;
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
                    ConfigData.PinConfigData data = EditorDataManager._instance.GetData<ConfigData.PinConfigData>(i);
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
        #region DefaultSaves
        public void SaveToConfig(GameObject pinPlaceHolder, List<EditorPin> pins)
        {
            boardData<PinData> data = new boardData<PinData>();
            data.boardLocation = pinPlaceHolder.transform.localPosition;
            data.shapes = pins.Select(pinData => pinData.pinData).ToList();
            level.pins = data;
        }

        public void SaveToConfig(Transform referenceGameBoard, List<EditorShapeManager> referenceShapes)
        {
            boardData<ShapeData> data = new boardData<ShapeData>();
            data.boardLocation = referenceGameBoard.localPosition;
            data.boardScale = referenceGameBoard.localScale;
            data.shapes = new List<ShapeData>(referenceShapes.Select(shape => shape.shapeData).ToList());
            level.ReferenceGameBoard = data;

        }
        public void SaveToConfig(List<EditorPinPoint> gameBoardPinPoints)
        {
            level.gameBoardPinPoints = gameBoardPinPoints.Select(pinPoint => pinPoint.pinPointData).ToList();
        }
        public void SaveToConfig(GameObject guidCanvas)
        {
            GuideCanvasData data = new GuideCanvasData();
            data.position = guidCanvas.transform.position;
            data.name = selectedGuideCanvasName;
            level.GuideCanvas = data;
        }
        public void SetForSave()
        {
            EditorUtility.SetDirty(level);
        }
        #endregion
        #region SaveNormalModeRegion
        public void SaveToConfig(RectTransform gameBoardCanvas,List<EditorShapeManager> boardShapes)
        {
            boardData<ShapeData> data = new boardData<ShapeData>();
            data.boardLocation = gameBoardCanvas.transform.localPosition;
            data.boardScale = gameBoardCanvas.localScale;
            data.shapes = boardShapes.Select(shape => {
                shape.shapeData.ColorData = VectorInt.White;
                shape.shapeData.shapeAddedNumber = 0;
                return shape.shapeData;
                } ).ToList();
            level.GameBoardCanvas = data;
        }
        #endregion
        #region SaveBlitzMode
        public void SaveEventsToConfig()
        {
            eventList = new List<EventData>();
            foreach (EditorShapeManager refShape in referenceShapeManagerList )
            {
                if(refShape.shapeEvent != null)
                    eventList.Add(refShape.shapeEvent);

            }
            level.events = eventList;
        }

        #endregion
        #region SaveHalfWayThereMode
        public void SaveHalfWayThereToConfig(RectTransform gameBoardCanvas, List<EditorShapeManager> boardShapes)
        {
            boardData<ShapeData> data = new boardData<ShapeData>();
            data.boardLocation = gameBoardCanvas.transform.localPosition;
            data.boardScale = gameBoardCanvas.localScale;
            data.shapes = new List<ShapeData>();
            foreach(var shape in boardShapes.Select(shape => shape.shapeData).ToList())
            {
                data.shapes.Add((ShapeData)shape.Clone());
            }
            level.GameBoardCanvas = data;
        }
        #endregion
    }

}