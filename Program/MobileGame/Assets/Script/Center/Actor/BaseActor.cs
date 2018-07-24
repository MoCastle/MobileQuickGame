using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public struct AnimStruct
{
    public string AnimName;
    public string ClassName;
}
public struct CutEffect
{
    public float RangeTime;
    public float SpeedRate;
}
public abstract class BaseActor : MonoBehaviour {
    //受击效果
    public CutEffect BeCut;
    public Vector2 ForceMoveDirection = Vector2.up;
    public bool IsHoly;

    public float CAttackMove = 1;
    public float LAttackSpeed = 3;
    public bool LockFace;
    float _GravityScale;
    public Vector2 HitMoveDir;
    BoxCollider2D _SkillHurtBox;
    
    public Propty ActorPropty;
    public BoxCollider2D SkillHurtBox
    {
        get
        {
            if (_SkillHurtBox == null)
            {
                _SkillHurtBox = TransCtrl.FindChild("SkillCheck").GetComponent<BoxCollider2D>();
            }
            return _SkillHurtBox;
        }
    }
    public float GetGravityScale
    {
        get
        {
            return _GravityScale;
        }
    }
    [SerializeField]
    protected AnimStruct[] _SkillEnum;
    public AnimStruct[] SkillMenue
    {
        get
        {
            return _SkillEnum;
        }
    }
    [SerializeField]
    protected float _MoveSpeed = 10;
    public float MoveSpeed
    {
        get
        {
            return _MoveSpeed;
        }
    }
    BoxCollider2D _ColliderCtrl;
    public BoxCollider2D ColliderCtrl
    {
        get
        {
            if (_ColliderCtrl == null)
            {
                _ColliderCtrl = GetComponent<BoxCollider2D>();
            }
            return _ColliderCtrl;
        }
    }

    Transform _ActorTransCtrl;
    
    int _CurAnimName = 0;
    public int CurAnimName
    {
        get
        {
            return _CurAnimName;
        }
    }
    
    //该属性已废弃
    public Transform ActorTransCtrl
    {
        get
        {
            return TransCtrl;
        }
        
    }

    Animator _AnimCtrl;
    public Animator AnimCtrl
    {
        get
        {
            if( _AnimCtrl == null )
            {
                _AnimCtrl = GetComponentInChildren<Animator>();
            }
            return _AnimCtrl;
        }
    }
    public Transform TransCtrl
    {
        get
        {
            return transform;
        }
    }
    Rigidbody2D _RigidCtrl;
    public Rigidbody2D RigidCtrl
    {
        get
        {
            if( _RigidCtrl == null )
            {
                _RigidCtrl = GetComponent<Rigidbody2D>();
            }
            return _RigidCtrl;
        }
    }
    Transform _FootTransCtrl;
    public Transform FootTransCtrl
    {
        get
        {
            if( _FootTransCtrl == null )
            {
                _FootTransCtrl = TransCtrl.FindChild("FootCheck");
            }
            return _FootTransCtrl;
        }
    }

    BaseState _ActorState;
    public BaseState ActorState
    {
        get
        {
            return _ActorState;
        }
        set
        {
            _ActorState = value;
        }
    }
    bool _IsOnGround = false;
    public virtual bool IsOnGround
    {
        get { return _IsOnGround; }
        set
        {
            if(_IsOnGround != value)
            {
                _IsOnGround = value;
                AnimCtrl.SetBool("IsOnGround", _IsOnGround);
                IsJustOnGround = value;
            }
            
        }
    }

    public virtual void LogicUpdate( )
    {
    }
    float _TimeJustOnGround = 0;
    bool _IsJustOnGround;
    public bool IsJustOnGround
    {
        get
        {
            return _IsJustOnGround;
        }
        set
        {
            if( _IsJustOnGround != value )
            {
                _IsJustOnGround = value;
                AnimCtrl.SetBool("IsJustOnGround", _IsJustOnGround);
                if( _IsJustOnGround )
                {
                    _TimeJustOnGround = Time.time;
                }
            }
        }
    }
    public void Update()
    {
        //着地状态检测
        float Width = ColliderCtrl.size.x;
        Vector2 Position = FootTransCtrl.position;
        Vector2 Size = Vector2.right * (0.4f- 0.01f);
        Size.y = 0.5f;
        Collider2D Collider = Physics2D.OverlapBox(Position, Size, 0, 1);
        if (Collider)
        {
            IsOnGround = true;
        }
        else
        {
            IsOnGround = false;
        }
        //触地检测
        if( IsJustOnGround &&_TimeJustOnGround + 0.2 < Time.time )
        {
            IsJustOnGround = false;
        }
        AnimCtrl.SetFloat("AnimTime",AnimCtrl.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if( CurAnimName != AnimCtrl.GetCurrentAnimatorStateInfo(0).nameHash )
        {
            SwitchState();
        }
        if ( _ActorState != null )
        {
            _ActorState.Update();
        }

        LogicUpdate();
        ActorPropty.ModVIT(1);
    }
    public virtual void SwitchState( )
    {
        string NewStateName = "";
        _CurAnimName = AnimCtrl.GetCurrentAnimatorStateInfo(0).nameHash;
        if ( SkillMenue != null && SkillMenue.Length > 0 )
        {
            foreach (AnimStruct Info in SkillMenue)
            {
                if (AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName(Info.AnimName))
                {
                    NewStateName = Info.ClassName;
                    AnimCtrl.SetFloat("AnimTime", 0);
                    break;
                }
            }
        }
        if (NewStateName == "")
        {
            NewStateName = "DefaultState";
        }
        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        Type GetState = assembly.GetType(NewStateName);
        BaseState NewState = (BaseState)Activator.CreateInstance(GetState, new object[] { this }); // 创建类的实例，返回为 object 类型，需要强制类型转换
        _ActorState = NewState;
    }
    public void Awake()
    {
        _GravityScale = RigidCtrl.gravityScale;
        LogicAwake();
        AnimCtrl.SetInteger("PercentVIT",(int)(ActorPropty.PercentLife * 100));
        AnimCtrl.SetInteger("PercentLife", (int)(ActorPropty.PercentVIT * 100));
        AnimCtrl.SetBool("Inited",true);
    }
    public virtual void LogicAwake( )
    {

    }
    public virtual void Move( Vector2 InDirection )
    {
        RigidCtrl.velocity = InDirection;
    }
    //改变位置
    public virtual void MovePs(Vector2 InDirection)
    {
        Vector3 DirTo3 = Vector3.zero;
        DirTo3.x = InDirection.x;
        DirTo3.y = InDirection.y;
        TransCtrl.position = TransCtrl.position + DirTo3;
    }
    //动画事件
    public void AttackNone()
    {
        ActorState.NoneState();
    }
    //动画事件
    public void AttackStart()
    {
        ActorState.AttackStart();
    }
    //动画事件
    public void Attackting()
    {
        ActorState.Attacking();
    }
    public void AttackEnd()
    {
        ActorState.AttackEnd();
    }
    //击退
    public virtual void HitBack( CutEffect HitEffect = new CutEffect(), Vector2 Direction = new Vector2())
    {
        if( IsHoly )
        {
            return;
        }
        
        if( !IsOnGround)
        {
            HitEffect.RangeTime = HitEffect.RangeTime * 4;
            HitEffect.SpeedRate = 0;
            ClickFly(HitEffect, Direction);
        }else
        {
            BeCut = HitEffect;
            AnimCtrl.SetTrigger("HitBack");
        }
    }
    //击飞
    public virtual bool ClickFly(CutEffect HitEffect = new CutEffect(), Vector2 Direction = new Vector2() )
    {
        if (IsHoly)
        {
            return false;
        }
        ForceMoveDirection = Direction;
        BeCut = HitEffect;
        AnimCtrl.SetTrigger("ClickFly");
        return true;
    }
    //拽取
    public virtual bool DragFly( )
    {
        AnimCtrl.SetTrigger("DragFly");
        return true;
    }

    public virtual void FaceForce( Vector2 InDir)
    {
        Vector3 OldScale = TransCtrl.localScale;
        if (InDir.x * OldScale.x < 0)
        {
            OldScale.x = OldScale.x * -1;
            TransCtrl.localScale = OldScale;
        }
    }
    //面向某个方向
    public virtual void FaceTo(Vector2 InDir)
    {
        if (LockFace)
        {
            return;
        }
        FaceForce(InDir);
    }
    //伤害
    public virtual void Hurt( int InAttack )
    {
        int LeftLife = ActorPropty.DeDuctLife(InAttack);
        AnimCtrl.SetInteger("PercentLife", (int)(ActorPropty.PercentLife * 100));
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Color TextColor = new Color();
        TextColor.a = 255;
        TextColor.r = 255;
        GameObject NewUI = UIEffectMgr.AddHurtInfo(InAttack.ToString(), TextColor, screenPos);
    }

    //扣除体力
    public virtual void CostVIT( int InCostVIT )
    {
        ActorPropty.DeDuctVIT(InCostVIT);
        AnimCtrl.SetInteger("PercentVIT", (int)(ActorPropty.PercentVIT * 100));
    }
}
