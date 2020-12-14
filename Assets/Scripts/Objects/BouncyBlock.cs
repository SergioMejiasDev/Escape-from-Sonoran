using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script of the places where the enemy can bounce.
/// </summary>
public class BouncyBlock : MonoBehaviour
{
    AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Function that we call when there is a collision on the object.
    /// </summary>
    /// <param name="collision">Object with which it collides.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}
