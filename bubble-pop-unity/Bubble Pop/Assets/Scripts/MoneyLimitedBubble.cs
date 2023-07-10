using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    class MoneyLimitedBubble : MonoBehaviour, LevelGoal
    {

        public static MoneyLimitedBubble m_instance;

        [SerializeField] float m_moneyGoal;
        [SerializeField] int m_numOfBubbles;
        [SerializeField] GameObject m_resultMenu;
        [SerializeField] GameObject m_goal;
        [SerializeField] GameObject m_restartButton;
        [SerializeField] GameObject m_continueButton;
        [SerializeField] GameObject m_QuitButton;
        [SerializeField] TMP_Text m_goalText;
        [SerializeField] TMP_Text m_resultText;
        [SerializeField] GameObject m_limitedBubblesUi;
        [SerializeField] TMP_Text m_limitedBubbleText;

        private void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            Time.timeScale = 1.0f;
            Debug.Log("From pause to unpause");
        }


        private void Start()
        {
            UpdateNumOfBubbles();
            UpdateMadeMoneyUi();
        }
        private void Update()
        {
            UpdateNumOfBubbles();
            
        }
        
        public void UpdateMadeMoneyUi()
        {
            m_goal.SetActive(true);
            m_goalText.text = LevelManager.m_instance.GetMadeMoney() + "/" + $"{m_moneyGoal}" + " Coins";

        }

        public void UpdateNumOfBubbles()
        {
            m_limitedBubblesUi.SetActive(true);
            int remainingBubbles = (m_numOfBubbles - LevelManager.m_instance.GetNumberOfPopedBubbles());
            m_limitedBubbleText.text = remainingBubbles + "\nBubbles";
            if (remainingBubbles <= 0)
            {
                ShowResultMenu();
            }
        }

        public float GetLevelRate()
        {
            throw new System.NotImplementedException();
        }

        public bool IsGoalReached()
        {
            if (LevelManager.m_instance.GetMadeMoney() >= m_moneyGoal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ShowResultMenu()
        {
            Time.timeScale = 0.0f;
            m_resultMenu.SetActive(true);
            if (IsGoalReached())
            {
                m_resultMenu.GetComponentInChildren<TMP_Text>().text = "Passed";
                m_continueButton.SetActive(true);
                m_restartButton.SetActive(false);
            }
            else
            {
                m_resultMenu.GetComponentInChildren<TMP_Text>().text = "Failed";
                m_restartButton.SetActive(true);
                m_continueButton.SetActive(false);
            }
            m_QuitButton.SetActive(true);

        }
    }
}
