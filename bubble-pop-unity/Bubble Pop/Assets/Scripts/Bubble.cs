using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour
    {
        PackageContent content;


        [SerializeField] TMP_Text m_text;
        // Start is called before the first frame update
        void Start()
        {
            SetPackageText();
        }

      
        private void SetPackageText()
        {
            string dataText;
            string callTimeText;
            string messagesCount;
            string packageContent;
            if (content.getPackageData() - 1 < float.Epsilon)
            {
                dataText = (content.getPackageData() * 1000) + "MB";
            }
            else
            {
                dataText = content.getPackageData() + "GB";
            }

            callTimeText = content.getPackageCallTime() + "Minutes";
            messagesCount = content.getPackageMessages() + "Message";

            packageContent = dataText + callTimeText + messagesCount;

            m_text.text = packageContent;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
