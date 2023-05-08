using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    const string m_platformTag = "Platform";

    Rigidbody2D playerRb;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(m_platformTag))
        {
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }
}
