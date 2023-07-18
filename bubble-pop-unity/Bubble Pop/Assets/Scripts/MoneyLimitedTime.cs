using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class MoneyLimitedTime : MonoBehaviour, LevelGoal
    {

        public static MoneyLimitedTime m_instance;

        [SerializeField] int m_numOfHearts;
        [SerializeField] int m_oneStarHearts;
        [SerializeField] int m_twoStarHearts;
        [SerializeField] int m_threeStarHearts;
        [SerializeField] float m_initialTime;
        [SerializeField] Image m_firstStar;
        [SerializeField] Image m_secStar;
        [SerializeField] Image m_thirdStar;
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
            UpdateRecievedHearts();
        }



        private void Update()
        {
            if (m_isLevelOver)
                return;
            if (m_remainingTime <= Mathf.Epsilon )
            {
                m_isLevelOver = true;
                ShowResultMenu();
                return;
            }
            UpdateRemainingTime();
        }

        public void UpdateRecievedHearts()
        {
            m_goal.SetActive(true);
            m_goalText.text = LevelManager.m_instance.GetRecievedHearts() + "/" + $"{m_oneStarHearts}";

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
            if (LevelManager.m_instance.GetRecievedHearts() >= m_oneStarHearts)
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
            
            m_limitedCustomerText.text = ((int)m_remainingTime).ToString();
        }

        public void ShowResultMenu()
        {
            Time.timeScale = 0.0f;
            InputHandler.m_instance.SwitchIsPaused();
            m_grayBackground.SetActive(true);
            m_resultMenu.SetActive(true);

            if (m_numOfHearts < m_oneStarHearts)
            {
                m_resultMenu.GetComponentInChildren<TMP_Text>().text = "Failed";
                m_restartButton.SetActive(true);
                m_continueButton.SetActive(false);
                m_QuitButton.SetActive(true);
                return;
            }


            else if (m_numOfHearts >= m_oneStarHearts && m_numOfHearts < m_twoStarHearts)
            {
                Debug.Log("One star");
                m_firstStar.gameObject.SetActive(true);
            }

            else if (m_numOfHearts >= m_twoStarHearts && m_numOfHearts < m_threeStarHearts)
            {
                Debug.Log("Two star");
                m_firstStar.gameObject.SetActive(true);
                m_secStar.gameObject.SetActive(true);

            }

            else if (m_numOfHearts >= m_threeStarHearts)
            {
                Debug.Log("Three star");
                m_firstStar.gameObject.SetActive(true);
                m_secStar.gameObject.SetActive(true);
                m_thirdStar.gameObject.SetActive(true);
            }

            m_resultMenu.GetComponentInChildren<TMP_Text>().text = "Passed";
            m_restartButton.SetActive(false);
            m_continueButton.SetActive(true);
            m_QuitButton.SetActive(true);



        }

        public void UpdateRecievedHearts(int numberfHearts)
        {
            m_numOfHearts = numberfHearts;
        }
    }
}
