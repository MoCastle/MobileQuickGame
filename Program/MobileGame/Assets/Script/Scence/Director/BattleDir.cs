using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDir : BaseDir
{
    public PlayerObj PlayerActor;

    public override void EnterScene()
    {
        _UIMgr.ShowUI("ScrollArea");
        if(PlayerActor== null)
            GenPlayer();
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
            if(MainCamera == null)
            {
                MainCamera = Resources.Load("Prefab\\Camera\\Main Camera") as Camera;
            }
        }
    }

    //生成玩家
    protected virtual void GenPlayer()
    {
        BaseActorObj player = ActorManager.Mgr.GenActor("Player");
        PlayerActor = player as PlayerObj;

        Propty playerPropty = PlayerMgr.Mgr.PlayerPropty;
        if(playerPropty!=null)
        {
            player.ActorPropty = playerPropty;
        }else
        {
            PlayerMgr.Mgr.PlayerPropty = playerPropty;
        }
    }

    //重生玩家
    public virtual void ReBirth()
    {

    }
}
