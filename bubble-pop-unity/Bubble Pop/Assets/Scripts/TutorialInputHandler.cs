using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInputHandler : MonoBehaviour
{
    [SerializeField] const string ANAR_POWER_UP_TAG = "AnarPowerUp";
    private void Update()
    {
        if (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Collider2D selectedObject = Physics2D.OverlapPoint(new Vector2(worldPos.x, worldPos.y));
            if (selectedObject.CompareTag(ANAR_POWER_UP_TAG))
            {
                Debug.Log("Anar here guys");
            }
        }
        
    }
}
