using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbleStateOne : MonoBehaviour
{
    [SerializeField] GameObject m_waterBubble;
    [SerializeField] List<Transform> m_instantiateTransforms = new List<Transform>();
    private List<GameObject> m_instantiatedObjects = new List<GameObject>();


    private void OnEnable()
    {
        Debug.Log("I am here");
        InstantiateBubble();
    }
    private void InstantiateBubble()
    {
       foreach(Transform tempTransform in m_instantiateTransforms)
        {
            GameObject temp = Instantiate(m_waterBubble, tempTransform.position, Quaternion.identity);
            m_instantiatedObjects.Add(temp);
        }
        
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
