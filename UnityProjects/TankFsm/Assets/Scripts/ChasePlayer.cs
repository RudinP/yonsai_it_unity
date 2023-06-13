using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TankAi tankAi = animator.GetComponent<TankAi>();
        tankAi.ChasePlayer();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TankAi tankAi = animator.GetComponent<TankAi>();
        tankAi.SetNextPoint();
    }

}
