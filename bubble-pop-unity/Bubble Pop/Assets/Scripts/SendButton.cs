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
            string coins = LevelManager.m_instance.GetCustomerCoinText();
            coinText.text = coins;
        }

    }
}
