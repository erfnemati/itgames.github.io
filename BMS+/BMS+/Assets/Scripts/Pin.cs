using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] PinColor m_pinColor;

    public PinColor GetPinColor()
    {
        return m_pinColor;
    }
}
