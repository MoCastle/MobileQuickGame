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

public abstract class BaseActor : MonoBehaviour {
    [SerializeField]
    protected AnimStruct[] _SkillEnum;
    public AnimStruct[] SkillMenue
    {
        get
        {
            return _SkillEnum;
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
    
    string _CurAnimName = "None";
    public string CurAnimName
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
            if( _ActorTransCtrl == null )
            {
                Transform NewTransCtrl = TransCtrl.FindChild("Actor");
                _ActorTransCtrl = NewTransCtrl;
            }
            return _ActorTransCtrl;
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
        //检测动画的运行状态
        if ( !AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName( CurAnimName ) )
        {
            if( SkillMenue != null && SkillMenue.Length > 0 )
            {
                foreach( AnimStruct Info in SkillMenue )
                {
                    if(AnimCtrl.GetCurrentAnimatorStateInfo(0).IsName(Info.AnimName))
                    {
                        _CurAnimName = Info.AnimName;
                        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
                        Type State = assembly.GetType(Info.ClassName);
                        BaseState NewState = (BaseState)Activator.CreateInstance(State,new object[]{this}); // 创建类的实例，返回为 object 类型，需要强制类型转换
                        _ActorState = NewState;
                        break;
                    }
                }
            }
        }
        
        if ( _ActorState != null )
        {
            _ActorState.Update();
        }

        LogicUpdate();

    }
    public void Awake()
    {
        LogicAwake();
    }
    public virtual void LogicAwake( )
    {

    }
}
