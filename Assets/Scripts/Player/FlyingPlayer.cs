using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script with the movement and attack functions assigned to the player when flying (chapter 3).
/// </summary>
public class FlyingPlayer : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] float speed = 4;

    [Header("Shoot")]
    float timeLastShoot;
    [SerializeField] float cadency = 0.25f;
    GameObject bullet;
    Transform arm;
    public GameObject armLeft;
    public GameObject armRight;
    [SerializeField] Transform shootPointLeft = null;
    [SerializeField] Transform shootPointRight = null;

    [Header("Components")]
    SpriteRenderer sr;
    Camera mainCamera;
    #endregion

    void Start()
    {
        mainCamera = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        armLeft.SetActive(false);
        armRight.SetActive(true);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        float v = Input.GetAxisRaw("Vertical");

        if (Time.timeScale != 0)
        {
            Movement(h, v);

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
    /// Function we call when a trigger collision occurs.
    /// </summary>
    /// <param name="collision">Object of the collision.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnTrigger3")
        {
            Level3Manager.level3Manager.StartFade();
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Function that makes the player move.
    /// </summary>
    /// <param name="h">A value returned by the GetAxisRaw Horizontal and that has a value of -1, 0, or 1.</param>
    /// <param name="v">A value that is returned by the GetAxisRaw Vertical and that has a value of -1, 0, or 1.</param>
    void Movement(float h, float v)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPosition = mainCamera.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - cameraPosition.x;
        mousePos.y = mousePos.y - cameraPosition.y;

        transform.Translate(Vector2.right * speed * Time.deltaTime * h);
        transform.Translate(Vector2.up * speed * Time.deltaTime * v);

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
