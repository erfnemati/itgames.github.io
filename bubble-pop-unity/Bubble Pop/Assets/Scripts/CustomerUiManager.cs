using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using TMPro;
using DG.Tweening;
using RTLTMPro;

//This script is for picture of customer.
public class CustomerUiManager : MonoBehaviour
{

    // Character Apearance : 
    [SerializeField] Sprite m_adultCustomer;
    [SerializeField] Sprite m_childCustomer;


    // Animation Parameters : 
    [SerializeField] Animator m_customerAnimator;
    private SpriteRenderer m_spriteRenderer;
    const string IS_HAPPY = "isHappy";

    //Ui parameters : 
    [SerializeField] GameObject m_firstDialogueBox;
    [SerializeField] RTLTextMeshPro m_firstDialogueBoxText;
    [SerializeField] Image m_firstDialogueBoxImage;

    [SerializeField] GameObject m_secDialogueBox;
    [SerializeField] RTLTextMeshPro m_secDialogueBoxText;
    [SerializeField] Image m_secDialogueBoxImage;

    [SerializeField] GameObject m_thirdDialogueBox;
    [SerializeField] RTLTextMeshPro m_thirdDialogueBoxText;
    [SerializeField] Image m_thirdDialogueBoxImage;

    [SerializeField] Sprite m_dataIcon;
    [SerializeField] Sprite m_callTimeIcon;
    [SerializeField] Sprite m_messageIcon;


    [SerializeField] TMP_Text m_requestValue;
    [SerializeField] GameObject m_heartObject;
    [SerializeField] GameObject m_burninMoneyObject;

    [SerializeField] Sprite m_zeroHeart;
    [SerializeField] Sprite m_oneThirdHeart;
    [SerializeField] Sprite m_twoThirdHeart;
    [SerializeField] Sprite m_completeHeart;

    [SerializeField] Image m_firstPanelSpriteRenderer;
    [SerializeField] Image m_secPanelSpriteRenderer;
    [SerializeField] Image m_thirdPanelSpriteRendere;

    [SerializeField]float m_popedItemCycle;
    [SerializeField] GameObject m_heartBar;

    //[SerializeField] RectTransform m_firstRectTransform;
    //[SerializeField] RectTransform m_secRectTransform;
    //[SerializeField] RectTransform m_thirdRectTransform;

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


    //This function will fill customer dialogue boxes : 
    public void SetRequestText(Request customerRequest)
    {
        int numberOfContentTypes = 1;
        if (customerRequest.GetRequestData().GetData() != 0)
        {

            m_firstDialogueBox.SetActive(true);
            m_firstDialogueBoxText.text = customerRequest.GetRequestData().GetData() + " گیگ";
            m_firstDialogueBoxImage.sprite = m_dataIcon;
            numberOfContentTypes++;
        }

        if (customerRequest.GetRequestCallTime().GetCallTime() != 0)
        {

            if (numberOfContentTypes == 1)
            {
                m_firstDialogueBox.SetActive(true);
                m_firstDialogueBoxText.text = customerRequest.GetRequestCallTime().GetCallTime() + " دقیقه";
                m_firstDialogueBoxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(0.6f, 0.6f);
                m_firstDialogueBoxImage.sprite = m_callTimeIcon;
            }
            else if (numberOfContentTypes == 2)
            {
                m_secDialogueBox.SetActive(true);
                m_secDialogueBoxText.text = customerRequest.GetRequestCallTime().GetCallTime() + " دقیقه";
                m_secDialogueBoxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(0.6f, 0.6f);
                m_secDialogueBoxImage.sprite = m_callTimeIcon;
            }
            numberOfContentTypes++;
        }

        if (customerRequest.GetRequestMessage().GetMessageCount() != 0)
        {
            if (numberOfContentTypes == 1)
            {
                m_firstDialogueBox.SetActive(true);
                m_firstDialogueBoxText.text = customerRequest.GetRequestMessage().GetMessageCount() + " پیامک";
                m_firstDialogueBoxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(0.8f, 0.6f);
                m_firstDialogueBoxImage.sprite = m_messageIcon;
            }
            else if (numberOfContentTypes == 2)
            {
                m_secDialogueBox.SetActive(true);
                m_secDialogueBoxText.text = customerRequest.GetRequestMessage().GetMessageCount() + " پیامک";
                m_secDialogueBoxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(0.8f, 0.6f);
                m_secDialogueBoxImage.sprite = m_messageIcon;
            }

            else if (numberOfContentTypes == 3)
            {
                m_thirdDialogueBox.SetActive(true);
                m_thirdDialogueBoxText.text = customerRequest.GetRequestMessage().GetMessageCount() + " پیامک";
                m_thirdDialogueBoxImage.GetComponent<RectTransform>().sizeDelta = new Vector2(0.8f, 0.6f);
                m_thirdDialogueBoxImage.sprite = m_messageIcon;

            }
        }


    }

    public void UpdateCustomerHearts(float proposalValue)
    {

        if (proposalValue - 0.3f <= Mathf.Epsilon)
        {
            m_secPanelSpriteRenderer.sprite = m_zeroHeart;
            m_thirdPanelSpriteRendere.sprite = m_zeroHeart;

            if (proposalValue <= Mathf.Epsilon)
            {
                m_firstPanelSpriteRenderer.sprite = m_zeroHeart;
            }
            else if (proposalValue - 0.1f <= Mathf.Epsilon)
            {
                m_firstPanelSpriteRenderer.sprite = m_oneThirdHeart;
            }

            else if (proposalValue - 0.1f > Mathf.Epsilon && proposalValue - 0.2f <= Mathf.Epsilon)
            {
                m_firstPanelSpriteRenderer.sprite = m_twoThirdHeart;
            }
            else
            {
                m_firstPanelSpriteRenderer.sprite = m_completeHeart;
            }
        }

        else if (proposalValue - 0.3f > Mathf.Epsilon && proposalValue - 0.6f <= Mathf.Epsilon)
        {
            m_firstPanelSpriteRenderer.sprite = m_completeHeart;
            m_thirdPanelSpriteRendere.sprite = m_zeroHeart;

            if (proposalValue - 0.4f <= Mathf.Epsilon)
            {
                m_secPanelSpriteRenderer.sprite = m_oneThirdHeart;
            }

            else if (proposalValue - 0.4f > Mathf.Epsilon && proposalValue - 0.5f <= Mathf.Epsilon)
            {
                m_secPanelSpriteRenderer.sprite = m_twoThirdHeart;
            }
            else
            {
                m_secPanelSpriteRenderer.sprite = m_completeHeart;
            }

        }

        else if (proposalValue - 0.6f > Mathf.Epsilon && proposalValue - 1f <= Mathf.Epsilon)
        {
            m_firstPanelSpriteRenderer.sprite = m_completeHeart;
            m_secPanelSpriteRenderer.sprite = m_completeHeart;

            if (proposalValue - 0.7f <= Mathf.Epsilon)
            {
                m_thirdPanelSpriteRendere.sprite = m_oneThirdHeart;
            }

            else if (proposalValue - 0.7f > Mathf.Epsilon && proposalValue - 0.8f <= Mathf.Epsilon)
            {
                m_thirdPanelSpriteRendere.sprite = m_twoThirdHeart;
            }
            else
            {
                m_thirdPanelSpriteRendere.sprite = m_completeHeart;
            }
        }


    }

    public void InstantiatePopedIcon(Vector3 position,float currentProposalValue,float lastProposalValue)
    {
        if (currentProposalValue - lastProposalValue >= Mathf.Epsilon)
        {
            GameObject heart = Instantiate(m_heartObject, position, Quaternion.identity);
            heart.transform.DOMove(position + new Vector3(0, 0.5f, 0), m_popedItemCycle);
            heart.transform.DOMove(m_heartBar.transform.position, m_popedItemCycle).OnComplete(() =>UpdateCustomerHearts(currentProposalValue));
            SetHappyAnimation();
            StartCoroutine(DestroyPopedIcon(heart.gameObject, m_popedItemCycle,currentProposalValue));

        }
        else
        {
            GameObject burningMoney = Instantiate(m_burninMoneyObject, position, Quaternion.identity);
            burningMoney.transform.DOMove(position + new Vector3(0, 0.5f, 0), m_popedItemCycle);
            burningMoney.transform.DOMove(m_heartBar.transform.position, m_popedItemCycle).OnComplete(() => UpdateCustomerHearts(currentProposalValue));
            StartCoroutine(DestroyPopedIcon(burningMoney.gameObject, m_popedItemCycle,currentProposalValue));

        }
    }

    IEnumerator DestroyPopedIcon(GameObject uiObject, float delay,float currentProposalValue)
    {
        yield return new WaitForSeconds(delay);
        Destroy(uiObject);
        if (0.9f - currentProposalValue <= Mathf.Epsilon)
        {
            SendButton.m_instance.ShakeButton();
            SendButton.m_instance.ChangeColor();
            SetHappyAnimation();
        }
        else
        {
            SendButton.m_instance.ResetButton();
        }

    }

}
