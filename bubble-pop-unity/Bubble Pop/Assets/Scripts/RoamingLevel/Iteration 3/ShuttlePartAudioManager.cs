using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlePartAudioManager : MonoBehaviour
{
    [SerializeField] AudioSource m_ShuttlePartAudioSource;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shuttle"))
        {
            m_ShuttlePartAudioSource.Play();
        }
    }
}
