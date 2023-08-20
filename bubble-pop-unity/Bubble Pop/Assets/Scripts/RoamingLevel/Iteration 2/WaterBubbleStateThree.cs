using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbleStateThree : MonoBehaviour
{
    [SerializeField] List<Transform> m_instantiatedTransforms = new List<Transform>();
    [SerializeField] List<GameObject> m_bubbles = new List<GameObject>();
    List<GameObject> m_instantiatedGameObjects = new List<GameObject>();

    private int m_bubbleIndex = 0;
    private void OnEnable()
    {
        PlayState();
    }

    private void PlayState()
    {
        foreach(Transform temp in m_instantiatedTransforms)
        {
            GameObject newTemp = Instantiate(m_bubbles[m_bubbleIndex], temp.position, Quaternion.identity);
            m_bubbleIndex = (m_bubbleIndex + 1) % m_bubbles.Count;
            m_instantiatedGameObjects.Add(newTemp);
        }
    }

    private void OnDisable()
    {
        foreach(GameObject temp in m_instantiatedGameObjects)
        {
            Destroy(temp);
        }
    }
}
