﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEndState : PlayerState {
    public FallingEndState(PlayerActor InActor) : base(InActor)
    {
    }
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Falling;
        }
    }
}
