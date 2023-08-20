using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleUiControllerSecIteration : MonoBehaviour
{
    [SerializeField] Slider m_FuelSilder;

    public void UpdateSlider(int value)
    {
        m_FuelSilder.value = value;
    }

    public void DeactivateUiComponent()
    {
        m_FuelSilder.gameObject.SetActive(false);
    }
}
