using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWEnabler : MonoBehaviour
{
    public static bool isWEnabled;

    void Update()
    {
        if (isWEnabled)
            transform.GetComponent<CanvasGroup>().alpha = 1f;
    }
}
