using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script associated with the type 3 enemy (yellow color, flies and shoots).
/// </summary>
public class EnemyClass3 : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] float speed = 4;
    public int direction = 1;
    [SerializeField] bool dontMove = false;
    Vector3 startingPosition;
    [SerializeField] float movementDistance = 100;

    [Header("Attack")]
    [SerializeField] float attackRange = 0.0f;
    GameObject player;
    GameObject bullet;
    [SerializeField] GameObject armLeft = null;
    [SerializeField] GameObject armRight = null;
    [SerializeField] Transform shootPointLeft = null;
    [SerializeField] Transform shootPointRight = null;
    float timeLastShoot;
    [SerializeField] float cadency = 1; 

    [Header("Health")]
    [SerializeField] int health = 5;
    GameObject explosion;

    [Header("Components")]
    SpriteRenderer sr;
    #endregion

    void Start()
    {
        startingPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        armLeft.SetActive(false);
        armRight.SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (direction == 1)
        {
            sr.flipX = false;
            armLeft.SetActive(false);
            armRight.SetActive(true);
        }
        else
        {
            sr.flipX = true;
            armLeft.SetActive(true);
            armRight.SetActive(false);
        }

        if ((player.activeSelf == true) && (Vector3.Distance(transform.position, player.transform.position) < attackRange))
        {
            LookAtPlayer();
        }
        else
        {
            if (!dontMove)
            {
                Movement();
            }
        }
    }

    /// <summary>
    /// Function we call if the enemy comes into contact with a trigger collider.
    /// </summary>
    /// <param name="other">Object colliding with the enemy.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("BulletPlayer")))
        {
            other.gameObject.SetActive(false);
            health -= 1;
            if (health <= 0)
            {
                explosion = ObjectPooler.SharedInstance.GetPooledObject("Explosion");
                if (explosion != null)
                {
                    explosion.SetActive(true);
                    explosion.transform.position = transform.position;
                    explosion.transform.rotation = transform.rotation;
                }
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Function that makes the enemy move constantly.
    /// </summary>
    void Movement()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        
        ChangeDirection();
    }

    /// <summary>
    /// Feature that makes the enemy look at the player.
    /// </summary>
    void LookAtPlayer()
    {
        if (player.transform.position.x < transform.position.x)
        {
            direction = -1;
            Vector3 dir = transform.position - player.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            armLeft.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Attack(shootPointLeft);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            direction = 1;
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            armRight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Attack(shootPointRight);
        }
    }

    /// <summary>
    /// Function that makes the enemy attack constantly.
    /// </summary>
    /// <param name="shootPoint">Site where the bullet is instantiated depending on where the enemy is looking.</param>
    void Attack(Transform shootPoint)
    {
        if (Time.time - timeLastShoot > cadency)
        {
            timeLastShoot = Time.time;

            bullet = ObjectPooler.SharedInstance.GetPooledObject("BulletEnemy");

            if (bullet != null)
            {
                bullet.transform.position = shootPoint.transform.position;
                bullet.transform.rotation = shootPoint.transform.rotation;
                bullet.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Function that makes the enemy change direction.
    /// </summary>
    void ChangeDirection()
    {
        if ((transform.position.x) > startingPosition.x + movementDistance)
        {
            direction = -1;
        }
        else if ((transform.position.x) < startingPosition.x - movementDistance)
        {
            direction = 1;
        }
    }
}
