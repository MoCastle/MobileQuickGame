using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : BaseState {
    public override SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Default;
        }
    }

    public override void Update()
    {
    }

    // Use this for initialization
    void Start () {
		
	}
    public DefaultState( BaseActor Actor ):base( Actor )
    {

    }
}
