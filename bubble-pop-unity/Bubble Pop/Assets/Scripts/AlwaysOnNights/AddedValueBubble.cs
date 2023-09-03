using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddedValueBubble : MonoBehaviour
{
    private Rigidbody2D m_currentRigidBody;
    private void Start()
    {
        m_currentRigidBody =GetComponent<Rigidbody2D>();
    }

    public void Pop()
    {
        //Need to change its sprite too : 
        m_currentRigidBody.bodyType = RigidbodyType2D.Dynamic;
        
    }

    public float GetBubbleValue()
    {
        return 10;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("LevelBoundry"))
        {
           
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        AddedValueBubbleGenerator._instance.ReduceNumberOfActiveValueBubbles();
    }
}
