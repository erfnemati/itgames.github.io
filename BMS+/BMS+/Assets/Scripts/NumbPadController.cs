using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;

public class NumbPadController : MonoBehaviour
{
    [SerializeField] char m_thisButtonChar;
    private TextInputManager m_phoneTextManager;

    private void Start()
    {
        m_phoneTextManager = FindObjectOfType<TextInputManager>();
    }

    public void AddDigit()
    {
        m_phoneTextManager.AddDigit(m_thisButtonChar);
    }

    public void RemoveDigit()
    {
        m_phoneTextManager.RemoveDigit();
    }


}
