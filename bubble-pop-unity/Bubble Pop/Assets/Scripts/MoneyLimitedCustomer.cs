using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    class MoneyLimitedCustomer : MonoBehaviour, LevelGoal
    {

        public static MoneyLimitedCustomer m_instance;

        [SerializeField] float m_moneyGoal;
        [SerializeField] int m_numOfCustomers;
        [SerializeField] GameObject m_grayBackground;
        [SerializeField] GameObject m_resultMenu;
        [SerializeField] GameObject m_goal;
        [SerializeField] GameObject m_restartButton;
        [SerializeField] GameObject m_continueButton;
        [SerializeField] GameObject m_QuitButton;
        [SerializeField] TMP_Text m_goalText;
        [SerializeField] TMP_Text m_resultText;
        [SerializeField] GameObject m_limitedCustomerUi;
        [SerializeField] TMP_Text m_limitedCustomerText;
        

        private float m_remainingTime = 0.0f;

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
            UpdateNumberOfCustomers();
            UpdateMadeMoneyUi();
        }
        private void Update()
        {
            
        }

        public void UpdateMadeMoneyUi()
        {
            m_goal.SetActive(true);
            m_goalText.text = LevelManager.m_instance.GetMadeMoney() + "/" + $"{m_moneyGoal}" + " Coins";

        }

        public void UpdateNumberOfCustomers()
        {
            m_numOfCustomers--;
            m_limitedCustomerUi.SetActive(true);
            m_limitedCustomerText.text = m_numOfCustomers + "\nCustomers";
            if (m_numOfCustomers <= 0)
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
            InputHandler.m_instance.SwitchIsPaused();
            m_grayBackground.SetActive(true);
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
