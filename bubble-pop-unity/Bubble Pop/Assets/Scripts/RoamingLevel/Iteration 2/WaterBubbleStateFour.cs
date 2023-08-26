using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubbleStateFour : MonoBehaviour
{
    [SerializeField] List<Transform> m_fixedWaterBubbleTransform = new List<Transform>();
    [SerializeField] List<Transform> m_movingWaterBubbleTransform = new List<Transform>();
    [SerializeField] List<Vector3> m_movingWaterBubbleDirection = new List<Vector3>();
    [SerializeField] List<int> m_movingWaterBubbleTravelDistance = new List<int>();
    

    [SerializeField] GameObject m_fixedBubblePrefab;
    [SerializeField] GameObject m_movingBubblePrefab;

    [SerializeField] float m_waterBubbleMovingSpeed;
    

    private List<GameObject> m_instantiatedObjects = new List<GameObject>();

    private void OnEnable()
    {
        GenerateFixedBubbles();
        GenerateMovingBubbles();
    }

    private void GenerateFixedBubbles()
    {
        foreach(Transform transform in m_fixedWaterBubbleTransform)
        {
            GameObject temp = Instantiate(m_fixedBubblePrefab, transform.position, Quaternion.identity);
            m_instantiatedObjects.Add(temp);
        }
    }

    private void GenerateMovingBubbles()
    {
        for(int i = 0; i < m_movingWaterBubbleTransform.Count; i++)
        {
            GameObject temp = Instantiate(m_movingBubblePrefab, m_movingWaterBubbleTransform[i].position, Quaternion.identity);
            m_instantiatedObjects.Add(temp);

            MoveWaterBubble tempMoveWaterBubble = temp.GetComponent<MoveWaterBubble>();
            tempMoveWaterBubble.SetSpeed(m_waterBubbleMovingSpeed);
            tempMoveWaterBubble.SetDirection(m_movingWaterBubbleDirection[i]);
            tempMoveWaterBubble.SetDistance(m_movingWaterBubbleTravelDistance[i]);
            
        }

    }

    private void OnDisable()
    {
        foreach(GameObject tempObject in m_instantiatedObjects)
        {
            Destroy(tempObject);
        }
    }



}
