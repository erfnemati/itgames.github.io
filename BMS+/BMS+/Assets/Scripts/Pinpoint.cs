using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinpoint : MonoBehaviour
{
    [SerializeField] List<Hexagon> m_ownedHexagons;
    private Pin m_currentPin = null;
    

    private void ChangePinpointSprite()
    {
        //For ui purposes
    }

    public void AddPin(Pin chosenPin)
    {
        if (m_currentPin != null)
        {
            return;
        }
        UpdateHexagonColorsFromPinAddition(chosenPin);
        ChangePinpointSprite();
        
    }

    public void RemovePin()
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
