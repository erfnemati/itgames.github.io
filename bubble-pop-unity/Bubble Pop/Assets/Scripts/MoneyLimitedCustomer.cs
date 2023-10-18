using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RTLTMPro;

namespace Assets.Scripts
{
    class MoneyLimitedCustomer : MonoBehaviour, LevelGoal
    {

        public static MoneyLimitedCustomer m_instance = null;

        [SerializeField] GameObject starbar;
        [SerializeField] Image sbi1, sbi2, sbi3, sbi4;
        [SerializeField] int m_numOfHearts;
        [SerializeField] int m_oneStarHearts;
        [SerializeField] int m_twoStarHearts;
        [SerializeField] int m_threeStarHearts;
        [SerializeField] int m_numOfCustomers;
        [SerializeField] GameObject m_firstStar;
        [SerializeField] GameObject m_secStar;
        [SerializeField] GameObject m_thirdStar;
        [SerializeField] GameObject m_grayBackground;
        [SerializeField] GameObject m_resultMenu;
       // [SerializeField] GameObject m_goal;
        [SerializeField] GameObject m_restartButton;
        [SerializeField] GameObject m_continueButton;
        [SerializeField] GameObject m_QuitButton;
       // [SerializeField] RTLTextMeshPro m_goalText;
        [SerializeField] RTLTextMeshPro m_resultText;
        //  [SerializeField] GameObject m_limitedCustomerUi;
        //  [SerializeField] RTLTextMeshPro m_limitedCustomerText;
        public bool fstar=false, secstar=false, thirdstar=false;
        private void Awake()
        {
            Time.timeScale = 1.0f;
        }
        private void Start()
        {
            starbar.GetComponent<Image>().sprite = sbi1.sprite; 
            
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
            if (m_numOfHearts >= 8 && m_numOfHearts < 12)
            {
                starbar.GetComponent<Image>().sprite = sbi2.sprite;
                fstar = true;
                secstar = false;
                thirdstar = false;
            }
            else if (m_numOfHearts >= 12 && m_numOfHearts < 16)
            {
                starbar.GetComponent<Image>().sprite = sbi3.sprite;
                fstar = false;
                secstar = true;
                thirdstar = false;
            }
            else if (m_numOfHearts >= 16)
            {
                starbar.GetComponent<Image>().sprite = sbi4.sprite;
                fstar = false;
                secstar = false;
                thirdstar = true;
            }
           // m_goal.SetActive(true);
            //m_goalText.text = LevelManager.m_instance.GetRecievedHearts() + "/" + $"{m_oneStarHearts}" ;
           // m_goalText.text = $"{m_oneStarHearts}" + " / " + LevelManager.m_instance.GetRecievedHearts();

        }

        public void UpdateNumberOfCustomers()
        {
            m_numOfCustomers--;
           // m_limitedCustomerUi.SetActive(true);
           // m_limitedCustomerText.text = m_numOfCustomers + "";
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
                //sssssssss
                m_resultText.gameObject.SetActive(true);
                m_resultMenu.GetComponentInChildren<RTLTextMeshPro>().text = "شروع مجدد";
                m_restartButton.SetActive(true);
                m_continueButton.SetActive(false);
                m_QuitButton.SetActive(true);
                return;
            }

            
            else if (m_numOfHearts >= m_oneStarHearts && m_numOfHearts < m_twoStarHearts)
            {
                Debug.Log("One star");
                starbar.GetComponent<Image>().sprite = sbi2.sprite;
                m_firstStar.SetActive(true);

                // starbar.sprite = sbi2.sprite;
                //  m_firstStar.gameObject.SetActive(true);
            }

            else if (m_numOfHearts >= m_twoStarHearts && m_numOfHearts <m_threeStarHearts)
            {
                Debug.Log("Two star");
                starbar.GetComponent<Image>().sprite = sbi3.sprite;
                m_firstStar.SetActive(true);
                m_secStar.SetActive(true);
                //  starbar.sprite = sbi3.sprite;
                //m_firstStar.gameObject.SetActive(true);
                //m_secStar.gameObject.SetActive(true);

            }

            else if (m_numOfHearts >= m_threeStarHearts)
            {
                Debug.Log("Three star");
                starbar.GetComponent<Image>().sprite = sbi4.sprite;
                m_firstStar.SetActive(true);
                m_secStar.SetActive(true);
                m_thirdStar.SetActive(true);
                //starbar.sprite = sbi4.sprite;
                //m_firstStar.gameObject.SetActive(true);
                //m_secStar.gameObject.SetActive(true);
                //m_thirdStar.gameObject.SetActive(true);
            }

            m_resultMenu.GetComponentInChildren<RTLTextMeshPro>().text = "Passed";
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
