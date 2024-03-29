using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_BackBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _batSpeedMovement;

    private Vector3 _initialPoint;

    private EnemyController _bat;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bat = animator.gameObject.GetComponent<EnemyController>();
        _initialPoint = _bat.initialPoint;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, _initialPoint,_batSpeedMovement * Time.deltaTime);
        _bat.Turn(_initialPoint);
        if (animator.transform.position == _initialPoint)
        {
            animator.SetTrigger("Bat_Arrived");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
