using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinpoint : MonoBehaviour
{
    [SerializeField] List<Hexagon> m_ownedHexagons;

    private Button m_pinPoint;
    private Pin m_currentPin = null;

    //Ui stuff here,maybe recycle later:
    private float m_initialPinPointWidth;
    private float m_initialPinPointHeight;
    private Color m_initialPinPointColor;
    private RectTransform m_pinPointRect;
    [SerializeField] Sprite m_initialPinPointSprite;
    [SerializeField] float m_fullPinPointScaleFactor;
 
    [SerializeField] Sprite m_redObject;
    [SerializeField] Sprite m_yellowObject;
    [SerializeField] Sprite m_blueObject;

    private void GetInitialPinPointDimentions()
    {
        m_initialPinPointHeight = m_pinPointRect.rect.height;
        m_initialPinPointWidth = m_pinPointRect.rect.width;
    }

    private void GetInitialColor()
    {
        m_initialPinPointColor = GetComponent<Image>().color;
    }

    private void Start()
    {
        GetRectTransform();
        GetInitialColor();
        GetInitialPinPointDimentions();
        
        m_pinPoint = GetComponent<Button>();
        m_pinPoint.onClick.AddListener(()=>GetComponent<Pinpoint>().ClickPinPoint());
    }

    private void GetRectTransform()
    {
        m_pinPointRect = GetComponent<RectTransform>();
    }


    private void ChangePinpointSprite()
    {
        if (m_currentPin == null)
        {
            RemoveSprite();
        }
        else
        {
            AddSprite();
        }
    }

    private void AddSprite()
    {
        float temp = m_initialPinPointHeight * m_fullPinPointScaleFactor;
        m_pinPointRect.sizeDelta = new Vector2(m_initialPinPointWidth, temp);
        
        m_pinPoint.image.color = new Color(255, 255, 255, 255);
        ChooseSprite();
        

    }

    private void ChooseSprite()
    {
        if (m_currentPin.GetPinColor() == PinColor.Blue)
        {
            m_pinPoint.image.sprite = m_blueObject;
        }
        else if (m_currentPin.GetPinColor() == PinColor.Yellow)
        {
            m_pinPoint.image.sprite = m_yellowObject;
        }
        else if (m_currentPin.GetPinColor() == PinColor.Red)
        {
            m_pinPoint.image.sprite = m_redObject;
        }
        else
        {
            m_pinPoint.image.sprite = m_initialPinPointSprite;
        }
    }

    private void RemoveSprite()
    {
        m_pinPointRect.sizeDelta = new Vector2(m_initialPinPointWidth, m_initialPinPointHeight);
        m_pinPoint.image.sprite = m_initialPinPointSprite;
        m_pinPoint.image.color = m_initialPinPointColor;
    }

    public void ClickPinPoint()
    {
        if(Player._instance.GetPlayerPin() == null)
        {
            return;
        }
        if (m_currentPin == null)
        {
            AddPin(Player._instance.GetPlayerPin());
        }
        else if (m_currentPin != null)
        {
            RemovePin();
        }
    }

    private void AddPin(Pin chosenPin)
    {
        if (m_currentPin != null)
        {
            Debug.Log("This pinpoint is full");
            return;
        }
        m_currentPin = chosenPin;
        UpdateHexagonColorsFromPinAddition(chosenPin);
        ChangePinpointSprite();
        
    }

    private void RemovePin()
    {
        if (m_currentPin == null)
        {
            return;
        }
        
        UpdateHexagonColorsFromPinDeletion();
        m_currentPin = null;
        ChangePinpointSprite();

    }

    private void UpdateHexagonColorsFromPinDeletion()
    {
        foreach(Hexagon tempHexagon in m_ownedHexagons)
        {
            tempHexagon.DeleteColor(FromPinColorToHexagonColor(m_currentPin));
        }
        
    }

    private void UpdateHexagonColorsFromPinAddition(Pin chosenPin)
    {
        
        foreach(Hexagon tempHexagon in m_ownedHexagons)
        {
            tempHexagon.AddColor(FromPinColorToHexagonColor(chosenPin));
        }
    }

    private HexagonColor FromPinColorToHexagonColor(Pin pin)
    {
        PinColor pinColor = pin.GetPinColor();
        if (pinColor == PinColor.Yellow)
        {
            return HexagonColor.Yellow;
        }
        else if (pinColor == PinColor.Red)
        {
            return HexagonColor.Red;
        }
        else if (pinColor == PinColor.Blue)
        {
            return HexagonColor.Blue;
        }
        return HexagonColor.White;
    }
}
