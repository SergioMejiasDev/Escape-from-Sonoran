using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that will be assigned the bullets, both the player's and the enemies'.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 0;
    [SerializeField] int damage = 0;
    [SerializeField] bool destroyOnGround = true;
    [SerializeField] bool enemyBullet = false;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);       
    }

    /// <summary>
    /// Function we call if the bullet has a trigger collision.
    /// </summary>
    /// <param name="collision">The object that it collides with.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyOnGround)
        {
            if ((collision.gameObject.CompareTag("Ground")) || (collision.gameObject.CompareTag("MovingPlatform")) || (collision.gameObject.CompareTag("BoxTrap")))
            {
                gameObject.SetActive(false);
            }
        }
        
        if (enemyBullet)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().Hurt(damage);
                
                gameObject.SetActive(false);
            }
        }

        if (!enemyBullet)
        {
            if (collision.gameObject.CompareTag("Borders"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
