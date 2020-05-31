using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneHit : MonoBehaviour {

    void OnCollisionEnter()
    {
        switch (gameObject.transform.name)
        {
            case "Rune1Hit":
                transform.GetComponent<RuneController>().IncreasePlayerStat(Random.Range(21, 27), transform.GetComponent<RuneController>().IncreaseMaxHp);
                Destroy(gameObject);
                break;
            case "Rune2Hit":
                transform.GetComponent<RuneController>().IncreasePlayerStat(Random.Range(21, 27), transform.GetComponent<RuneController>().IncreaseDmg);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
