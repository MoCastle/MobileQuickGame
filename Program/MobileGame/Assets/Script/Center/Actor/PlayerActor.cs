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
    public float MoveVector;
    public float ChargeAddSpeed;
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
    public void MoveForward()
    {
        Vector3 OldPst = TransCtrl.position;
        OldPst.x = OldPst.x + MoveVector;
        TransCtrl.position = OldPst;
    }
}
