using System;
using System.Xml.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class TestingScript : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Time.timeScale = 0;
        //}


        if(Input.GetKeyDown(KeyCode.K))
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(100, 0, 0);
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            var enemies = new List<GameObject>();

            enemies.AddRange(GameObject.FindGameObjectsWithTag("Mob"));
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Boss"));

            List<Transform> minionPositions = new List<Transform>();

            enemies.ForEach(p => minionPositions.Add(p.transform));

            foreach (var i in minionPositions)
            {
                Debug.Log("name :" + i.name);
            }
        }
    }
}
