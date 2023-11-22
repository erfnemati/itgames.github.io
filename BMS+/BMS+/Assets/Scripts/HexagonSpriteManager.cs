using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpriteManager : MonoBehaviour
{
    public static HexagonSpriteManager _instance;

    [SerializeField] Sprite m_whiteHexagonColorSprite;
    [SerializeField] Sprite m_blueHexagonColorSprite;
    [SerializeField] Sprite m_redHexagonColorSprite;
    [SerializeField] Sprite m_yellowHexagonColorSprite;
    [SerializeField] Sprite m_orangeHexagonColorSprite;
    [SerializeField] Sprite m_greenHexagonColorSprite;
    [SerializeField] Sprite m_purpleHexagonColorSprite;
    [SerializeField] Sprite m_brownHexagonColorSpirte;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

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
            return m_orangeHexagonColorSprite;
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
