﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneDir : BattleDir {
    //生成玩家
    protected override void GenPlayer()
    {
        BaseActorObj player = ActorManager.Mgr.GenActor("Player");
        PlayerActor = player as PlayerObj;
    }
    public override void EnterScene()
    {
        base.EnterScene();
        BaseCharacter character = PlayerActor.Character;
        character.DeathEvent += () => { UIManager.Mgr.ShowUI("PlayerDeathUI"); };

    }
    
}
