using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutotrial
{
    public class ReferenceHexagon : Hexagon
    {
        //[SerializeField] int m_hexagonAddedColorNumber;

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

        //public int GetReferenceHexagonNumber()
        //{
        //    return m_hexagonAddedColorNumber;
        //}

    }
}
