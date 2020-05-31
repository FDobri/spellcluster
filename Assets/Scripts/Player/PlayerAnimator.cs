using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimator : MonoBehaviour
{

    private float animationSmoothening = .1f;
    //private float castingTime = 2f;
    private float castingFor;

    NavMeshAgent agent;
    Animator animator;

    private bool animationInterrupted;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, animationSmoothening, Time.deltaTime);
    }

    public void CancelCastingAnimation()
    {
        animator.CrossFade("Locomotion", animationSmoothening);
        //animator.Play("Locomotion");
    }

    public void SpellCastAnimation()
    {
        if (Random.Range(1, 10) > 5)
            animator.CrossFade("SpellCasting", animationSmoothening);
        else
            animator.CrossFade("SpellCasting2", animationSmoothening);
        //StartCoroutine(SpellCastingTransition());
    }

    public void SpellShootAnimation()
    {
        animator.CrossFade("SpellShooting", animationSmoothening);
        StartCoroutine(LocomotionAfterTime());
    }

    public void DeathAnimation()
    {
        animator.CrossFade("Death", animationSmoothening);
    }

    public IEnumerator LocomotionAfterTime()
    {
        yield return new WaitForSeconds(.5f);
        animator.CrossFade("Locomotion", animationSmoothening);
    }
}