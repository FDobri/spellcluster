using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBoss : MonoBehaviour
{

    GameObject player;

    [SerializeField]
    ParticleSystem fireParticle;

    ParticleSystem fire;

    [SerializeField]
    int damage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SelfDestruct());
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 10);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 10 * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, player.transform.position) < .1f)
        {
            player.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject);
            fire = Instantiate(fireParticle, transform.position, transform.rotation);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(fire);
        if (gameObject)
            Destroy(gameObject);
    }
}
