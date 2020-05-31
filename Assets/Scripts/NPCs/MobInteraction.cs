using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobInteraction : MonoBehaviour {

    GameObject player;
    NavMeshAgent agent;
    bool willInteract;
    float distanceBtwnObjects;
    private float minInteractableDistance = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = player.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CheckForHit();
        }

        if ((willInteract == true))
        {
            distanceBtwnObjects = (transform.position - player.transform.position).sqrMagnitude;
            if (distanceBtwnObjects < minInteractableDistance)
            {
                Interact();
                willInteract = false;
                //destroy the interaction particle
            }
        }
    }

    private void Interact()
    {
        Debug.Log("Mob Interaction!");
    }

    void CheckForHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Mob")
        {
            agent.SetDestination(hit.point);
            willInteract = true;
            //create an interaction particle
        }
    }
}
