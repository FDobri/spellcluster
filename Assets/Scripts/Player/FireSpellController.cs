using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpellController : MonoBehaviour
{

    List<Transform> minionPositions = new List<Transform>();
    int spellDmg;

    void Start()
    {
        spellDmg = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerSpells>().GetSpellDamage();
        StartCoroutine(DealDamageToNearbyEnemies());
        Destroy(gameObject, 4f);
    }

    IEnumerator DealDamageToNearbyEnemies()
    {
        yield return new WaitForSeconds(.7f);

        var enemies = new List<GameObject>();

        enemies.AddRange(GameObject.FindGameObjectsWithTag("Mob"));
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Boss"));

        enemies
            .ForEach(p => minionPositions.Add(p.transform));

        var newList = minionPositions
            .Where(p => p)
            .Where(p => Vector3.Distance(p.position, transform.position) < 3f && p.GetComponent<MobStats>().GetCurHealth() > 0)
            .ToList();

        newList
            .ForEach(p => p.GetComponent<MobStats>().TakeDamage(Random.Range((spellDmg - 5) / 4, (spellDmg + 5) / 4)));

        newList
            .ForEach(p => p.GetComponent<MobController>().SetCombat(true));

        StartCoroutine(DealDamageToNearbyEnemies());
    }

}
