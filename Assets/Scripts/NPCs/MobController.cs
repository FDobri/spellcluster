using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobController : MonoBehaviour
{

    GameObject player;
    NavMeshAgent minionAgent;
    MobAnimator mobAnimator;

    bool canAttack = true;
    bool inCombat = false;
    float distanceToPlayer;
    [SerializeField]
    float damage;
    [SerializeField]
    float rotationSpeed;
    Vector3 startingPosition;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        minionAgent = transform.GetComponentInChildren<NavMeshAgent>();
        mobAnimator = GetComponent<MobAnimator>();
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (inCombat)
        {
            Fountain.canHeal = false;
            distanceToPlayer = (player.transform.position - transform.position).magnitude;
            if (distanceToPlayer > 2.5f)
            {
                ChasePlayer();
            }
            else
            {
                MinionAttack();
            }
        }
    }

    public void SetCombat(bool tof)
    {
        inCombat = tof;
    }
    void OnCollisionEnter(Collision collision)
    {
        AggroNearbyMinions();

        if (inCombat == false)
        {
            inCombat = true;
        }
    }

    public void AggroNearbyMinions()
    {
        var minions = new List<GameObject>();

        minions = GameObject.FindGameObjectsWithTag("Mob").Where(p => Vector3.Distance(p.transform.position, transform.position) <= 8f).ToList();

        minions.ForEach(p => p.GetComponent<MobController>().inCombat = true);
    }

    private void MinionAttack()
    {
        if (inCombat)
        {
            StandInPlace();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            if (canAttack)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (player.GetComponent<PlayerStats>().GetCurHealth() > 0 && inCombat)
        {
            mobAnimator.AttackAnimation();
            canAttack = false;
            StartCoroutine(CanAttackCooldown());
        }
        else
        {
            inCombat = false;
            minionAgent.SetDestination(startingPosition);
        }
        //player.GetComponent<PlayerStats>().TakeDamage(23);
    }

    private IEnumerator CanAttackCooldown()
    {
        yield return new WaitForSeconds(3f);
        if (inCombat)
            canAttack = true;
    }

    void StandInPlace()
    {
        minionAgent.SetDestination(minionAgent.transform.position);
    }

    private void ChasePlayer()
    {
        minionAgent.SetDestination(player.transform.position);
        Fountain.canHeal = false;
    }

    public IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(.9f);
        if (inCombat == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(Random.Range(damage - 5f, damage + 5f));
            if (transform.position == GetComponent<NavMeshAgent>().destination && GetComponent<MobStats>().GetCurHealth() > 0)
            {
                mobAnimator.GetAnimator().CrossFade("CombatState", .1f);
            }
        }
    }
}
