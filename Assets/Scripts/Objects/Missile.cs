using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Missile launched by the final boss.
/// </summary>
public class Missile : MonoBehaviour
{
    float speed = 25;

    void OnEnable()
    {
        StartCoroutine(DestroyMissile());
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    /// <summary>
    /// Function we call when a trigger collision occurs.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Hurt(1);
            GameObject explosion = ObjectPooler.SharedInstance.GetPooledObject("Explosion");
            if (explosion != null)
            {
                explosion.SetActive(true);
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
            }
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Coroutine where the missile is destroyed.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyMissile()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }

}
