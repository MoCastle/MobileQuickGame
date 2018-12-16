using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;

struct ActorSaveInfo
{
    Vector2 Position;
    Propty ActorPropty;
}

[System.Serializable]
public struct AnimStruct
{
    public string AnimName;
    public int ActionID;
}
public struct CutEffect
{
    public float RangeTime;
    public float SpeedRate;
}
public enum ActorType
{
    Enemy,
    Player
}
public abstract class BaseActor : MonoBehaviour {
    [SerializeField]
    [Title("角色类型", "black")]
    ActorType _Type;
    public ActorType Type
    {
        get
        {
            return _Type;
        }
    }
    //平台脚相关
    #region
    public BoxCollider2D _PlatFoot;
    public BoxCollider2D PlatFoot
    {   
        get
        {
            if( !_PlatFoot )
            {
                _PlatFoot = TransCtrl.FindChild("PlaneFoot").GetComponent<BoxCollider2D>();
            }
            return _PlatFoot;
        }
    }
    public bool _OnPlat;
    public bool OnPlat
    {
        get
        {
            return _OnPlat;
        }
    }
    //重新可以踩在平台上
    public void ReOpenPlatFoot( )
    {
        PlatFoot.isTrigger = false;

    }
    public void ClosePlatFoot( )
    {
        PlatFoot.isTrigger = true;
    }

    #endregion
    #region
    //当前ID
    static int TotalActorID = 0;
    public int _ActorID;
    public int ActorID
    {
        get
        {
            return _ActorID;
        }
    }

    BaseDir CenceDir;
    #endregion

    public delegate void Action();
    public event Action DeathEvent;
    public void AddDeathEvent( Action InFunction )
    {
        DeathEvent += InFunction;
    }

    GameCtrl _GameCtr;
    GameCtrl GameCtrler
    {
        get
        {
            if(_GameCtr == null)
            {
                _GameCtr = GameCtrl.GameCtrler;
            }
            return _GameCtr;
        }
    }
    //生命状态
    protected bool _Alive = true;
    public bool Alive
    {
        get
        {
            return _Alive;
        }
    }
    //受击效果
    public CutEffect BeCut;
    public Vector2 ForceMoveDirection = Vector2.up;
    [Title("是正否处于无敌状态", "black")]
    public bool IsHoly;
    [Title("攻击移动距离 废弃", "black")]
    public float CAttackMove = 1;
    [Title("攻击时移动速度", "black")]
    public float LAttackSpeed = 3;
    public bool LockFace;
    float _GravityScale;
    public Vector2 HitMoveDir;
    BoxCollider2D _SkillHurtBox;

    [Title("人物属性", "black")]
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
    [Title("技能与动画列表", "black")]
    protected AnimStruct[] _SkillEnum;
    public AnimStruct[] SkillMenue
    {
        get
        {
            return _SkillEnum;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return ActorPropty.MoveSpeed;
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
    
    public Transform ActorTransCtrl
    {
        get
        {
            return TransCtrl;
        }
        
    }

    //除非为了省事 尽量不要直接使用动画状态机
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

    //动画状态机封装接口 有事用这个
    AnimAdaptor _AnimAdaptor;
    public AnimAdaptor AnimAdaptor
    {
        get
        {
            if( _AnimAdaptor == null )
            {
                _AnimAdaptor = new AnimAdaptor(GetComponentInChildren<Animator>());
            }
            return _AnimAdaptor;
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
        Debug.Log("Before: " + gameObject.name + "-" + "Update");
        Debug.Log(transform.position);
        if (GameCtrler.IsPaused)
        {
            return;
        }
        //着地状态检测
        float Width = ColliderCtrl.size.x;
        Vector2 Position = FootTransCtrl.position;
        Vector2 Size = Vector2.right * (0.4f- 0.01f);
        Size.y = 0.5f;
        LayerMask Layer = 1 << LayerMask.NameToLayer("Ground");
        if(!PlatFoot.isTrigger)
        {
            Layer += 1 << LayerMask.NameToLayer("PlatForm");
        }
        
        Collider2D Collider = Physics2D.OverlapBox(Position, Size, 0, Layer);
        if (Collider)
        {
            //判断角色是否有可以站在平台上的脚
            IsOnGround = true;

            //检查脚下的是不是平台
            if( Collider.gameObject.layer == 1 << LayerMask.NameToLayer("PlatForm"))
            {
                _OnPlat = true;
            }
        }
        else
        {
            IsOnGround = false;
            _OnPlat = false;
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
        

    }
    public virtual void SwitchState( )
    {
        if( _ActorState!=null )
        {
            _ActorState.CompleteFunc();
        }
        
        string NewStateName = "";
        _CurAnimName = AnimCtrl.GetCurrentAnimatorStateInfo(0).nameHash;
        //先检查是否符合自己的设定
        if ( (SkillMenue != null) && SkillMenue.Length > 0 )
        {
            foreach (AnimStruct Info in SkillMenue)
            {
                if (AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName(Info.AnimName))
                {
                    NewStateName = "";//Info.ClassName;
                    AnimCtrl.SetFloat("AnimTime", 0);
                    break;
                }
            }
        }
        //再检查是否符合通用技能设定
        if (NewStateName == "" && (SkillManager.Obj._SkillEnum != null) && SkillManager.Obj._SkillEnum.Length > 0)
        {
            foreach (AnimStruct Info in SkillManager.Obj._SkillEnum)
            {
                if (AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName(Info.AnimName))
                {
                    NewStateName = "";//Info.ClassName;
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
    public void OnEnable( )
    {
        LogicEnable();
    }
    public virtual void LogicEnable( )
    {

    }

    public void Awake()
    {
        _ActorID = TotalActorID;
        TotalActorID = TotalActorID + 1;

        //设置初始生命状态
        _Alive = true;
        _GravityScale = RigidCtrl.gravityScale;
        LogicAwake();
        AnimCtrl.SetInteger("PercentVIT",(int)(ActorPropty.PercentLife * 100));
        AnimCtrl.SetInteger("PercentLife", (int)(ActorPropty.PercentVIT * 100));
        AnimCtrl.SetBool("Inited",true);
    }

    public virtual void LogicAwake( )
    {

    }
    public void Start()
    {
        LogicStart();
    }
    public virtual void LogicStart()
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
        /*
        if( !IsOnGround)
        {
            HitEffect.RangeTime = HitEffect.RangeTime * 4;
            HitEffect.SpeedRate = 0;
            ClickFly(HitEffect, Direction);
        }else
        {
            BeCut = HitEffect;
            AnimCtrl.SetTrigger("HitBack");
        }*/
        BeCut = HitEffect;
        AnimCtrl.SetTrigger("HitBack");
        BeBreak();
    }
    //击飞
    public virtual bool ClickFly(CutEffect HitEffect = new CutEffect(), Vector2 Direction = new Vector2() )
    {
        
        if (IsHoly)
        {
            return false;
        }
        BeBreak();
        ForceMoveDirection = Direction;
        BeCut = HitEffect;
        if (AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName("ClickFly"))
        {
            SwitchState();
        }
        else
        {
            AnimCtrl.SetTrigger("ClickFly");
        }
        
        return true;
    }
    //拽取
    public virtual bool DragFly( )
    {
        AnimCtrl.SetTrigger("DragFly");
        return true;
    }
    //面向方向
    public Vector2 FaceDirection
    {
        get
        {
            Vector2 Direction = Vector2.right;
            if(TransCtrl.localScale.x* Direction.x < 0)
            {
                Direction.x = Direction.x * -1;
            }
            return Direction;
        }
    }

    public virtual void FaceForce( Vector2 InDir)
    {
        Vector3 OldScale = TransCtrl.localScale;
        if (InDir.x * (OldScale.x*10) < 0.01)
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
        if( LeftLife < 0.0001f )
        {
            Death();
        }
    }

    //扣除体力
    public virtual void CostVIT( int InCostVIT )
    {
        ActorPropty.DeDuctVIT(InCostVIT);
        //AnimCtrl.SetInteger("PercentVIT", (int)(ActorPropty.PercentVIT * 100));
    }

    //死亡
    public virtual void Death( )
    {
        _Alive = false;
        AnimCtrl.SetTrigger("Death");
        if( DeathEvent!=null )
        {
            DeathEvent();
        }
        
    }

    public virtual void BeBreak()
    {
        
    }
}
