
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorAnimator : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponent<BaseActorObj>();
        NoticeObject.EnterState();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponent<BaseActorObj>();
        NoticeObject.ActionCtrl.CurAction.CompleteFunc();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
