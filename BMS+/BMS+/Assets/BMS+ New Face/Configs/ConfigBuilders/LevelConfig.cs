using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Level Config")]
public class LevelConfig : ScriptableObject
{
    public Vector3 PinPlaceholder; //{ get; private set; }
    public List<PinData> pins; //{ get; private set; }

    public Vector3 GameBoardCanvas; //{ get; private set; }
    public List<ShapeData> shapes; //{  get; private set; }

    public Vector3 ReferenceGameBoard; //{ get; private set; }
    public List<ShapeData> refrenceShapes; //{ get; private set; }

    public List<PinPointData> gameBoardPinPoints; //{ get; private set; }

    public GuidCanvasData GuideCanvas; //{ get; private set; }

    // public something events;

    public void SetLevelConfigData(List<PinData> pins, List<ShapeData> shapes, List<ShapeData> refrenceShapes,
        List<PinPointData> gamePinPoints, Vector3 pinPlaceHolder, Vector3 gameBoardCanvas,Vector3 referenceGameBoard,
        GuidCanvasData guideCanvas ) 
    { 
        this.pins = pins;
        this.shapes = shapes;
        this.refrenceShapes = refrenceShapes;
        this.gameBoardPinPoints = gamePinPoints;
        this.PinPlaceholder= pinPlaceHolder;
        this.GuideCanvas= guideCanvas;
        this.GameBoardCanvas= gameBoardCanvas;
        this.ReferenceGameBoard= referenceGameBoard;

    }


}
