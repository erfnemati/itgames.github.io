using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FuelBubble"))
        {
            RoamingInputHandler._instance.SetIsDragging(false);
            FindAnyObjectByType<RoamingGameManager>().ReloadState();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
