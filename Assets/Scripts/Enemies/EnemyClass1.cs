using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script associated with the type 1 enemy (red, goes on foot and shoots).
/// </summary>
public class EnemyClass1 : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] float speed = 3; //Indicamos la velocidad del enemigo.
    public int direction = 1; //Indicamos la dirección inicial del enemigo, siendo 1 a la derecha y -1 a la izquierda.
    [SerializeField] bool dontMove = false; //Este booleano será por si queremos que el enemigo no se mueva, que permanezca constantemente en el mismo sitio.
    Vector3 startingPosition; //Definimos la posición inicial del enemigo, a partir de la cual irá moviéndose de un lado a otro.
    [SerializeField] float movementDistance = 100; //La distancia máxima y mínima con respecto al origen que se moverá el enemigo.

    [Header("Attack")]
    [SerializeField] float attackRange = 0; //Distancia a partir de la cual el enemigo ataca.
    GameObject player; //Indicamos a quien atacará el enemigo, que viene a ser el jugador.
    GameObject bullet; //Indicamos la bala que utilizaremos.
    [SerializeField] GameObject armLeft = null; //Indicamos cual es el brazo si el enemigo mira a la izquierda.
    [SerializeField] GameObject armRight = null; //Indicamos cual es el brazo si el enemigo mira a la derecha.
    [SerializeField] Transform shootPointLeft = null; //Indicamos donde se instancia la bala en el cañón izquierdo.
    [SerializeField] Transform shootPointRight = null; //Indicamos donde se instancia la bala en el cañón derecho.
    float timeLastShoot; //Esta variable se pone a 0 cada vez que el enemigo dispare, es para saber cuanto tiempo ha pasado desde el último disparo.
    [SerializeField] float cadency = 1; //Definimos cuanto tiempo puede pasar entre disparo y disparo. 

    [Header("Health")]
    [SerializeField] int health = 5; //Indicamos la salud del enemigo.
    GameObject explosion; //Indicamos la animación de la explosión que aparecerá cuando muera el enemigo.

    [Header("Components")]
    Animator anim; //Definimos el componente Animator.
    SpriteRenderer sr; //Definimos el componente Sprite Renderer.
    #endregion

    void Start()
    {
        startingPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

        Animation();
    }

    /// <summary>
    /// Function called when a trigger collider comes into contact with the enemy.
    /// </summary>
    /// <param name="other">The object that collides with the enemy.</param>
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
    /// Feature that makes the enemy stand up and look at the player.
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
        else if (player.transform.position.x >= transform.position.x)
        {
            direction = 1;
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            armRight.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Attack(shootPointRight);
        }
    }

    /// <summary>
    /// Function where the enemy constantly attacks the player.
    /// </summary>
    /// <param name="shootPoint">Site where the bullets are instantiated according to the direction of the enemy.</param>
    void Attack(Transform shootPoint)
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
    /// Function where the animation function is constantly called.
    /// </summary>
    void Animation()
    {
        anim.SetBool("Attack", (player.activeSelf == true) && (Vector3.Distance(transform.position, player.transform.position) < attackRange) || dontMove);
    }

    /// <summary>
    /// Function where the enemy changes direction of movement.
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
