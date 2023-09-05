using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedValueBubble : MonoBehaviour
{
    private Rigidbody2D m_currentRigidBody;
    private int m_currentBubbleValue = 2;
    private void Start()
    {
        m_currentRigidBody =GetComponent<Rigidbody2D>();
    }

    public void Pop()
    {
        //Need to change its sprite too : 
        m_currentRigidBody.bodyType = RigidbodyType2D.Dynamic;
        
    }

    public int GetBubbleValue()
    {
        return m_currentBubbleValue;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LevelBoundry"))
        {
            AddedValueBubbleGenerator._instance.ReduceNumberOfActiveValueBubbles();
            Destroy(this.gameObject);
            
        }
    }
    public void SetCurrentBubbleValue(int value)
    {
        m_currentBubbleValue = value;
    }
}
