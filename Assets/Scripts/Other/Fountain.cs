using System.Collections;
using UnityEngine;

public class Fountain : MonoBehaviour
{

    GameObject player;
    PlayerStats playerStats;
    public static bool canHeal = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        StartCoroutine(HealPeriod());
    }


    IEnumerator HealPeriod()
    {
        yield return new WaitForSeconds(1f);
        if (Vector3.Distance(player.transform.position, transform.position) < 3f && canHeal)
            playerStats.IncreaseCurHealth((int)Random.Range(10f, 20f));
        StartCoroutine(HealPeriod());
    }
}