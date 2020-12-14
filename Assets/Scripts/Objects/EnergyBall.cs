using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Final Boss energy balls script.
/// </summary>
public class EnergyBall : MonoBehaviour
{
    float speed = 10;

    void OnEnable()
    {
        StartCoroutine(DestroyBall());
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);      
    }

    /// <summary>
    /// Function we call when a collision occurs.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletPlayer"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Boss3Manager.boss3Manager.StartExplosion(true);
        }
    }

    /// <summary>
    /// Coroutine where the ball is destroyed after a few seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
    }
}
