using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringTrigger : MonoBehaviour
{
    [SerializeField] float jumpForce = 100.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Debug.Log("Spring activated");
                playerRb.velocity = Vector2.zero;
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
