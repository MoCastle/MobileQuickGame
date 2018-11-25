using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
[System.Serializable]
public struct AttackRate
{
    public string Name;
    public float Rate;
}
public class EnemyActor : BaseActor
{
    [Title("巡逻AI名字", "black")]
    public string AIGuardingName;
    [Title("战斗AI名字", "black")]
    public string AIBattleName;

    public Vector3 BirthPlace;
    public int InitID;
    public AttackRate[] CloseAttackArray;
    public AttackRate[] FarAttackArray;
    public Slider LifeSlider;

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
            if (_BattleArea == null)
            {
                _BattleArea = transform.FindChild("BattleEyeSize").GetComponent<BoxCollider2D>();
            }
            return _BattleArea;
        }
    }
    protected AICtrler _AICtrler;
    public AICtrler AICtrl
    {
        get
        {
            if (_AICtrler == null)
            {
                ResetAICtrl(AIGuardingName);
            }
            return _AICtrler;
        }
        set
        {
            _AICtrler = value;
        }
    }

    public override void LogicAwake()
    {
        base.LogicAwake();
        Debug.Log(gameObject.name + "-" + "Awake");
        Debug.Log(transform.position);
    }
    public override void LogicStart()
    {
        base.LogicStart();
        Debug.Log(gameObject.name+"-"+"Start");
        Debug.Log(transform.position);
    }

    public override void LogicUpdate()
    {
       
        AICtrl.Update();
        LifeSlider.value = ActorPropty.PercentLife;

    }

    public BaseActor CheckGetPlayer( )
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        LayerMask Layer = 1 << LayerMask.NameToLayer("Player");
        ContactFilter.SetLayerMask(Layer);
        GuardingArea.OverlapCollider(ContactFilter, ColliderList);

        BaseActor TargetActor = null;
        if(ColliderList[0] != null && ColliderList[0].gameObject.tag == "Player" )
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
        if (ColliderList[0] != null && ColliderList[0].gameObject.tag == "Player")
        {
            TargetActor = ColliderList[0].GetComponent<PlayerActor>();
        }
        return TargetActor;
    }

    public virtual bool CheckOnGroundClose( )
    {

        return false;
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

    public void AIComplete( )
    {
        //_AICtrler.EndAI();
    }

    //被打断
    public override void BeBreak()
    {
        AICtrl.Break();
    }
    /*
    //击退
    public override void HitBack(CutEffect HitEffect = new CutEffect())
    {
        float RandValue = Random.Range(0f, 1f);
        //击退概率 暂时写死
        if( RandValue < 0.6f )
        {
            base.HitBack(HitEffect);
        }
    }*/

    //公共接口
    #region
    //判断是否超出战斗区域
    public virtual bool IfInBattleArea()
    {
        return (TransCtrl.position - BirthPlace).x > 60;
     }
    #endregion

    //AI相关
    #region 
    //重置AI控制器
    public void ResetAICtrl(string AICtrlName)
    {
        if ( AICtrlName == null || AICtrlName =="")
        {
            AICtrlName = AICtrlName == null ? "Null" : "Empty";
            Debug.Log("Erro AICtrlName" + AICtrlName);
            _AICtrler = new GuardAiCtrl(this);
            return;
        }
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetCtrler = assembly.GetType(AICtrlName);
        AICtrler NewCtrler = (AICtrler)Activator.CreateInstance(GetCtrler, new object[] { this }); // 创建类的实例，返回为 object 类型，需要强制类型转换
        _AICtrler = NewCtrler;
        if(_AICtrler == null)
        {
            _AICtrler = new GuardAiCtrl(this);
        }
    }
    //进入哨戒状态
    public virtual void EnterGuarding()
    {
        ResetAICtrl(AIGuardingName);
    }
    public virtual void EnterBattle()
    {
        ResetAICtrl(AIBattleName);
    }
    #endregion
}
