using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneDir : BattleDir {
    
    public override void EnterScene()
    {
        base.EnterScene();
        BaseCharacter character = PlayerActor.character;
        character.DeathEvent += () => { UIManager.Mgr.ShowUI("PlayerDeathUI"); };

    }
    
}
