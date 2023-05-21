using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Assets.Scripts
{
    public class Bubble : MonoBehaviour
    {
        PackageContent m_content;


        [SerializeField] TMP_Text m_text;
        // Start is called before the first frame update
        void Start()
        {
            m_content = new PackageContent();
            Debug.Log(m_content.getPackageData());
            SetPackageText();
        }

      
        private void SetPackageText()
        {
            string dataText;
            string callTimeText;
            string messagesCount;
            string packageContent;
            if (m_content.getPackageData() - 1 < float.Epsilon)
            {
                dataText = (m_content.getPackageData() * 1000) + "MB";
            }
            else
            {
                dataText = m_content.getPackageData() + "GB\n";
            }

            callTimeText = m_content.getPackageCallTime() + "Mins\n";
            messagesCount = m_content.getPackageMessages() + "SMS";

            packageContent = dataText + callTimeText + messagesCount;

            m_text.text = packageContent;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
