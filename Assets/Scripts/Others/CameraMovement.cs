using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that makes the camera constantly follow the player.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    public Transform target;
    [Range(0.0f, 20.0f)]
    public float smoothing;
    public bool interpolation;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 cameraPosition = target.position + offset;

            if (interpolation)
            {
                transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothing * Time.deltaTime);
            }
            else
            {
                transform.position = cameraPosition;
            }
        }
    }
}
