using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RTLTMPro;

public class FuelBubble : MonoBehaviour
{
    [SerializeField] RTLTextMeshPro m_rtlText;
    

    public void SetText(string text)
    {
        m_rtlText.text = text;
    }
}
