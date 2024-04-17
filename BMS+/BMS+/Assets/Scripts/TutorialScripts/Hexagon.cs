using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

namespace Tutotrial
{
    public class Hexagon : MonoBehaviour
    {

        [SerializeField] protected int m_numOfAddedColors = 0;
        [SerializeField] protected List<HexagonColor> m_colorList = new List<HexagonColor>();
        [SerializeField] protected HexagonColor m_currentColor = HexagonColor.White;
        [SerializeField] protected RTLTextMeshPro m_numberOfAddedColorsText;
        [SerializeField] protected SpriteRenderer m_spriteRenderer;

        private void Start()
        {
            m_colorList.Add(HexagonColor.White);
            UpdateNumOfAddedColorsText();
            UpdateSprite();
        }

        public void AddColor(HexagonColor chosenColor)
        {
            m_colorList.Add(chosenColor);
            ReloadColorFromAddition(chosenColor);
            m_numOfAddedColors++;
            UpdateNumOfAddedColorsText();
        }

        public void DeleteColor(HexagonColor deletedColor)
        {
            foreach (HexagonColor hexagonColor in m_colorList)
            {
                if (hexagonColor == deletedColor)
                {
                    m_colorList.Remove(hexagonColor);
                    break;
                }
            }
            m_numOfAddedColors--;
            ReloadColorFromDeletion();
            UpdateNumOfAddedColorsText();
        }

        private void UpdateNumOfAddedColorsText()
        {
            if (m_numOfAddedColors > 0)
            {
                m_numberOfAddedColorsText.text = m_numOfAddedColors + "";
            }
            else
            {
                m_numberOfAddedColorsText.text = " ";
            }


        }


        private void ReloadColorFromDeletion()
        {
            m_currentColor = HexagonColorManager._instance.GetColor(m_colorList);
            UpdateSprite();
        }

        private void ReloadColorFromAddition(HexagonColor chosenColor)
        {
            m_currentColor = HexagonColorManager._instance.GetColor(m_currentColor, chosenColor);
            UpdateSprite();
        }

        protected void UpdateSprite()
        {
            //m_spriteRenderer.sprite =  HexagonSpriteManager._instance.GetHexagonSprite(m_currentColor);
            if (m_spriteRenderer == null)
            {
                Debug.Log("Sprite null here");
            }

            if (HexagonSpritePicker._instance == null)
            {
                Debug.Log("Sprite picker is null");
            }
            m_spriteRenderer.sprite = HexagonSpritePicker._instance.GetHexagonSprite(m_currentColor);

        }

        public HexagonColor GetHexagonColor()
        {
            return m_currentColor;
        }

        public void SetHexagonColor(HexagonColor hexagonColor)
        {
            m_currentColor = hexagonColor;
            UpdateSprite();
        }

        public int GetHexagonNumber()
        {
            return m_numOfAddedColors;
        }



    }
}
