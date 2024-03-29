using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_FollowBehaviour : StateMachineBehaviour
{
    [SerializeField] private float _batSpeedMovement;

    [SerializeField] private float _timeBaseFollowing;

    private float _timeFollowing;
    private Transform _playerTransform;
    private EnemyController bat;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timeFollowing = _timeBaseFollowing;
        _playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        bat = animator.gameObject.GetComponent<EnemyController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position,_playerTransform.position,_batSpeedMovement * Time.deltaTime);
        
        //bat.TryToFind();
        bat.Turn(_playerTransform.position);
        _timeFollowing -= Time.deltaTime;
        if (_timeFollowing <= 0)
        {
            animator.SetTrigger("Bat_Back");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
