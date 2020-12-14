using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script of the explosions that are generated when the player or the enemies die.
/// </summary>
public class Explosion : MonoBehaviour
{
    AudioSource audioSource;

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        StartCoroutine(DestroyExplosion());
    }

    /// <summary>
    /// Corroutine where the explosion is destroyed.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyExplosion()
    {
        yield return new WaitForSeconds(0.7f);
        gameObject.SetActive(false);
    }
}
