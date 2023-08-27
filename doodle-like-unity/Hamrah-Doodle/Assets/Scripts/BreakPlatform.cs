using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    [SerializeField] float jumpForce = 10.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null && playerRb.velocity.y < 0)
            {
                Debug.Log("It has broken!");
                playerRb.velocity = Vector2.zero;
                playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Destroy(this.gameObject);
            }
        }
    }
}
