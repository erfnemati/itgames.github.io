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
    [SerializeField] float m_fullPinPointHeigth;
    [SerializeField] float m_fullPinPointWidth;
    [SerializeField] Sprite m_redObject;
    [SerializeField] Sprite m_yellowObject;
    [SerializeField] Sprite m_blueObject;

    private void GetInitialPinPointDimentions()
    {
        m_initialPinPointHeight = GetComponent<RectTransform>().rect.height;
        m_initialPinPointWidth = GetComponent<RectTransform>().rect.width;
    }

    private void GetInitialColor()
    {
        m_initialPinPointColor = GetComponent<Image>().color;
    }

    private void Start()
    {
        GetInitialColor();
        GetInitialPinPointDimentions();
        m_pinPoint = GetComponent<Button>();
        m_pinPoint.onClick.AddListener(()=>GetComponent<Pinpoint>().ClickPinPoint());
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

    }

    private void RemoveSprite()
    {

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
        ChangePinpointSprite();

    }

    private void UpdateHexagonColorsFromPinDeletion()
    {
        foreach(Hexagon tempHexagon in m_ownedHexagons)
        {
            tempHexagon.DeleteColor(FromPinColorToHexagonColor(m_currentPin));
        }
        m_currentPin = null;
    }

    private void UpdateHexagonColorsFromPinAddition(Pin chosenPin)
    {
        m_currentPin = chosenPin;
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
