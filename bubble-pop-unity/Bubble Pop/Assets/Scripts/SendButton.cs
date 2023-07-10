using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class SendButton : MonoBehaviour
    {
        [SerializeField] TMP_Text coinText;

        public void SendCustomer()
        {
            LevelManager.m_instance.SendProposal();
        }

        public void SetCoinText()
        {
            string coins = LevelManager.m_instance.GetLastCustomerEarnedCoins();
            coinText.text = coins;
        }

    }
}
