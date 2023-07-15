using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    class MoneyLimitedTime : MonoBehaviour, LevelGoal
    {

        public static MoneyLimitedTime m_instance;
        [SerializeField] float m_moneyGoal;
        [SerializeField] float m_initialTime;
        [SerializeField] GameObject m_resultMenu;
        [SerializeField] GameObject m_grayScreen;
        [SerializeField] GameObject m_timer;
        [SerializeField] GameObject m_goal;
        [SerializeField] GameObject m_restartButton;
        [SerializeField] GameObject m_continueButton;
        [SerializeField] GameObject m_QuitButton;
        [SerializeField] TMP_Text m_goalText;
        [SerializeField] TMP_Text m_resultText;
        [SerializeField] TMP_Text m_timerText;
        
        private float m_remainingTime = 0.0f;
        private bool m_isLevelOver = false;

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

            m_remainingTime = m_initialTime;
            Time.timeScale = 1.0f;
            Debug.Log("From pause to unpause");
        }

        private void Start()
        {
            UpdateMadeMoneyUi();
        }



        private void Update()
        {
            if (m_isLevelOver)
                return;
            if (m_remainingTime <= Mathf.Epsilon )
            {
                LevelManager.m_instance.FinishLevel();
                m_isLevelOver = true;
                ShowResultMenu();
                return;
            }
            UpdateRemainingTime();
        }

        public void UpdateMadeMoneyUi()
        {
            m_goal.SetActive(true);
            //m_goalText.text = LevelManager.m_instance.GetMadeMoney() + "Coins";
            m_goalText.text = LevelManager.m_instance.GetMadeMoney() + "/" + $"{m_moneyGoal}" + " Coins";

        }

        private void UpdateRemainingTime()
        {
            m_remainingTime -= Time.deltaTime;
            UpdateUiTimer();
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

        private void UpdateUiTimer()
        {
            
            m_timerText.text = ((int)m_remainingTime).ToString() + "\nSeconds";
        }

        public void ShowResultMenu()
        {
            Time.timeScale = 0.0f;
            InputHandler.m_instance.SwitchIsPaused();
            m_grayScreen.SetActive(true);
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
