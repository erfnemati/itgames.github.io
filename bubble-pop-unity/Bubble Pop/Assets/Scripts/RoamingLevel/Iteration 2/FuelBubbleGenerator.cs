using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBubbleGenerator : MonoBehaviour
{
    [SerializeField] List<string> m_adWords = new List<string>();

    private int adWordsIndex = 0;

    [SerializeField] GameObject m_fuelBubbleObject;
    [SerializeField] Transform m_instantiateTransform;

    public void InstansiateFuelBubble()
    {
        GameObject temp = Instantiate(m_fuelBubbleObject, m_instantiateTransform.position, Quaternion.identity);
        SetFuelBubbleText(temp);
    }

    private void SetFuelBubbleText(GameObject gameObject)
    {
        FuelBubble fuelBubble = gameObject.GetComponent<FuelBubble>();
        fuelBubble.SetText(m_adWords[adWordsIndex]);
        adWordsIndex = (adWordsIndex + 1) % m_adWords.Count;
    }
}
