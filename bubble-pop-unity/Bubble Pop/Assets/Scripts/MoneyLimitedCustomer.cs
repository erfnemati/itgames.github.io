using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RTLTMPro;

namespace Assets.Scripts
{
    class MoneyLimitedCustomer : MonoBehaviour, LevelGoal
    {

        public static MoneyLimitedCustomer m_instance = null;

        [SerializeField] int m_numOfHearts;
        [SerializeField] int m_oneStarHearts;
        [SerializeField] int m_twoStarHearts;
        [SerializeField] int m_threeStarHearts;
        [SerializeField] int m_numOfCustomers;
        [SerializeField] Image m_firstStar;
        [SerializeField] Image m_secStar;
        [SerializeField] Image m_thirdStar;
        [SerializeField] GameObject m_grayBackground;
        [SerializeField] GameObject m_resultMenu;
        [SerializeField] GameObject m_goal;
        [SerializeField] GameObject m_restartButton;
        [SerializeField] GameObject m_continueButton;
        [SerializeField] GameObject m_QuitButton;
        [SerializeField] RTLTextMeshPro m_goalText;
        [SerializeField] TMP_Text m_resultText;
        [SerializeField] GameObject m_limitedCustomerUi;
        [SerializeField] RTLTextMeshPro m_limitedCustomerText;

        private void Awake()
        {
            Time.timeScale = 1.0f;
        }
        private void Start()
        {
            
            
            if (m_instance != null && m_instance != this)
            {
                Destroy(this.gameObject);
            }

            else
            {
                m_instance = this;
            }

            
            UpdateNumberOfCustomers();
            UpdateGoalUi();
        }
        public void UpdateGoalUi()
        {
            m_goal.SetActive(true);
            //m_goalText.text = LevelManager.m_instance.GetRecievedHearts() + "/" + $"{m_oneStarHearts}" ;
            m_goalText.text = $"{m_oneStarHearts}" + " / " + LevelManager.m_instance.GetRecievedHearts();

        }

        public void UpdateNumberOfCustomers()
        {
            m_numOfCustomers--;
            m_limitedCustomerUi.SetActive(true);
            m_limitedCustomerText.text = m_numOfCustomers + "";
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
            if (LevelManager.m_instance.GetRecievedHearts() >= m_numOfHearts)
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

            else if (m_numOfHearts >= m_twoStarHearts && m_numOfHearts <m_threeStarHearts)
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

        public void UpdateRecievedHearts(int numberOfhearts)
        {
            m_numOfHearts = numberOfhearts;
            UpdateGoalUi();
        }
    }
}
