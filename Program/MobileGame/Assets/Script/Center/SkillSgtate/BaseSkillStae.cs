using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillStage
{
    Start,
    Execute
}

struct SkillOp
{
    public delegate void TimeEvent();
    SkillStage _Stage;
    public SkillStage Stage
    {
        get
        {
            return Stage;
        }
    }

    BaseSkillStae State;
    public void Update( )
    {

    }
}
public abstract class BaseSkillStae : BaseState
{

    public BaseSkillStae( BaseActor InActor ):base( InActor )
    { }
}
