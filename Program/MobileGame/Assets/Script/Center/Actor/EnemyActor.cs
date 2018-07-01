using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct AttackRate
{
    public string Name;
    public float Rate;
}
public class EnemyActor : BaseActor
{


    public AttackRate[] CloseAttackArray;
    public AttackRate[] FarAttackArray;

    BoxCollider2D _GuardingArea;
    BoxCollider2D GuardingArea
    {
        get
        {
            if( _GuardingArea == null )
            {
                _GuardingArea = transform.FindChild("GuardingArea").GetComponent<BoxCollider2D>();
            }
            return _GuardingArea;
        }
    }
    BoxCollider2D _BattleArea;
    BoxCollider2D BattleArea
    {
        get
        {
            if (_GuardingArea == null)
            {
                _GuardingArea = transform.FindChild("BattleArea").GetComponent<BoxCollider2D>();
            }
            return _GuardingArea;
        }
    }
    BaseAI _AICtrler;
    public BaseAI AICtrl
    {
        get
        {
            return _AICtrler;
        }
        set
        {
            _AICtrler = value;
        }
    }
    public override void LogicAwake()
    {
        SwitcfhToGuardState();
    }
    public override void LogicUpdate()
    {
        _AICtrler.Update();
    }
    public BaseActor CheckGetPlayer( )
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        LayerMask Layer = 1 << LayerMask.NameToLayer("Player");
        ContactFilter.SetLayerMask(Layer);
        GuardingArea.OverlapCollider(ContactFilter, ColliderList);

        BaseActor TargetActor = null;
        if(ColliderList[0] != null && ColliderList[0].gameObject.name == "Player" )
        {
            TargetActor = ColliderList[0].GetComponent<PlayerActor>();
        }
        return TargetActor;
    }
    public BaseActor CheckInBattleArea( )
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        LayerMask Layer = 1 << LayerMask.NameToLayer("Player");
        ContactFilter.SetLayerMask(Layer);
        BattleArea.OverlapCollider(ContactFilter, ColliderList);

        BaseActor TargetActor = null;
        if (ColliderList[0] != null && ColliderList[0].gameObject.name == "Player")
        {
            TargetActor = ColliderList[0].GetComponent<PlayerActor>();
        }
        return TargetActor;
    }
    public virtual void SwitcfhToGuardState( )
    {
        _AICtrler = new GuardAI(this);
    }
    public virtual bool CheckOnGroundClose( )
    {

        return false;
    }
    public virtual void FaceTo( Vector2 InDir )
    {
        Vector3 OldScale = TransCtrl.localScale;
        if ( InDir.x * OldScale.x < 0)
        {
            OldScale.x = OldScale.x * -1;
            TransCtrl.localScale = OldScale;
        }
    }
    //移动
    public override void Move( Vector2 InDirection )
    {
        InDirection.y = 0;
        base.Move(InDirection);
    }
    
    public void Guard()
    {
        AnimCtrl.SetBool("Run", false);
    }

    public void SwitchAttackAI( )
    {
        _AICtrler = new CloseAttack(this);
    }

    public void AIComplete( )
    {
        _AICtrler.EndAI();
    }
}
