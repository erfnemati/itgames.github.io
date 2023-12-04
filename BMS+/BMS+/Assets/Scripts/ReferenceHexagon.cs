using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHexagon : Hexagon
{
    [SerializeField] int m_hexagonAddedColorNumber;

    private void UpdateHexagonAddedColorNumber()
    {
        m_numberOfAddedColorsText.text = m_hexagonAddedColorNumber + "";
    }

    private void Awake()
    {
        if (m_hexagonAddedColorNumber > 0)
        {
            UpdateHexagonAddedColorNumber();
        }
        
    }


}
