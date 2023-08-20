using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbleStateOne : MonoBehaviour
{
    [SerializeField] GameObject m_waterBubble;
    [SerializeField] Transform m_instantiateTransform;
    [SerializeField] List<GameObject> m_instantiatedObjects = new List<GameObject>();


    private void OnEnable()
    {
        Debug.Log("I am here");
        InstantiateBubble();
    }
    private void InstantiateBubble()
    {
       
        GameObject temp = Instantiate(m_waterBubble, m_instantiateTransform.position, Quaternion.identity);
        m_instantiatedObjects.Add(temp);
    }

    private void OnDisable()
    {
        Debug.Log("Disabling here");
        foreach(GameObject temp in m_instantiatedObjects)
        {
            Destroy(temp.gameObject);
        }
    }
}
