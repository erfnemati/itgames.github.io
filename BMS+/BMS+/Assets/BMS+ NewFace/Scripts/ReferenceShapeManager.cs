using GameData;
using RTLTMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReferenceShapeManager : ShapeManager
{
    private void OnEnable()
    {
        eventManager.StartListening(EventName.OnBlitzHappened, new Action<EventData>(BlitzEventRoutine));

        eventManager.StopListening(EventName.OnColorAdded, new Action<int, VectorInt>(AddColorRoutine));
        eventManager.StopListening(EventName.OnColorRemoved, new Action<int, VectorInt>(RemoveColorRoutine));
    }
    private void OnDestroy()
    {
        eventManager.StopListening(EventName.OnBlitzHappened, new Action<EventData>(BlitzEventRoutine));
    }
    private void BlitzEventRoutine( EventData eventData)
    {
        Debug.Log("Here3");

        if (eventData.shapeId == shapeId)
        {
            Debug.Log("here4");
            m_numOfAddedColors=eventData.shapeAddedNumber;
            m_currentColor = eventData.changeToColor;
            UpdateNumOfAddedColorsText();
            UpdateSprite();
        }

    }


}
