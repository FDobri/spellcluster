using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobAnimator : MonoBehaviour
{
    Animator animator;
    const float animationSmoothening = .1f;
    MobController mobController;

    public Animator GetAnimator()
    {
        return animator;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        mobController = GetComponent<MobController>();
    }

    public void AttackAnimation()
    {
        if (transform.tag == "Mob")
        {
            animator.CrossFade("Attack", animationSmoothening);
            StartCoroutine(mobController.EndAttackAnimation());
        }
        else if (transform.tag == "Boss")
        {
            if(Random.Range(1,10) > 5)
            {
                animator.CrossFade("Attack", animationSmoothening);
                StartCoroutine(mobController.EndAttackAnimation());
            }else
            {
                animator.CrossFade("Attack2", animationSmoothening);
                StartCoroutine(mobController.EndAttackAnimation());
            }
        }
    }

    public void LocomotionAnimation()
    {
        animator.Play("Locomotion");
    }

    public void DeathAnimation()
    {
        animator.Play("Death");
    }
}
