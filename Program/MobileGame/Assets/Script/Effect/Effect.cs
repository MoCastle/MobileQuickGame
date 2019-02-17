using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

    Animator _EFAnimator;
    Animator EFAnimator
    {
        get
        {
            if (_EFAnimator == null)
            {
                _EFAnimator = GetComponent<Animator>();
            }
            return _EFAnimator;
        }
    }

    // Use this for initialization
    void OnEnable()
    {
        if (EFAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
        {
            EFAnimator.playbackTime = 1f;
            EFAnimator.Play("EffectAnim", 0, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EFAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9)
        {
            EffectManager.Manager.PutBackEffect(this.gameObject);

        }
    }
}
