using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorPin : MonoBehaviour
{
    public PinData pinData;
    public PinColorData pinColorData;
    private void Awake()
    {
        pinData = new PinData();
        pinData.pinCapacity = 0;
        pinData.pincolor = Color.white;
    }
    public void SetPinCapacity(int number)=> pinData.pinCapacity=number;
    public void SetPinColor(Color color) =>  pinData.pincolor = color;
    public void SetPinColorData(PinColorData data) => pinColorData = data;
    public void AddPinNum()=> pinData.pinCapacity++;
    public void RemovePinNum()=>pinData.pinCapacity--;
}
