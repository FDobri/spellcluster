using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaughterTextManager : MonoBehaviour
{
    public static int killCount;
    Text slaughterTxt;

    void Awake()
    {
        slaughterTxt = GetComponent<Text>();
        killCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slaughterTxt.text = "Manslaughter count: " + killCount.ToString();
    }
}
