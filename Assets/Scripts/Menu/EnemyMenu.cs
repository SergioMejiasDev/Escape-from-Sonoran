using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script of the enemies that appear moving in the main menu.
/// </summary>
public class EnemyMenu : MonoBehaviour
{
    float speed = 2;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
