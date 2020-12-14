using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that positions the AIM where the pointer is.
/// </summary>
public class MousePointer : MonoBehaviour
{
    RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition = Input.mousePosition;
    }
}
