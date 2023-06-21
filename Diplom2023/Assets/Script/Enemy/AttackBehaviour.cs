using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    GameObject[] players;
    Transform closestPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float closestDistance = float.MaxValue;
        closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(animator.transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }
        if (closestPlayer != null)
        {
            animator.transform.LookAt(closestPlayer);
            float distance = Vector3.Distance(animator.transform.position, closestPlayer.position);
            if (distance > 3)
            {
                
                animator.SetBool("isAttacking", false);
                ChaseBehaviour.numer = 3;
            }

            if (distance > 15)
                animator.SetBool("isChasing", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    
    
}