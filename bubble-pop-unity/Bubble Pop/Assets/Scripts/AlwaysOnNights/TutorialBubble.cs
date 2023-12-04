using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubble : MonoBehaviour
{
    [SerializeField] Sprite m_popedBubbleStar;
    public void PopTutorialBubble()
    {
        GetComponent<SpriteRenderer>().sprite = m_popedBubbleStar;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
