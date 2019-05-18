using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using GameScene;
using UI;

public class InputTouchAnimatorBehaviour : StateMachineBehaviour
{
    [SerializeField]
    [Header("进入时设置状态")]
    public HandGesture inputEnterState;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        InputTouch touch = animator.GetComponent<InputTouch>();
        touch.SwitchState(inputEnterState);
    }

}
