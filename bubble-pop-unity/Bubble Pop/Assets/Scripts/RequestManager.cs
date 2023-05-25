using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class RequestManager:MonoBehaviour
    {
        public static RequestManager m_instance;
        
      

        private void Start()
        {
            if (m_instance == null)
            {
                m_instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
           
        }

        public Request GetNewRequest()
        {
            return new Request();
        }

        private Request MakeRequest()
        {
            return null;
        }

    }
}
