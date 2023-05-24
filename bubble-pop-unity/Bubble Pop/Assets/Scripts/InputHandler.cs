using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        private void Update()
        {
            if (Input.touchCount != 0  && Input.touches[0].phase == TouchPhase.Began)
            {
                
                Vector3 worldTouchPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Collider2D selectedObject = Physics2D.OverlapPoint(new Vector2(worldTouchPos.x,worldTouchPos.y));
                if (selectedObject != null && selectedObject.CompareTag("Bubble"))
                {
                    selectedObject.GetComponent<Bubble>().Pop();
                }
            }
        }

    }
}
