
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;


namespace Assets.Scripts
{
    public class CustomerManager : MonoBehaviour
    {
        Request m_customerRequest;
        Proposal m_currentProposal = new Proposal();

        [SerializeField] Sprite m_dataIcon;
        [SerializeField] Sprite m_callTimeIcon;
        [SerializeField] Sprite m_messageIcon;

        [SerializeField] GameObject m_firstDialogueBox;
        [SerializeField] TMP_Text m_firstDialogueBoxText;
        [SerializeField] Image m_firstDialogueBoxImage;
        
        

        [SerializeField] GameObject m_secDialogueBox;
        [SerializeField] TMP_Text m_secDialogueBoxText;
        [SerializeField] Image m_secDialogueBoxImage;
        

        [SerializeField] GameObject m_thirdDialogueBox;
        [SerializeField] TMP_Text m_thirdDialogueBoxText;
        [SerializeField] Image m_thirdDialogueBoxImage;

        [SerializeField] CustomerSlider m_customerSlider;
        [SerializeField] TMP_Text m_requestValue;
        [SerializeField] GameObject m_heartObject;
        [SerializeField] GameObject m_burninMoneyObject;
        [SerializeField] GameObject m_sendButton;

        private float m_lastValue = 0.0f;
        private float m_currentValue = 0.0f;

        void Start()
        {
            
            Invoke(nameof(SetRequest), 0.2f);
        }

        private void SetRequest()
        {
            m_customerRequest = RequestManager.m_instance.GetNewRequest();
            SetRequestText();
            SetRequestValueUi();
        }

        public void AddItem(Bubble bubble)
        {
            m_currentProposal.AddBubble(bubble);
            Vector3 position = bubble.transform.position;
            
            

            m_lastValue = m_currentValue;
            m_currentValue = CheckProposal();

            if (m_currentValue - m_lastValue >= Mathf.Epsilon)
            {
                GameObject heart = Instantiate(m_heartObject, position, Quaternion.identity);
                heart.transform.DOMove(position + new Vector3(0, 0.5f, 0), 0.5f);
                heart.transform.DOMove(m_customerSlider.transform.position, 0.5f).OnComplete(() => UpdateCustomerCompletionBar(m_currentValue));
                StartCoroutine(DestroyUiGameObject(heart.gameObject, 0.5f));
                
            }
            else
            {
                GameObject burningMoney = Instantiate(m_burninMoneyObject, position, Quaternion.identity);
                burningMoney.transform.DOMove(position + new Vector3(0, 0.5f, 0), 0.5f);
                burningMoney.transform.DOMove(m_customerSlider.transform.position, 0.5f).OnComplete(() => UpdateCustomerCompletionBar(m_currentValue));
                StartCoroutine(DestroyUiGameObject(burningMoney.gameObject, 0.5f));

            }

        }

        IEnumerator DestroyUiGameObject(GameObject uiObject,float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(uiObject);
            if (1 - m_currentValue <= Mathf.Epsilon)
            {
                SendButton.m_instance.ShakeButton();
            }

        }

        private float CheckProposal()
        {
            if (m_customerRequest == null)
            {
                return 0;
            }

            float dataPoint = 0;
            float callTimePoint = 0;
            float messagePoint = 0;
            int weight = 0;

            GetProposalDataPoint(ref dataPoint, ref weight);
            Debug.Log("Data point is : " + dataPoint);
            GetProposalMessagePoint(ref messagePoint, ref weight);
            Debug.Log("Message point is : " + messagePoint);
            GetProposalCallTimePoint(ref callTimePoint, ref weight);
            Debug.Log("Call time point is : " + callTimePoint);

            float proposalPoint = (callTimePoint + messagePoint + dataPoint) / weight;
            if(proposalPoint <= Mathf.Epsilon)
            {
                proposalPoint = 0;
            }

            Debug.Log("Proposal point is : " + proposalPoint);
            return proposalPoint;
        }

        private void GetProposalCallTimePoint(ref float callTimePoint, ref int weight)
        {
            float requestCallTime = m_customerRequest.GetRequestCallTime().GetCallTime();
            if (m_currentProposal.GetProposalCallTime().GetCallTime() != 0 || requestCallTime != 0)
            {
                weight++;
                float diffCallTime = Mathf.Abs(m_currentProposal.GetProposalCallTime().GetCallTime() - requestCallTime);

                if (diffCallTime < Mathf.Epsilon)
                {
                    callTimePoint = 1;
                }
                else
                {
                    callTimePoint = 1 - (Mathf.Abs(diffCallTime) / requestCallTime);
                    if (callTimePoint  <= Mathf.Epsilon)
                    {
                        callTimePoint = 0f;
                    }
                }
            }
        }

        private void GetProposalMessagePoint(ref float messagePoint, ref int weight)
        {
            float requestMessage = m_customerRequest.GetRequestMessage().GetMessageCount();
            if (m_currentProposal.GetProposalMessage().GetMessageCount() != 0 || requestMessage != 0)
            {
                weight++;
                float diffMessage = Mathf.Abs(m_currentProposal.GetProposalMessage().GetMessageCount() - requestMessage);
                if (diffMessage < Mathf.Epsilon)
                {
                    messagePoint = 1;
                }
                else
                {
                    messagePoint = 1 - Mathf.Abs(diffMessage / requestMessage);
                    if (messagePoint  <= Mathf.Epsilon)
                    {
                        messagePoint = 0f;
                    }
                }
            }
        }

        private void GetProposalDataPoint(ref float dataPoint, ref int weight)
        {
            float requestData = m_customerRequest.GetRequestData().GetData();
            if (m_currentProposal.GetProposalData().GetData() != 0 || requestData != 0)
            {
                weight++;
                float diffData = Mathf.Abs(m_currentProposal.GetProposalData().GetData() - requestData);

                if (diffData < Mathf.Epsilon)
                {
                    dataPoint = 1;
                }
                else
                {

                    dataPoint = 1 - Mathf.Abs(diffData / requestData);
                    if (dataPoint  <= Mathf.Epsilon)
                    {
                        dataPoint = 0f;
                    }
                }
            }
        }

        public int GetHearts()
        {
            if (m_customerRequest == null)
            {
                return 0;
            }
            float rateOfProposal = CheckProposal();
            float recievedHearts = rateOfProposal * m_customerRequest.GetRequestValue();
            int roundedHearts = 0;
            if (recievedHearts - 0.5f <= Mathf.Epsilon)
            {
                roundedHearts = 0;
            }

            else if (recievedHearts - 0.5f > Mathf.Epsilon && recievedHearts - 1.5f < Mathf.Epsilon)
            {
                roundedHearts = 1;
            }

            else if (recievedHearts - 1.5f >= Mathf.Epsilon && recievedHearts - 2.3 < Mathf.Epsilon)
            {
                roundedHearts = 2;
            }
            else
            {
                roundedHearts = 3;
            }
            return roundedHearts;
        }
        

  
        private void SetRequestText()
        {
            int numberOfContentTypes = 1;
            if (m_customerRequest.GetRequestData().GetData() != 0)
            {
                
                m_firstDialogueBox.SetActive(true);
                m_firstDialogueBoxText.text = m_customerRequest.GetRequestData().GetData() + "\nGIG";
                m_firstDialogueBoxImage.sprite = m_dataIcon;
                numberOfContentTypes++;
            }

            if (m_customerRequest.GetRequestCallTime().GetCallTime() != 0)
            {
                
                if(numberOfContentTypes == 1)
                {
                    m_firstDialogueBox.SetActive(true);
                    m_firstDialogueBoxText.text = m_customerRequest.GetRequestCallTime().GetCallTime() + "\nMins";
                    m_firstDialogueBoxImage.sprite = m_callTimeIcon;
                }
                else if (numberOfContentTypes == 2)
                {
                    m_secDialogueBox.SetActive(true);
                    m_secDialogueBoxText.text = m_customerRequest.GetRequestCallTime().GetCallTime() + "\nMins";
                    m_secDialogueBoxImage.sprite = m_callTimeIcon;
                }
                numberOfContentTypes++;
            }

            if (m_customerRequest.GetRequestMessage().GetMessageCount() != 0)
            {
                if (numberOfContentTypes == 1)
                {
                    m_firstDialogueBox.SetActive(true);
                    m_firstDialogueBoxText.text = m_customerRequest.GetRequestMessage().GetMessageCount() + "\nMessage";
                    m_firstDialogueBoxImage.sprite = m_messageIcon;
                }
                else if (numberOfContentTypes == 2)
                {
                    m_secDialogueBox.SetActive(true);
                    m_secDialogueBoxText.text = m_customerRequest.GetRequestMessage().GetMessageCount() + "\nMessage";
                    m_secDialogueBoxImage.sprite = m_messageIcon;
                }

                else if (numberOfContentTypes == 3)
                {
                    m_thirdDialogueBox.SetActive(true);
                    m_thirdDialogueBoxText.text = m_customerRequest.GetRequestMessage().GetMessageCount() + "\nMessage";
                    m_thirdDialogueBoxImage.sprite = m_messageIcon;

                }
            }


        }

        private void SetRequestValueUi()
        {
            
            //m_requestValue.text =  m_customerRequest.GetRequestValue() + " Coins";
        }

        private void UpdateCustomerCompletionBar(float newSliderValue)
        {
            
           m_customerSlider.SetValue(newSliderValue);
            Debug.Log("I am updating");
         
        }
    }
}
