using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script is for picture of customer.
public class CustomerUiManager : MonoBehaviour
{
    [SerializeField] Sprite m_adultCustomer;
    [SerializeField] Sprite m_childCustomer;

    [SerializeField] Animator m_customerAnimator;

    private SpriteRenderer m_spriteRenderer;

    const string IS_HAPPY = "isHappy";
    const string IS_SAD = "isSad";


    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        SetCharacterSprite();
    }
    public void SetCharacterSprite()
    {
        int randomSpriteNumber = Random.Range(0, 1);

        if (randomSpriteNumber == 0)
        {
            m_spriteRenderer.sprite = m_adultCustomer;
        }
        else if (randomSpriteNumber == 1)
        {
            m_spriteRenderer.sprite = m_childCustomer;
        }
    }

    public void SetHappyAnimation()
    {
        m_customerAnimator.SetTrigger(IS_HAPPY);
    }

    public void SetSadAnimation()
    {
        m_customerAnimator.SetTrigger(IS_SAD);
    }

    public void SetIdleAnimation()
    {

    }
}
