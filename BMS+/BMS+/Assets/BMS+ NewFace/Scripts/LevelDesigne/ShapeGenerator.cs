using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelDesign;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEditor;
using System;
using System.Linq;
public class ShapeGenerator
{
    private LevelDesignBoard board;
    public ShapeGenerator(LevelDesignBoard board)
    {
        this.board = board;
    }
    public void GenerateRectangles()
    {

    }
    public void GenerateTriangles()
    {
    }
    public void GenerateHexagons()
    {
        Transform rect = board.GetComponent<Transform>();
        Transform canvasTransform = AddCanvasComponants(rect);
        //hexagon Generation Attribiutes
        float margin = 0.2f;
        float hexLengthX = 0.63f;
        float hexLengthY = 0.5f;
        float shift;
        float OffSetY = 0.5f;
        Vector3 rootPosition = rect.position;


        for (int i = 0; i < board.gameBoardSize.x; i++)
        {
            shift = (i % 2 != 0) ? 0.32f : 0;
            for (int j = 0; j < board.gameBoardSize.y; j++)
            {
                Vector3 position = new Vector3(rootPosition.x - shift - j * hexLengthX, rootPosition.y + OffSetY + i * hexLengthY, 0);
                int shapeId = i * (int)board.gameBoardSize.x + j;
                InstantiateShape(rect, position, shapeId);

                InstantiateTopPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                InstantiateTopLeftPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                if (j == 0 && i % 2 == 0)
                {
                    InstantiateTopRightPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                    InstantiateBotRightPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                    InstantiateBotPinPoint(canvasTransform, position, hexLengthX, hexLengthY);

                }
                if (i == 0)
                {
                    InstantiateBotLeftPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                    InstantiateBotPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                }

                if (j == board.gameBoardSize.y - 1 && i % 2 != 0)
                {
                    InstantiateBotLeftPinPoint(canvasTransform, position, hexLengthX, hexLengthY);
                }


            }
        }
    }

    private void InstantiateShape(Transform rect, Vector3 position, int shapeID)
    {
        GameObject shape = GameObject.Instantiate(LevelDesignBoard._instance.GetData<PrefabConfig>().ShapePrefab, position, Quaternion.identity, rect);
        EditorShapeManager shapeManager = shape.AddComponent<EditorShapeManager>();
        shapeManager.SetID(shapeID);
        board.shapeManagerList.Add(shapeManager);

    }

    private void InstantiateBotLeftPinPoint(Transform parent, Vector3 position, float hexLengthX, float hexLengthY)
    {
        Vector3 bottomLeftPosition = new Vector3(position.x - hexLengthX / 2, position.y - hexLengthY / 3, position.z);
        InstantiateAndConfigurePinPoint(parent, bottomLeftPosition);

    }

    private void InstantiateBotPinPoint(Transform parent, Vector3 position, float hexLengthX, float hexLengthY)
    {
        Vector3 bottomPosition = new Vector3(position.x, position.y - 2 * hexLengthY / 3, position.z);
        InstantiateAndConfigurePinPoint(parent, bottomPosition);

    }

    private void InstantiateBotRightPinPoint(Transform parent, Vector3 position, float hexLengthX, float hexLengthY)
    {

        Vector3 bottomRightPosition = new Vector3(position.x + hexLengthX / 2, position.y - hexLengthY / 3, position.z);
        InstantiateAndConfigurePinPoint(parent, bottomRightPosition);
    }

    private void InstantiateTopRightPinPoint(Transform parent, Vector3 position, float hexLengthX, float hexLengthY)
    {
        Vector3 topRightPosition = new Vector3(position.x + hexLengthX / 2, position.y + hexLengthY / 3, position.z);
        InstantiateAndConfigurePinPoint(parent, topRightPosition);
    }

    private void InstantiateTopLeftPinPoint(Transform parent, Vector3 position, float hexLengthX, float hexLengthY)
    {
        Vector3 topLeftPosition = new Vector3(position.x - hexLengthX / 2, position.y + hexLengthY / 3, position.z);
        InstantiateAndConfigurePinPoint(parent, topLeftPosition);

    }

    private void InstantiateTopPinPoint(Transform parent, Vector3 position, float hexLengthX, float hexLengthY)
    {
        Vector3 topPosition = new Vector3(position.x, position.y + 2 * hexLengthY / 3, position.z);
        InstantiateAndConfigurePinPoint(parent, topPosition);
    }

    private void InstantiateAndConfigurePinPoint(Transform parent, Vector3 position)
    {
        GameObject pinPoint = GameObject.Instantiate(LevelDesignBoard._instance.GetData<PrefabConfig>().PinPointPrefab, position, Quaternion.identity, parent);
        board.pinPointList.Add(pinPoint.AddComponent<EditorPinPoint>());
    }



    private Transform AddCanvasComponants(Transform rect)
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
        return board.transform;
    }

    public void AddPinPointNeighbors()
    {
        foreach (EditorPinPoint pinPoint in board.pinPointList)
        {
            pinPoint.SetNeighborShapes();
        }
    }

    //Refference Board
    public void InitializeReferenceBoard()
    {
        CreateReferenceBoard();
        InitializeShapes();
        setReferenceBoardTransform();
        SetShapeData();
    }
    private void CreateReferenceBoard()
    {
        board.referenceBoard = new GameObject("Reference Board");
        board.referenceBoard.transform.position = board.transform.position; // get this from gameboard instance; 

    }
    private void InitializeShapes()
    {

        foreach(var shape in board.shapeManagerList)
        {
            GameObject referenceShape = GameObject.Instantiate(LevelDesignBoard._instance.GetData<PrefabConfig>().ShapePrefab,
                shape.transform.localPosition,
                Quaternion.identity, board.referenceBoard.transform);
            var refereneShapeManager=referenceShape.AddComponent<EditorShapeManager>();
            refereneShapeManager.SetID(shape.shapeData.shapeId);
            board.referenceShapeManagerList.Add(refereneShapeManager );

        }

    }
    private void setReferenceBoardTransform()
    {
        board.referenceBoard.transform.localPosition = new Vector3(2.4f, -2, 0.03f);
        board.referenceBoard.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

    }
    private void SetShapeData()
    {
        foreach(var shape in board.referenceShapeManagerList)
        {
            shape.SetPosition();
        }
    }

    public void RemoveShapes()
    {
        int shapeCount = board.shapeManagerList.Count;
        int pinpointCount=board.pinPointList.Count;
        for(int i=0; i<shapeCount; i++)
            GameObject.DestroyImmediate(board.shapeManagerList.First().gameObject);
        for(int i=0;i<pinpointCount; i++)
            GameObject.DestroyImmediate(board.pinPointList.First().gameObject);
    }

    public void RemoveUnUsedPinPoints()
    { 

        List<EditorPinPoint> unUsedPinpoints=board.pinPointList.Where(item => item.pinPointData.neighborShapes.Count == 0).ToList();
        var count = unUsedPinpoints.Count;
        for (int i = 0; i <count ; i++)
            GameObject.DestroyImmediate(unUsedPinpoints.ElementAt(i).gameObject);
    }
}
