using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    GameObject playerObject;

    [HideInInspector]
    public bool inCombat = false;
    [HideInInspector]
    public bool isCasting = false;

    bool isOnCd = false;
    bool isFirstSpell = false;

    float castingFor = 0f;
    float castingTime1 = 2f;
    float castingTime2 = 3f;

    Vector3 startingPoint;

    [SerializeField]
    GameObject spellPrefab1;
    [SerializeField]
    GameObject spellPrefab2;
    GameObject spell;

    NavMeshAgent agent;
    MobStats mobStats;
    BossAnimator bossAnimator;

    void Start()
    {
        startingPoint = transform.position;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        mobStats = GetComponent<MobStats>();
        bossAnimator = GetComponent<BossAnimator>();
    }

    void Update()
    {
        if (DistanceBtwnObjects(playerObject.transform.position, transform.position) < 10f)
        {
            inCombat = true;
        }

        if (inCombat)
        {
            Fountain.canHeal = false;
            transform.LookAt(playerObject.transform.position);
            if (DistanceBtwnObjects(playerObject.transform.position, transform.position) < 10f && isOnCd == false && isCasting == false)
            {
                agent.SetDestination(transform.position);
                CastSpell();
            }

            if (DistanceBtwnObjects(playerObject.transform.position, transform.position) > 10f && isCasting == false)
            {
                agent.SetDestination(playerObject.transform.position);
            }

            if (isCasting)
            {
                castingFor += Time.deltaTime;
            }
        }
    }

    private void CastSpell()
    {
        if (playerObject.GetComponent<PlayerStats>().GetCurHealth() > 0)
        {
            System.Random rnd = new System.Random();
            isCasting = true;
            if (rnd.Next(0, 10) > 5)
            {
                isFirstSpell = true;
                StartCoroutine(ShootSpell(castingTime1));
                bossAnimator.GeneralAnimation("SpellCasting1");
            }
            else
            {
                isFirstSpell = false;
                StartCoroutine(ShootSpell(castingTime2));
                bossAnimator.GeneralAnimation("SpellCasting2");
            }
        }
        else
        {
            inCombat = false;
            mobStats.SetCurHpToMaxHp();
            agent.SetDestination(startingPoint);
        }
    }

    IEnumerator ShootSpell(float castingTime)
    {
        yield return new WaitForSeconds(castingTime);
        if (isCasting && castingFor >= castingTime - .1f)
        {
            if (isFirstSpell == true)
            {
                Instantiate(spellPrefab1, transform.position + new Vector3(0, 2.5f, 0), transform.rotation);
                bossAnimator.GeneralAnimation("SpellShoot1");
            }
            else
            {
                Instantiate(spellPrefab2, playerObject.transform.position, transform.rotation);
                bossAnimator.GeneralAnimation("SpellShoot2");
            }
        }
        isCasting = false;
        isOnCd = true;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);
        if (mobStats.GetCurHealth() > 0f)
            isOnCd = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (inCombat == false)
        {
            inCombat = true;
        }
    }

    float DistanceBtwnObjects(Vector3 v1, Vector3 v2)
    {
        return Vector3.Distance(v1, v2);
    }
}
