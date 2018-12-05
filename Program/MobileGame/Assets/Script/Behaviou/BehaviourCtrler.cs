using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCtrler {

    public BehaviourCtrler(BaseActorObj actor, Animator animator, AnimStruct[] behaviourList)
    {
        _Acotr = actor;
        _Animator = animator;
        _BehaviourList = behaviourList;
    }

    BaseActorObj _Acotr;
    Animator _Animator;
    AnimStruct[] _BehaviourList; 
    string _CurName = "Usual";
    
    void Update( )
    {

    }
}
