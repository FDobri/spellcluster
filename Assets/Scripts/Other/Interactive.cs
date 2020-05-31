using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactive : MonoBehaviour {

    GameObject playerObject;
 
    [SerializeField]
    RawImage SignBoard;

    CanvasGroup interactionWindowCG;

    [SerializeField]
    string interactionTextTitle;
    
    [SerializeField]
    string interactionText;

    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        interactionWindowCG = SignBoard.GetComponent<CanvasGroup>();
        interactionWindowCG.alpha = 0f;
    }

    void Update () {
        if (Vector3.Distance(playerObject.transform.position, gameObject.transform.position) < 3f)
        {
            Interact();
        }
        else
        {
            interactionWindowCG.alpha = 0f;
        }
    }

    private void Interact()
    {
        interactionWindowCG.alpha = 1f;
        SignTextManager.signMessage = interactionText;
    }
}
