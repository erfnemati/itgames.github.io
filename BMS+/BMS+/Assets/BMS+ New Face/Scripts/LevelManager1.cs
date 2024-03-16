using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// changes to do
/// 1- pin button creator;
/// 2- levelObject witch assines by levelPicker ;
/// 3- routine for generating game board;
/// </summary>
public class LevelManager1 : MonoBehaviour
{
    public static LevelManager1 _instance;

    private GameObject gameBoard;
    private GameObject referenceBoard;

    private List<ShapeManager> m_shapes = new List<ShapeManager>();
    private List<ReferenceShapeManager> m_referenceShapes= new List<ReferenceShapeManager>();
    [SerializeField] List<Button> m_towerButtons = new List<Button>();
    [SerializeField] AudioClip m_victorySound;

    public delegate void EndLevelAction();
    public static event EndLevelAction OnLevelVictory;
    public static event EndLevelAction OnLevelDefeat;
    public static event EndLevelAction OnLevelRetreat;
    public static event EndLevelAction OnGameWin;

    public LevelConfig levelConfig; //{ get; private set; }
    private void OnEnable()
    {
        LevelTimer.OnTimeOver += LostLevel;
        //LevelTimer.OnTimeOver += DeactivateTowerButtons;
        OnLevelDefeat += DeactivateTowerButtons;
        OnLevelVictory += PlayVictorySound;
        OnLevelVictory += DeactivateTowerButtons;
        OnLevelRetreat += DeactivateTowerButtons;
        //Debug.Log("Level manager enabling");

    }

    private void OnDisable()
    {
        LevelTimer.OnTimeOver -= LostLevel;
        //LevelTimer.OnTimeOver -= DeactivateTowerButtons;
        OnLevelDefeat -= DeactivateTowerButtons;
        OnLevelVictory -= PlayVictorySound;
        OnLevelVictory -= DeactivateTowerButtons;
        OnLevelRetreat -= DeactivateTowerButtons;
        //Debug.Log("Level manager disabling");
    }

    private void Awake()
    {
        if(_instance != null && _instance!= this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        //levelConfig= Instantiate(levelConfig); // get from levelpicker
    }
    private void Start()
    {
        InstantiateGameBoard();
        InstantiateReferenceBoard();
        InstantiateGuideCanvas();
        InstantiatePins();
        InstantiateGameBoardPinPoints();
    }

    private void Update() //[con]: whats this for
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            WinLevel();
        }
    }

    private void InstantiateGameBoard()
    {
        gameBoard=AddCanvasComponants(levelConfig.GameBoardCanvas);
        gameBoard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        gameBoard.name = gameBoard.ToString() ;
        GameObject shapePrefab = DataManager._instance.GetPrefabs().ShapePrefab;
        foreach( ShapeData shapeData in levelConfig.shapes)
        {
            ShapeManager shapeManager = AddConfigShapeToBoard<ShapeManager>(shapePrefab, shapeData,gameBoard.transform);
            m_shapes.Add(shapeManager);
        }
    }
    private void InstantiateReferenceBoard()
    {
        referenceBoard = AddCanvasComponants(levelConfig.ReferenceGameBoard);
        referenceBoard.name = referenceBoard.ToString();
        referenceBoard.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        GameObject shapePrefab = DataManager._instance.GetPrefabs().ShapePrefab;
        foreach (ShapeData shapeData in levelConfig.shapes)
        {
            ReferenceShapeManager shapeManager = AddConfigShapeToBoard<ReferenceShapeManager>(shapePrefab, shapeData,referenceBoard.transform);
            m_referenceShapes.Add(shapeManager);
        }
    }
    private T AddConfigShapeToBoard<T>(GameObject shapePrefab, ShapeData shapeData, Transform parent) where T : ShapeManager
    {
        GameObject shape = Instantiate(shapePrefab, parent);
        shape.transform.localPosition=shapeData.Position;
        T shapeManager = shape.AddComponent<T>();
        shapeManager.InitializeShape(shapeData.ColorData, shapeData.shapeAddedNumber,shapeData.shapeId);

        return shapeManager;
    }
    private GameObject AddCanvasComponants(Vector3 position)
    {
        GameObject Board = Instantiate(new GameObject(), position, Quaternion.identity);
        Board.AddComponent<Canvas>();
        Board.AddComponent<CanvasScaler>();
        Board.AddComponent<GraphicRaycaster>();
        return Board;
    }
    private void InstantiateGuideCanvas()
    {
        Instantiate(levelConfig.GuideCanvas.GuidCanvasPrefab,levelConfig.GuideCanvas.position,Quaternion.identity);
    }
    private void InstantiatePins()
    {
        GameObject pinPlaceholder = Instantiate(new GameObject(),levelConfig.PinPlaceholder,Quaternion.identity);
        pinPlaceholder.name = pinPlaceholder.ToString();
        SetGridFeatures(pinPlaceholder.AddComponent<GridLayoutGroup>());
        foreach (PinData pinData in levelConfig.pins)
        {
            InstantiatePin(pinData, pinPlaceholder.transform);

        }
    }
    public void SetGridFeatures(GridLayoutGroup grid)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 1;
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.cellSize = new Vector2(0.7f, 0.7f);
    }
    private void InstantiateGameBoardPinPoints()
    {
        GameObject pinPointPrefab = DataManager._instance.GetPrefabs().PinPointPrefab;
        foreach(PinPointData pinpointData in levelConfig.gameBoardPinPoints)
        {
            GameObject pinpointObject = Instantiate(pinPointPrefab, pinpointData.position, Quaternion.identity, gameBoard.transform);
            Pinpoint1 pinPointScript =pinpointObject.AddComponent<Pinpoint1>();
            List<ShapeManager> ShapesCorrespandingToConfigIDs = m_shapes.Where(shape => pinpointData.neighborShapes.Contains(shape.shapeId)).ToList();
            pinPointScript.InitializePinPoint(ShapesCorrespandingToConfigIDs, pinpointData.pinPointColor);
        }
    }
    private void InstantiatePin(PinData pinData, Transform parent)
    {
        GameObject pinPrefab = DataManager._instance.GetPrefabs().PinPrefab;
        GameObject pinObject = Instantiate(pinPrefab, parent);
        Pin1 pinScript = pinObject.GetComponent<Pin1>();
        pinScript.GetComponentInChildren<Button>().onClick.AddListener(() => Player1._instance.PickPin(pinScript));
        pinScript.InitializePin(pinData.pincolor, pinData.pinCapacity);
    }
    public void CompareBoards()
    {

        if (m_shapes.Count != m_referenceShapes.Count)
        {
            Debug.Log("Something wrong with gameboard hexagons and reference hexagons");
            return;
        }
        
        for(int i = 0; i < m_referenceShapes.Count; i++)
        {
            if (m_shapes[i].GetShapeColor() == m_referenceShapes[i].GetShapeColor())
            {
                if (m_shapes[i].GetShapeNumber() == m_referenceShapes[i].GetShapeNumber())
                {
                    continue;
                }
                else
                {
                    Debug.Log("Not there yet");
                    return;
                }
            }
            else
            {
                Debug.Log("Not there yet");
                return;
            }
        }

        WinLevel();
    }

    private void DeactivateTowerButtons()
    {
        foreach(Button tempButton in m_towerButtons)
        {
            tempButton.enabled = false;
        }
    }

    private void WinLevel()
    {
        
        Debug.Log("You have won");
        OnLevelVictory();
        if (BmsPlusSceneManager._instance.IsLastLvl())
        {
            OnGameWin();
        }

    }

    private void LostLevel()
    {
        Debug.Log("You have lost");
        if (PlayerLifeManager._instance != null)
        {
            PlayerLifeManager._instance.DecrementNumOfLives();
        }
        OnLevelDefeat();
    }

    public void RetreatLevel()
    {
        LostLevel();
    }

    public void RetreatMainMenu()
    {
        int numOfCurrentLives = PlayerLifeManager._instance.GetCurrentNumberOfLives();
        for (int i = 0; i < numOfCurrentLives;i++)
        {
            Debug.Log("Oops i am here");
            if(PersistentDataManager._instance != null)
            {
                PersistentDataManager._instance.IncrementNumOfConsumedLives();
            }
            PlayerLifeManager._instance.DecrementNumOfLives();
        }
        OnLevelRetreat();
    }

    private void PlayVictorySound()
    {
        Debug.Log("Play victory sound");
        SoundManager._instance.StopAllSoundEffects();
        SoundManager._instance.PlaySoundEffect(m_victorySound);
    }
}
