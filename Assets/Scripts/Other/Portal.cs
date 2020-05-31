using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal : MonoBehaviour {

    GameObject player;

    [SerializeField]
    Vector3 tpPoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) < 2f)
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            if (tpPoint != new Vector3(0, 0, 0))
                player.transform.position = tpPoint;
            else
                player.transform.position = new Vector3(100, 0, 0);
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);


            //player.transform.position = new Vector3(-6, 0, 1);
            //player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }
    }

}
