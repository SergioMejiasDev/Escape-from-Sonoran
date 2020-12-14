using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Explosive boxes script.
/// </summary>
public class TNT : MonoBehaviour
{
    GameObject explosion;

    /// <summary>
    /// Function called when a collision occurs.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            explosion = ObjectPooler.SharedInstance.GetPooledObject("Explosion");
            if (explosion != null)
            {
                explosion.SetActive(true);
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
            }
            Boss2Manager.boss2Manager.RespawnTNT();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            explosion = ObjectPooler.SharedInstance.GetPooledObject("Explosion");
            if (explosion != null)
            {
                explosion.SetActive(true);
                explosion.transform.position = transform.position;
                explosion.transform.rotation = transform.rotation;
            }
            Boss2Manager.boss2Manager.RespawnTNT();
            collision.gameObject.GetComponent<Dog>().Hurt(1);
            Destroy(gameObject);
        }
    }
}
