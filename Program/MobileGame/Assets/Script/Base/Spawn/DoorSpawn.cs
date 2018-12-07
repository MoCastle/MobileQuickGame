using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawn : PlayerSpawn
{
    public string CenceName;
    public int DoorID;
    // Use this for initialization

    public BoxCollider2D EnemyChecker;
    public BoxCollider2D PlayerChecker;

    bool Inited;

    //跳场景
    public void SwitchCence()
    {
        if (CenceName != "" || DoorID > 0)
        {
            GameCtrl.GameCtrler.CenceCtroler.EnterCence(new NormalScence(GameCtrl.GameCtrler.CenceCtroler, new ScenceMsg(CenceName, DoorID)));
        }
    }

    public void Update()
    {
        if( !Inited )
        {
            if( CheckGetPlayer( ) )
            {
                return;
            }
            else
                Inited = true;
        }

        if (CheckGetPlayer())
        {
            if (CheckEnemyInArea())
            {
                return;
            }
            
            SwitchCence();
        }
    }
    public override BaseActorObj GenActor()
    {
        BaseActorObj Actor = base.GenActor();
        Inited = false;
        return Actor;
    }

    public bool CheckGetPlayer()
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        LayerMask Layer = 1 << LayerMask.NameToLayer("Player");
        ContactFilter.SetLayerMask(Layer);
        PlayerChecker.OverlapCollider(ContactFilter, ColliderList);

        BaseActor TargetActor = null;
        if (ColliderList[0] != null && ColliderList[0].gameObject.tag == "Player")
        {
            TargetActor = ColliderList[0].GetComponent<PlayerActor>();
        }
        return TargetActor != null;
    }

    public bool CheckEnemyInArea()
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        LayerMask Layer = 1 << LayerMask.NameToLayer("Enemy");
        ContactFilter.SetLayerMask(Layer);
        EnemyChecker.OverlapCollider(ContactFilter, ColliderList);

        BaseActor TargetActor = null;
        if (ColliderList[0] != null && ColliderList[0].gameObject.tag == "Enemy")
        {
            TargetActor = ColliderList[0].GetComponent<PlayerActor>();
        }
        return TargetActor != null;
    }
}