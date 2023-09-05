using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBubble : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }
    public void PopMoon()
    {
        m_rigidBody.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("LevelBoundry"))
        {
            AlwaysOnNigthsGameManager._instance.SetIsMoonGenerated(false);
        }
    }
}
