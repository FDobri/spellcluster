using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamestrikeBoss : MonoBehaviour
{
    [SerializeField]
    float damage;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>().TakeDamage(damage);
        StartCoroutine(DealDamageAgain());
    }

    IEnumerator DealDamageAgain()
    {
        yield return new WaitForSeconds(1f);

        if (Vector3.Distance(transform.position, player.transform.position) < 2f)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(damage/2);
    }
}
