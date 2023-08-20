using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleStarter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("InnerHandler"))
        {
            Invoke(nameof(StartShuttle),0.5f);
        }
    }

    private void StartShuttle()
    {
        ShuttleController._instance.StartShuttle();
    }
}
