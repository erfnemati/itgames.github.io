using System.Collections.Generic;
using System.Linq;
using GameData;
using UnityEngine;
using UnityEngine.UI;
using GameEnums;
using Unity.VisualScripting;
/// <summary>
/// changes to do
/// 1- pin button creator;
/// 2- levelObject witch assines by levelPicker ;
/// 3- routine for generating game board;
/// </summary>
public class LevelInitializer
{

    private GameObject gameBoard;
    private GameObject referenceBoard;
    private GameObject gameBoardCanvas;

    private List<ShapeManager> m_shapes = new List<ShapeManager>();
    private List<ReferenceShapeManager> m_referenceShapes = new List<ReferenceShapeManager>();
    private List<Pinpoint> m_pinPoints = new List<Pinpoint>();

    private DataManager m_dataManager;
    public LevelConfigData levelConfig { get; set; }
    public LevelInitializer(LevelConfigData levelConfig)
    {
        this.levelConfig = levelConfig;
        m_dataManager = ServiceLocator._instance.Get<DataManager>();
    }
    public  void InitalizeLevelFromConfig()
    {
        InstantiateGameBoard();
        InstantiateReferenceBoard();
        InstantiateGuideCanvas();
        InstantiatePins();
        InstantiateGameBoardPinPoints();

        if (levelConfig.gameMode ==  GameMode.Normal)
        {
            SetNormalInitials();
        }
        else if(levelConfig.gameMode == GameMode.HalfWayThere)
        {
            SetHalfWayThereInitials();
        }
        else if(levelConfig.gameMode == GameMode.Blitz) 
        {
            SetNormalInitials();

            //ServiceLocator._instance.Get<LevelTimer>().SetBlitzModeInitials(levelConfig.events);
        }
    }

    private void InstantiateGameBoard()
    {
        gameBoard = new GameObject("GameBoard");
        gameBoardCanvas=AddCanvasComponants(gameBoard.transform);
        gameBoard.transform.position = levelConfig.GameBoardCanvas.boardLocation;
        gameBoard.transform.localScale = levelConfig.GameBoardCanvas.boardScale;
        gameBoard.name = gameBoard.ToString();
        GameObject shapePrefab = m_dataManager.GetData<PrefabConfig>().ShapePrefab;
        foreach (ShapeData shapeData in levelConfig.GameBoardCanvas.shapes)
        {
            ShapeManager shapeManager = AddConfigShapeToBoard<ShapeManager>(shapePrefab, shapeData, gameBoard.transform);
            m_shapes.Add(shapeManager);
        }
    }
    private void InstantiateReferenceBoard()
    {
        referenceBoard = AddReferenceCanvasComponants(levelConfig.ReferenceGameBoard.boardLocation, levelConfig.ReferenceGameBoard.boardScale );
        referenceBoard.name = referenceBoard.ToString();
        GameObject shapePrefab = m_dataManager.GetData<PrefabConfig>().ShapePrefab;
        foreach (ShapeData shapeData in levelConfig.ReferenceGameBoard.shapes)
        {
            ReferenceShapeManager shapeManager = AddConfigShapeToBoard<ReferenceShapeManager>(shapePrefab, shapeData, referenceBoard.transform);
            m_referenceShapes.Add(shapeManager);
        }
    }
    private T AddConfigShapeToBoard<T>(GameObject shapePrefab, ShapeData shapeData, Transform parent) where T : ShapeManager
    {
        GameObject shape = GameObject.Instantiate(shapePrefab, parent);
        shape.transform.localPosition = shapeData.Position;
        T shapeManager = shape.AddComponent<T>();
        shapeManager.InitializeShape(shapeData.ColorData, shapeData.shapeAddedNumber, shapeData.shapeId);

        return shapeManager;
    }
    private GameObject AddCanvasComponants(Transform rect)
    {
        GameObject temp = new GameObject();
        GameObject board = GameObject.Instantiate(temp, rect);
        board.name = "BoardCanvas";
        GameObject.DestroyImmediate(temp);
        Canvas boardCanvas = board.AddComponent<Canvas>();
        boardCanvas.sortingOrder = 3;
        boardCanvas.additionalShaderChannels = (AdditionalCanvasShaderChannels)1;
        board.AddComponent<CanvasScaler>();
        board.AddComponent<GraphicRaycaster>();
        return board;
    }
    private GameObject AddReferenceCanvasComponants(Vector3 position, Vector3 scale)
    {
        GameObject board = new GameObject("ReferenceGameBoard");

        board.transform.position = position;
        board.transform.localScale = scale;

        return board;
    }
    private void InstantiateGuideCanvas()
    {
        GameObject guideCanvasPrefab = m_dataManager.GetData<ConfigData.GuideCanvasConfigData>((int)levelConfig.GuideCanvas.name).prefab;
        GameObject.Instantiate(guideCanvasPrefab, levelConfig.GuideCanvas.position, Quaternion.identity);
    }
    private void InstantiatePins()
    {
        GameObject pinPlaceholder = GameObject.Instantiate(new GameObject(), levelConfig.pins.boardLocation, Quaternion.identity);
        pinPlaceholder.name = pinPlaceholder.ToString();
        SetGridFeatures(pinPlaceholder.AddComponent<GridLayoutGroup>());
        foreach (PinData pinData in levelConfig.pins.shapes)
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
        GameObject pinPointPrefab = m_dataManager.GetData<PrefabConfig>().PinPointPrefab;
        foreach (PinPointData pinpointData in levelConfig.gameBoardPinPoints)
        {
            GameObject pinpointObject = GameObject.Instantiate(pinPointPrefab, gameBoardCanvas.transform);
            pinpointObject.transform.localPosition = pinpointData.position;
            Pinpoint pinPointScript = pinpointObject.AddComponent<Pinpoint>();
            m_pinPoints.Add(pinPointScript);
            List<ShapeManager> ShapesCorrespandingToConfigIDs = m_shapes.Where(shape => pinpointData.neighborShapes.Contains(shape.shapeId)).ToList();
            pinPointScript.InitializePinPoint(ShapesCorrespandingToConfigIDs, pinpointData.pinPointColor, pinpointData.InitialColor, pinpointData.neighborShapes);
        }
    }
    private void InstantiatePin(PinData pinData, Transform parent)
    {
        GameObject pinPrefab = m_dataManager.GetData<PrefabConfig>().PinPrefab;
        GameObject pinObject = GameObject.Instantiate(pinPrefab, parent);
        Pin1 pinScript = pinObject.GetComponent<Pin1>();
        Button button = pinScript.GetComponentInChildren<Button>();
        pinScript.GetComponentInChildren<Button>().onClick.AddListener(() => ServiceLocator._instance.Get<Player>().PickPin(pinScript));
        pinScript.InitializePin(pinData.pincolor, pinData.pinCapacity);
    }
    private void SetHalfWayThereInitials()
    {
        foreach(Pinpoint pinPoint in m_pinPoints)
        {
            if(pinPoint.GetPinPointColor == VectorInt.White)
            {
                pinPoint.GetComponent<Button>().onClick.AddListener(() => pinPoint.ClickPinPoint());
            }
        }
    }
    private void SetNormalInitials()
    {
        foreach (Pinpoint pinPoint in m_pinPoints)
                pinPoint.GetComponent<Button>().onClick.AddListener(() => pinPoint.ClickPinPoint());
    }
    public List<ShapeManager> GetGeneratedShapes() => m_shapes;
    public List<ReferenceShapeManager> GetGeneratedReferenceShapes() => m_referenceShapes;
}