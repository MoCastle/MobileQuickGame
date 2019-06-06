using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMaskAdapt : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ScreenMask mask = animator.gameObject.GetComponent<ScreenMask>();
        mask.OnAnimEnd();
    }
}
