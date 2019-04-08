
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class CharactorAnimator : StateMachineBehaviour
{
    //进入状态
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponentInParent<BaseActorObj>();
        NoticeObject.EnterState();
    }
    //离开状态
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BaseActorObj NoticeObject = animator.gameObject.GetComponentInParent<BaseActorObj>();
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
