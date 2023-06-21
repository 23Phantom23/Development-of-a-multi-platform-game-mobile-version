using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    float timer;
    GameObject[] players;
    float chaseRange = 10;
    bool govn;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 5)
            animator.SetBool("isPatrolling", true);

        float closestDistance = float.MaxValue;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(animator.transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }


        govn = true;
        if (govn == true)
        {
            float distance = Vector3.Distance(animator.transform.position, closestPlayer.position);
            if (distance < chaseRange)
                animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

   

       
    
}