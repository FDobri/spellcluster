using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossAnimator : MonoBehaviour
{
    Animator animator;
    const float animationSmoothening = .1f;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void GeneralAnimation(string name)
    {
        animator.Play(name);
    }

    public void SpellShootAnimation1()
    {
        animator.Play("SpellShoot1");
        StartCoroutine(LocomotionAfterTime());
    }

    public IEnumerator LocomotionAfterTime()
    {
        yield return new WaitForSeconds(1f);
        animator.CrossFade("Locomotion", animationSmoothening);
    }
}
