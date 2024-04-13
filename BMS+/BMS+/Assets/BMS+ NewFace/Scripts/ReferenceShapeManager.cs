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
        eventManager.StartListening(EventName.OnBlitzHappened, new Action<int, VectorInt>(BlitzEventRoutine));

        eventManager.StopListening(EventName.OnColorAdded, new Action<int, VectorInt>(AddColorRoutine));
        eventManager.StopListening(EventName.OnColorRemoved, new Action<int, VectorInt>(RemoveColorRoutine));
    }
    private void OnDisable()
    {
        eventManager.StopListening(EventName.OnBlitzHappened, new Action<int, VectorInt>(BlitzEventRoutine));
    }
    private void BlitzEventRoutine(int shapeEffected, VectorInt changeToColor)
    {

        if (shapeEffected == shapeId)
        {
            m_numOfAddedColors++;
            m_currentColor = changeToColor;
            UpdateNumOfAddedColorsText();
            UpdateSprite();
        }

    }


}
