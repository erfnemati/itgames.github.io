using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutotrial
{
    public class HexagonSpritePicker : MonoBehaviour
    {
        public static HexagonSpritePicker _instance;

        [SerializeField] Sprite m_whiteHexagonColorSprite;
        [SerializeField] Sprite m_blueHexagonColorSprite;
        [SerializeField] Sprite m_redHexagonColorSprite;
        [SerializeField] Sprite m_yellowHexagonColorSprite;
        [SerializeField] Sprite m_orangeHexagonColorSprite;
        [SerializeField] Sprite m_greenHexagonColorSprite;
        [SerializeField] Sprite m_purpleHexagonColorSprite;
        [SerializeField] Sprite m_brownHexagonColorSpirte;

        /*[SerializeField] Color m_whiteColor;
        [SerializeField] Color m_redColor;
        [SerializeField] Color m_blueColor;
        [SerializeField] Color m_yellowColor;
        [SerializeField] Color m_orangeColor;
        [SerializeField] Color m_purpleColor;
        [SerializeField] Color m_greenColor;
        [SerializeField] Color m_brownColor;*/



        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        /*public Color GetHexagonColor(HexagonColor color)
        {
            if (color == HexagonColor.White)
            {
                return m_whiteColor;
            }
            else if (color == HexagonColor.Blue)
            {
                return m_blueColor;
            }
            else if (color == HexagonColor.Red)
            {
                return m_redColor;
            }
            else if (color == HexagonColor.Yellow)
            {
                return m_yellowColor;
            }
            else if (color == HexagonColor.Orange)
            {
                return m_orangeColor; ;
            }
            else if (color == HexagonColor.Green)
            {
                return m_greenColor;
            }
            else if (color == HexagonColor.Purple)
            {
                return m_purpleColor;
            }
            else if (color == HexagonColor.Brown)
            {
                return m_brownColor;
            }
            else
            {
                return m_whiteColor;
            }
        }*/

        public Sprite GetHexagonSprite(HexagonColor color)
        {
            if (color == HexagonColor.White)
            {
                return m_whiteHexagonColorSprite;
            }
            else if (color == HexagonColor.Blue)
            {
                return m_blueHexagonColorSprite;
            }
            else if (color == HexagonColor.Red)
            {
                return m_redHexagonColorSprite;
            }
            else if (color == HexagonColor.Yellow)
            {
                return m_yellowHexagonColorSprite;
            }
            else if (color == HexagonColor.Orange)
            {
                return m_orangeHexagonColorSprite; ;
            }
            else if (color == HexagonColor.Green)
            {
                return m_greenHexagonColorSprite;
            }
            else if (color == HexagonColor.Purple)
            {
                return m_purpleHexagonColorSprite;
            }
            else if (color == HexagonColor.Brown)
            {
                return m_brownHexagonColorSpirte;
            }
            else
            {
                return m_whiteHexagonColorSprite;
            }
        }
    }
}
