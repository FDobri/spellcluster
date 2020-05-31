using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAnimator : MonoBehaviour
{

    private NavMeshAgent agent;
    Animator animator;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, .1f, Time.deltaTime);

        if (agent.destination != agent.transform.position)
        {
            LocomotionAnimation();
        }
    }

    public void LocomotionAnimation()
    {
        animator.Play("Locomotion");
    }

    public IEnumerator DeathAnimation()
    {
        gameObject.transform.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        yield return new WaitForSeconds(.2f);
        animator.Play("Death");
    }
}
