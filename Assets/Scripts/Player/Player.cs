using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script with the movement and attack functions assigned to the player when he goes on foot (chapters 1 and 2).
/// </summary>
public class Player : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] float speed = 4;
    [SerializeField] float jump = 8.0f;
    [SerializeField] LayerMask groundLayerMask = 0;

    [Header("Shoot")]
    float timeLastShoot;
    [SerializeField] float cadency = 0;
    public Vector3 mousePos;
    GameObject bullet;
    Transform arm;
    public GameObject armLeft;
    public GameObject armRight;
    [SerializeField] Transform shootPointLeft = null;
    [SerializeField] Transform shootPointRight = null;

    [Header("Components")]
    BoxCollider2D boxCollider2D;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    Camera mainCamera;
    #endregion

    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        armLeft.SetActive(false);
        armRight.SetActive(true);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (Time.timeScale != 0)
        {
            Movement(h);

            Animation(h, IsGrounded());

            if ((Input.GetButtonDown("Jump")) && IsGrounded() == true)
            {
                Jump();
            }

            if (Input.GetButton("Fire1") && Time.time - timeLastShoot > cadency)
            {
                Shoot();
            }

            if (Input.GetButtonDown("Cancel"))
            {
                GameManager.gameManager.PauseGame();
            }
        }
    }

    /// <summary>
    /// Function we call when a collision occurs.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }

    /// <summary>
    /// Function called when the player is having a constant collision with an object.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            if ((collision.gameObject.GetComponent<MovingPlatform>().isVertical == true) && (collision.gameObject.GetComponent<MovingPlatform>().direction == -1))
            {
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = 1;
            }
        }
    }

    /// <summary>
    /// Function that we call when the player leaves a collision.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
            rb.gravityScale = 1;
        }
    }

    /// <summary>
    /// Function that we call when the player enters a trigger collision.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnTrigger1")
        {
            Level1Manager.level1Manager.SpawnZone();
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.name == "SpawnTrigger2")
        {
            Level2Manager.level2Manager.SpawnZone();
            collision.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Boolean that indicates from a raycast if the player is touching the ground.
    /// </summary>
    /// <returns>True if the raycast touches an object with the specified mask, false if it doesn't.</returns>
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size - new Vector3(0.15f, 0, 0), 0f, Vector2.down, 0.1f, groundLayerMask);
        return raycastHit.collider != null;
    }

    /// <summary>
    /// Function that makes the player move.
    /// </summary>
    /// <param name="h">A value returned by the GetAxisRaw and that has a value of -1, 0, or 1.</param>
    void Movement(float h)
    {
        mousePos = Input.mousePosition;
        Vector3 cameraPosition = mainCamera.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - cameraPosition.x;
        mousePos.y = mousePos.y - cameraPosition.y;

        transform.Translate(Vector2.right * speed * Time.deltaTime * h);

        if (mousePos.x >= 0)
        {
            sr.flipX = false;
            armLeft.SetActive(false);
            armRight.SetActive(true);
            arm = armRight.transform;
        }
        else if (mousePos.x < 0)
        {
            sr.flipX = true;
            armLeft.SetActive(true);
            armRight.SetActive(false);
            arm = armLeft.transform;
        }

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        arm.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// Function that makes animations run.
    /// </summary>
    /// <param name="h">A value returned by the GetAxisRaw and that has a value of -1, 0, or 1.</param>
    /// <param name="isGrounded">Boolean that is true when a player is touching the ground.</param>
    void Animation(float h, bool isGrounded)
    {
        anim.SetBool("Walking", h != 0 && isGrounded);
        anim.SetBool("Jumping", !isGrounded);
    }

    /// <summary>
    /// Function that makes the jumps run.
    /// </summary>
    void Jump()
    {
        rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Function that makes the player shoot.
    /// </summary>
    void Shoot()
    {
        bullet = ObjectPooler.SharedInstance.GetPooledObject("BulletPlayer");
        Transform shootPoint;
        
        if (sr.flipX == false)
        {
            shootPoint = shootPointRight;
        }
        else
        {
            shootPoint = shootPointLeft;
        }
        timeLastShoot = Time.time;
        
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = shootPoint.rotation;
            bullet.SetActive(true);
        }
    }
}
