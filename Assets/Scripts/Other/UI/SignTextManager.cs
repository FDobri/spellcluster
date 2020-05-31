using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignTextManager : MonoBehaviour {

    public static string signMessage;
    Text signTxt;

    [SerializeField]
    string txtMsg;

    void Awake()
    {
        signTxt = GetComponent<Text>();
        signMessage = "";
    }

    void Update()
    {
        signTxt.text = signMessage;
    }
}
