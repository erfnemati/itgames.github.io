using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _instance;
    Pin currentPlayerPin;

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

    public Pin GetPlayerPin()
    {
        return currentPlayerPin;
    }

    
    private void SetPin(PinColor chosenPinColor)
    {
        Pin tempPin = new Pin(chosenPinColor);
        if (tempPin.GetPinColor() != currentPlayerPin.GetPinColor())
        {
            currentPlayerPin.ResetPinUi();
            currentPlayerPin = new Pin(chosenPinColor);
        }
        
    }

    //Probably not the best idea:

    public void PickRedPin()
    {
        SetPin(PinColor.Red);
    }

    public void PickYellowPin()
    {
        SetPin(PinColor.Yellow);
    }

    public void PickBluePin()
    {
        SetPin(PinColor.Blue);
    }
}
