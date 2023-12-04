using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithPlatform : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided");
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null && playerRb.velocity.y < 0)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
            }
        }
    }
}
