using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActEnum
{
    Hand_Drag,
    Hand_Click,
    Hand_Slip,
    Hand_Holding,
    Dir_Left,
    Dir_Right,
    Dir_Up,
    InputPercent
}

public class PlayerActor : BaseActor {
    
	// Use this for initialization
	public override void LogicAwake(){
        
    }
	
	// Update is called once per frame
	public override void LogicUpdate() {
        
    }
    public void FireOff()
    {
        if( ActorState.SkillType == SkillEnum.RocketCut )
        {
            RocketCutState State = (RocketCutState)ActorState;
            State.FireOff();
        }
    }

}
