using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceShapeManager : ShapeManager
{
    private void UpdateHexagonAddedColorNumber()
    {
        m_numberOfAddedColorsText.text = m_numOfAddedColors + "";
    }

    private void Awake()
    {
        if (m_numOfAddedColors > 0)
        {
            UpdateHexagonAddedColorNumber();
        }

    }

    private void Start()
    {
        UpdateSprite();
    }

}
