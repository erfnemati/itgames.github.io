using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbleStateTwo : MonoBehaviour
{
    [SerializeField] List<Transform> m_instantiatedTransforms = new List<Transform>();
    [SerializeField] GameObject m_waterBubbleObject;
    private List<GameObject> m_instantiatedGameObjects = new List<GameObject>();

    private void OnEnable()
    {
        PlayWaterState();
    }

    private void PlayWaterState()
    {
        Debug.Log("Playing state two right now");
        foreach (Transform tempTransform in m_instantiatedTransforms)
        {
            GameObject temp = Instantiate(m_waterBubbleObject, tempTransform.position, Quaternion.identity);
            m_instantiatedGameObjects.Add(temp);
            
        }
    }

    private void OnDisable()
    {
        foreach(GameObject temp in m_instantiatedGameObjects)
        {
            Destroy(temp.gameObject);
        }
    }
}
