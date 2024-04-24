using ConfigData;
using GameData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorPin : MonoBehaviour
{
    public PinData pinData;
    public PinConfigData pinColorData;
    private void Awake()
    {
        pinData = new PinData();
        pinData.pinCapacity = 0;
        pinData.pincolor = VectorInt.White;
    }
    public void SetPinCapacity(int number)=> pinData.pinCapacity=number;
    public void SetPinColor(VectorInt color) =>  pinData.pincolor = color;
    public void SetPinColorData(PinConfigData data) => pinColorData = data;
    public void AddPinNum()=> pinData.pinCapacity++;
    public void RemovePinNum()=>pinData.pinCapacity--;
}
