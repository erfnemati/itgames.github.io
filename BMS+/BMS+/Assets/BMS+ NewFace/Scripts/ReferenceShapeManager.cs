using RTLTMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        colorManager = new ShapesColorManager();
        m_numberOfAddedColorsText = gameObject.GetComponentsInChildren<RTLTextMeshPro>().First();
        m_spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        UpdateNumOfAddedColorsText();
        UpdateSprite();
    }

}
