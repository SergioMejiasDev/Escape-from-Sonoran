using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script of the switches that open doors.
/// </summary>
public class Switch : MonoBehaviour
{
    [SerializeField] GameObject offSwitch = null;
    [SerializeField] GameObject door = null;
    [SerializeField] bool restart = false;
    AudioSource audioSource;
    AudioSource doorAudioSource;
    BoxCollider2D switchCollider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        doorAudioSource = door.GetComponent<AudioSource>();
        switchCollider = GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Function we call when there is a trigger collision.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(SwitchOn());
        }
    }

    /// <summary>
    /// Coroutine where the switch is activated and opens the door.
    /// </summary>
    /// <returns></returns>
    IEnumerator SwitchOn()
    {
        offSwitch.SetActive(false);
        switchCollider.enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        doorAudioSource.Play();
        yield return new WaitForSeconds(1);
        door.SetActive(false);
        
        if (restart)
        {
            StartCoroutine(SwitchRestart());
        }
    }

    /// <summary>
    /// Coroutine where the switch and door are reset.
    /// </summary>
    /// <returns></returns>
    IEnumerator SwitchRestart()
    {
        yield return new WaitForSeconds(2);
        door.SetActive(true);
        doorAudioSource.Play();
        yield return new WaitForSeconds(2);
        offSwitch.SetActive(true);
        audioSource.Play();
        switchCollider.enabled = true;
    }
}
