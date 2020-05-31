using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 3.8f);
    }

    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * 35f;
        transform.GetComponent<CanvasGroup>().alpha -= 0.003f;
    }
}
