using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehaviour : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject[] players;
    float attackRange = 2;
    float chaseRange = 20;
    public static int numer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = 4;

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform closestPlayer = FindClosestPlayer();
        if (closestPlayer != null)
        {
            agent.SetDestination(closestPlayer.position);
            float distance = Vector3.Distance(animator.transform.position, closestPlayer.position);
            if (distance < attackRange)
            {
                
                animator.SetBool("isAttacking", true);
                numer = 2;
            }

            if (distance > chaseRange)
                animator.SetBool("isChasing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 2;
    }

    Transform FindClosestPlayer()
    {
        float closestDistance = float.MaxValue;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(agent.transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        return closestPlayer;
    }
}