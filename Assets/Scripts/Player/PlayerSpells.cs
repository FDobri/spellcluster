using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField]
    GameObject frostSpell;
    [SerializeField]
    GameObject flamestrikeSpell;
    GameObject spellObject;
    GameObject target;

    PlayerAnimator playerAnimator;

    NavMeshAgent agent;

    static bool canCastW;
    bool spellActive;
    bool isCasting;
    bool isOnCooldownQ, isOnCooldownW;
    bool isCastingQ, isCastingW;
    float spellCastingTime = 1f;
    float spellCooldownTime = 3f;
    float castingCurrentlyFor;
    float cooldownCurrentlyForQ, cooldownCurrentlyForW;

    float rotationSpeed = 100f;
    float spellForwardForce = 500f;

    [SerializeField]
    int spellDamage = 50;

    public event Action OnSpellCast;
    public event Action OnSpellCastInterrupt;

    Vector3 flamestrikePosition;

    [SerializeField]
    Slider castingBar;
    [SerializeField]
    Slider cooldownBarQ;
    [SerializeField]
    Slider cooldownBarW;

    CanvasGroup canvasGroupCasting;
    public static void SetCanCastW(bool tof)
    {
        canCastW = tof;
    }

    public void IncreaseSpellDamage(int amount)
    {
        spellDamage += amount;
        SpellPowerText.spDmg = spellDamage;
    }

    public int GetSpellDamage()
    {
        return spellDamage;
    }

    private void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        agent = GetComponent<NavMeshAgent>();

        OnSpellCast += SpellCast;
        OnSpellCast += playerAnimator.SpellCastAnimation;
        OnSpellCast += LookAtMouse;

        OnSpellCastInterrupt += CancelCasting;
        OnSpellCastInterrupt += playerAnimator.CancelCastingAnimation;

        canvasGroupCasting = castingBar.GetComponent<CanvasGroup>();

        SetVisibility(canvasGroupCasting, 0f);
        SpellPowerText.spDmg = spellDamage;
    }

    private void FixedUpdate()
    {
        if (isCasting)
        {
            //LookAtMouse();
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !Input.GetMouseButton(1) && isOnCooldownQ == false && !isCasting)
        {
            OnSpellCast();
            SetSpellCastingTrue(true, false);
        }

        if (Input.GetKeyUp(KeyCode.W) && !Input.GetMouseButton(1) && isOnCooldownW == false && !isCasting && canCastW)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                flamestrikePosition = (new Vector3(hit.point.x, transform.position.y + .1f, hit.point.z));
                OnSpellCast();
                SetSpellCastingTrue(false, true);
            }
        }

        if (isCasting)
        {
            castingBar.value = castingCurrentlyFor / spellCastingTime;
            castingCurrentlyFor += Time.deltaTime;
            SetVisibility(canvasGroupCasting, .8f);
        }

        if (isOnCooldownQ)
        {
            cooldownBarQ.value = cooldownCurrentlyForQ / spellCooldownTime;
            cooldownCurrentlyForQ -= Time.deltaTime;
            if (cooldownCurrentlyForQ <= 0)
            {
                isOnCooldownQ = false;
            }
        }

        if (isOnCooldownW)
        {
            cooldownBarW.value = cooldownCurrentlyForW / spellCooldownTime;
            cooldownCurrentlyForW -= Time.deltaTime;
            if (cooldownCurrentlyForW <= 0)
            {
                isOnCooldownW = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButton(1))
        {
            OnSpellCastInterrupt();
        }
    }

    private void SetSpellCastingTrue(bool tof1, bool tof2)
    {
        isCastingQ = tof1;
        isCastingW = tof2;
    }

    private void CancelCasting()
    {
        isCasting = false;
        SetVisibility(canvasGroupCasting, 0f);
        castingCurrentlyFor = 0f;
        playerAnimator.CancelCastingAnimation();
    }

    void SpellCast()
    {
        isCasting = true;
        agent.SetDestination(transform.position);
        StartCoroutine(SpellShooting());
    }

    IEnumerator SpellShooting()
    {
        yield return new WaitForSeconds(spellCastingTime);
        if (isCasting == true && castingCurrentlyFor >= spellCastingTime)
        {
            Shoot();
            isCasting = false;
        }
    }

    void CreateSpell(GameObject gameObject)
    {
        if (isCastingQ)
        {
            spellObject = Instantiate(gameObject, transform.position + transform.up, transform.rotation);
            isOnCooldownQ = true;
            spellCooldownTime = 3f;
            cooldownCurrentlyForQ = spellCooldownTime;
        }
        else if (isCastingW)
        {
            spellObject = Instantiate(gameObject, flamestrikePosition, Quaternion.identity);
            isOnCooldownW = true;
            spellCooldownTime = 5f;
            cooldownCurrentlyForW = spellCooldownTime;
        }
    }

    private void Shoot()
    {
        if (isCastingQ)
        {
            CreateSpell(frostSpell);
            spellObject.GetComponent<Rigidbody>().AddForce(transform.forward * spellForwardForce);
        }
        else if (isCastingW)
            CreateSpell(flamestrikeSpell);

        playerAnimator.SpellShootAnimation();
        castingCurrentlyFor = 0f;
        SetVisibility(canvasGroupCasting, 0f);
        SetSpellCastingTrue(false, false);
    }

    void LookAtMouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (new Vector3(hit.point.x, transform.position.y + .4f, hit.point.z) - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void SetVisibility(CanvasGroup canvasGroup, float f)
    {
        canvasGroup.alpha = f;
    }
}
