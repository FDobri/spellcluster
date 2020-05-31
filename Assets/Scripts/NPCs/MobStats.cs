using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MobStats : MonoBehaviour
{

    [SerializeField]
    GameObject runeObject;

    [SerializeField]
    GameObject minionHealth;

    [SerializeField]
    GameObject portalAfterBoss;

    Vector3 healthScale;

    [SerializeField]
    int maxHealth;
    int curHealth;

    MobAnimator mobAnimator;
    BasicAnimator basicAnimator;

    public event Action OnMinionDeath;

    [SerializeField]
    private Behaviour[] disabledScriptsUponDying;

    private void Start()
    {
        basicAnimator = GetComponent<BasicAnimator>();
        try
        {
            mobAnimator = GetComponent<MobAnimator>();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("Couldn't find mob animator or boss animator");
            throw;
        }
        curHealth = maxHealth;

        OnMinionDeath += DisableComponents;
        OnMinionDeath += SpawnRune;
        healthScale = minionHealth.transform.localScale;
    }

    public void SetCurHpToMaxHp()
    {
        curHealth = maxHealth;
        minionHealth.transform.localScale = healthScale;
    }

    private void SpawnRune()
    {
        if (runeObject)
            Instantiate(runeObject, gameObject.transform.position + new Vector3(0, 1f, 0), gameObject.transform.rotation);
        else
            return;
    }

    private void DisableComponents()
    {
        transform.GetComponent<NavMeshAgent>().obstacleAvoidanceType = 0;
        foreach (Behaviour b in disabledScriptsUponDying)
        {
            b.enabled = false;
        }
    }

    public void TakeDamage(int amount)
    {

        if (curHealth >= amount)
        {
            if (transform.tag == "Mob")
                minionHealth.transform.localScale = new Vector3(minionHealth.transform.localScale.x, minionHealth.transform.localScale.y, minionHealth.transform.localScale.z - (((float)amount / (float)maxHealth) / 2));
            else
                minionHealth.transform.localScale = new Vector3(minionHealth.transform.localScale.x, minionHealth.transform.localScale.y, minionHealth.transform.localScale.z - (((float)amount / (float)maxHealth)));
        }
        else
            Destroy(minionHealth);

        curHealth -= amount;

        if (curHealth <= 0)
        {
            Death();
            if (minionHealth)
                Destroy(minionHealth);
        }
    }

    void Death()
    {
        OnMinionDeath();
        GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
        basicAnimator.DeathAnimation();

        if (mobAnimator)
        {
            Destroy(gameObject, 2f);
            mobAnimator.DeathAnimation();
            SlaughterTextManager.killCount++;
        }
        else
        {
            gameObject.transform.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            gameObject.transform.GetComponent<BossController>().inCombat = false;
            gameObject.transform.GetComponent<BossController>().isCasting = false;
            Destroy(gameObject, 5f);
            StartCoroutine(basicAnimator.DeathAnimation());
            Instantiate(portalAfterBoss, transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            PlayerSpells.SetCanCastW(true);
            SpellWEnabler.isWEnabled = true;
        }
        Fountain.canHeal = true;
    }

    public int GetCurHealth()
    {
        return curHealth;
    }
}
