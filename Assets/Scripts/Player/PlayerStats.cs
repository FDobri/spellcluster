using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    PlayerAnimator playerAnimator;

    [SerializeField]
    private Text playerNameText;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Slider HPBar;

    [SerializeField]
    private float maxHealth;

    private float curHealth;

    delegate void DisableEnableComponentsDel(bool tof);
    DisableEnableComponentsDel tofDel;

    [SerializeField]
    private Behaviour[] disabledScriptsUponDying;

    [SerializeField]
    private Vector3 spawnPosition;

    public event Action OnPlayerDeath;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        tofDel = DisableOrEnableComponents;
        playerAnimator = GetComponent<PlayerAnimator>();
        curHealth = maxHealth;
        UpdateHPBar();
        OnPlayerDeath += playerAnimator.DeathAnimation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnPlayerDeath();
        }
    }

    public void TakeDamage(float amount)
    {
        if (curHealth > 0)
        {
            curHealth -= (int)amount;
            UpdateHPBar();
            if (curHealth <= 0)
            {
                tofDel(false);
                OnPlayerDeath();
                StartCoroutine(RespawnPlayer());
            }
        }
        else
        {
            return;
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<NavMeshAgent>().enabled = false;
        gameObject.transform.position = spawnPosition;
        tofDel(true);
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
        GetComponent<PlayerAnimator>().StartCoroutine("LocomotionAfterTime");
        curHealth = maxHealth;
        UpdateHPBar();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Mob"))
        {
            g.GetComponent<MobStats>().SetCurHpToMaxHp();
        }
        Fountain.canHeal = true;
    }


    private void DisableOrEnableComponents(bool tof)
    {
        foreach (Behaviour b in disabledScriptsUponDying)
        {
            b.enabled = tof;
        }
    }

    void UpdateHPBar()
    {
        HPBar.value = gameObject.GetComponent<PlayerStats>().GetCurHealth() / maxHealth;
        healthText.text = GetCurHealth().ToString() + "/" + maxHealth;
    }
        
    public float GetCurHealth()
    {
        return curHealth;
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        curHealth += amount;
        UpdateHPBar();
    }

    public void IncreaseCurHealth(float amount)
    {
        if (curHealth + amount > maxHealth)
        {
            curHealth = maxHealth;
        }
        else
        {
            curHealth += amount;
        }
        UpdateHPBar();
    }
}
