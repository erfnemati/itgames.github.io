using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using LevelDesign;
using RTLTMPro;

[CustomEditor(typeof(EditorPinPoint))]
public class EditorPinPointWindow : Editor
{
    private Rect windowRect = new Rect(20, 20, 120, 50);
    private EditorPinPoint pinPointManager;
 
    private void Awake()
    {
        pinPointManager =(EditorPinPoint)target;
        
    }
    private void OnSceneGUI()
    {
        if(LevelDesignBoard._instance.phase==LevelDesignPhase.Phase4)
            windowRect = GUILayout.Window(0, windowRect, WindowFunction, "My Window");
    }

    private void WindowFunction(int windowID)
    {
        
        foreach (EditorPin pin in LevelDesignBoard._instance.pinList)
        {
            if (GUILayout.Button(pin.pinColorData.name.ToString()))
            {
                AddClickedPin(pin);
            }    
        }
        if (GUILayout.Button("Clear"))
        {
            ResetPinPoint();
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 10000));
    }
    private void AddClickedPin(EditorPin pin)
    {
        if(pinPointManager.stationedPin ==null)
        {
            pin.AddPinNum();
            pinPointManager.stationedPin = pin;
            pinPointManager.InvokeAddColorEvent(pin.pinColorData.color);
            Image image= target.GetComponent<Image>();
            image.sprite = pin.pinColorData.sprite;
            image.color=Color.white;
            pin.GetComponentInChildren<RTLTextMeshPro>().text=pin.pinData.pinCapacity.ToString();//kasif

        }
    }
    // initial color is not black
    private void ResetPinPoint()
    {
        if(pinPointManager.stationedPin != null)
        {
            pinPointManager.stationedPin.RemovePinNum();
            pinPointManager.InvokeRemoveColorEvent(pinPointManager.stationedPin.pinColorData.color);
            pinPointManager.stationedPin.GetComponentInChildren<RTLTextMeshPro>().text=pinPointManager.stationedPin.pinData.pinCapacity.ToString();//kasif
            pinPointManager.stationedPin = null;
            Image image = target.GetComponent<Image>();
            image.sprite = pinPointManager.initialSprite;
            image.color = Color.black;

        }
    }
}
