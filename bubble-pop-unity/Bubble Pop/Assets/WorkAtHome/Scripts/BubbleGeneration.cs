using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.WorkAtHome.Scripts
{
    class BubbleGeneration : MonoBehaviour, ISequence
    {

        private bool m_isPopingEnable = false;
        [SerializeField] int m_numOfBubbles;
        [SerializeField] GameObject m_bubblePrefab;
        [SerializeField] List<Transform> m_bubbleTransfroms;
        [SerializeField] List<GameObject> m_generatedBubbles;
        public void ChangeSequence()
        {
            throw new NotImplementedException();
        }

        public void PlaySequence()
        {
            GenerateBubbles();
        }

        public void GenerateBubbles()
        {
            for(int i = 0; i <m_numOfBubbles; i++)
            {
                GameObject generatedObject = Instantiate(m_bubblePrefab,m_bubbleTransfroms[i].position,Quaternion.identity);
                m_generatedBubbles.Add(generatedObject);
            }
        }

        private void Update()
        {
            if (m_isPopingEnable)
            {
                CheckInput();
            }
        }

        private void CheckInput()
        {

        }
    }
}
