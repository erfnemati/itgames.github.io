using GameData;
using GameEnums;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SceneToConfigCreator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ShapeConfig shapeConfig; 
    private GameObject GameBoard;
    private GameObject ReferenceBoard;
    private GameObject Redpin;
    private GameObject Bluepin;
    private GameObject Yellowpin;
    readonly string path = "assets/BMS+ NewFace/Configs/DemoLevels";

    private LevelConfigData level;
    void Start()
    {
        LevelConfigData levelData = (LevelConfigData)Resources.Load("generalLevel");
        level = Instantiate<LevelConfigData>(levelData);
        AssetDatabase.CreateAsset(level, path);
        FIndGameObjects();

    }

    private void FIndGameObjects()
    {
        GameBoard = GameObject.Find("GameBoard");
        ReferenceBoard = GameObject.Find("ReferenceGameBoard");
        Redpin = GameObject.Find("RedPinCanvas");
        Bluepin = GameObject.Find("BluePinCanvas");
        Yellowpin = GameObject.Find("YellowPinCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SavePinsToConfig()
    {
        level.pins.shapes[0].pinCapacity = Redpin.GetComponent<Pin>().GetNumberOfUsage();
        level.pins.shapes[1].pinCapacity = Yellowpin.GetComponent<Pin>().GetNumberOfUsage();
        level.pins.shapes[2].pinCapacity = Redpin.GetComponent<Pin>().GetNumberOfUsage();

    }

    public void SaveGameBoardToConfig()
    {
        boardData<ShapeData> data = new boardData<ShapeData>();
        data.shapes = new List<ShapeData>();
        data.boardLocation = GameBoard.transform.localPosition;
        data.boardScale = GameBoard.transform.localScale;
        List<Hexagon> hexagons = GameBoard.GetComponent<GameBoard>().GetHexagons();
        for(int i = 0; i < hexagons.Count; i++)
        {
            ShapeData shapeData = new ShapeData();
            shapeData.shapeId = i;
            shapeData.ColorData = VectorInt.White;
            shapeData.shapeAddedNumber = 0;
            shapeData.Position = hexagons[i].transform.localPosition;
            data.shapes.Add(shapeData);
        }
        level.GameBoardCanvas = data;

    }
    public void SavePinPointsToConfig()
    {
        level.gameBoardPinPoints = new List<PinPointData>();
        List<Hexagon> gameBoardHexagons = GameBoard.GetComponent<GameBoard>().GetHexagons();
        foreach (Pinpoint pinpoint in GameBoard.GetComponentsInChildren<Pinpoint>())
        {
            PinPointData data = new PinPointData();
            data.position = pinpoint.transform.localPosition;
            data.neighborShapes = new List<int>();
            data.pinPointColor=VectorInt.White;
            data.InitialColor = new Color(1, 1, 1, 0.4f);
            foreach(var hex in pinpoint.GetOwnedHexagons())
                for(int i=0;i< GameBoard.GetComponent<GameBoard>().GetHexagons().Count; i++)
                {
                    if(hex == gameBoardHexagons[i])
                    {
                        data.neighborShapes.Add(i);
                    }
                }
        }


    }
    public void SaveReferenceBoardToConfig(GameObject guidCanvas)
    {
        boardData<ShapeData> data = new boardData<ShapeData>();
        data.shapes = new List<ShapeData>();
        data.boardLocation = ReferenceBoard.transform.localPosition;
        data.boardScale = ReferenceBoard.transform.localScale;
        List<ReferenceHexagon> hexagons = ReferenceBoard.GetComponent<ReferenceGameBoard>().GetReferenceHexagons();
        for (int i = 0; i < hexagons.Count; i++)
        {
            ShapeData shapeData = new ShapeData();
            shapeData.shapeId = i;
            shapeData.ColorData = shapeConfig.shapeColors.Find(shape => (int)shape.name== (int)hexagons[i].GetHexagonColor()).color;
            shapeData.shapeAddedNumber = hexagons[i].GetHexagonNumber() ;
            shapeData.Position = hexagons[i].transform.localPosition;
            data.shapes.Add(shapeData);
        }
        level.GameBoardCanvas = data;
    }
}
