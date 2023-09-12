using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using RTLTMPro;

public class AddedValueBubble : MonoBehaviour
{
    private Rigidbody2D m_currentRigidBody;
    private int m_currentBubbleValue = 1;

    [SerializeField] float m_finalScale;

    [SerializeField] Sprite m_popedStar;
    [SerializeField] RTLTextMeshPro m_starText;

    private void Start()
    {
        SetText();
        transform.localScale = Vector3.zero;
        transform.DOScale(m_finalScale, 0.5f);
        m_currentRigidBody =GetComponent<Rigidbody2D>();
    }

    public void Pop()
    {
        GetComponent<SpriteRenderer>().sprite = m_popedStar;
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

    private void SetText()
    {
        int randomIndex = Random.Range(1, 4);

        if (randomIndex == 1)
        {
            m_starText.text = "شارژ 10000 تومانی";
        }
        else if (randomIndex == 2)
        {
            m_starText.text = "شارژ 5000 تومانی";
        }
        else
        {
            m_starText.text = "شارژ 2000 تومانی";
        }
    }
}
