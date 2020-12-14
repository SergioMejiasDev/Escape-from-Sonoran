using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Automatic turrets script.
/// </summary>
public class Turret : MonoBehaviour
{
    GameObject bullet;
    [SerializeField] Transform shootPoint = null;
    float timeLastShoot;
    public float cadency;

    void Update()
    {
        if (Time.time - timeLastShoot > cadency)
        {
            timeLastShoot = Time.time;

            bullet = ObjectPooler.SharedInstance.GetPooledObject("BulletEnemy");

            if (bullet != null)
            {
                bullet.transform.position = shootPoint.position;
                bullet.transform.rotation = shootPoint.rotation;
                bullet.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Function we call when a trigger collision occurs.
    /// </summary>
    /// <param name="collision">Object with which it collides.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
