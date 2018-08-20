using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputInfo
{
    public Vector2 Shift;
    public bool IsLegal;
    public bool IsPushing;
    public float MaxDst;
    public Vector2 EndPs;
    public InputInfo( bool InIsLegal = false )
    {
        Shift = Vector2.zero;
        IsLegal = InIsLegal;
        IsPushing = false;
        MaxDst = 0;
        EndPs = Vector2.zero;
    }
    public float Percent
    {
        get
        {
            if( MaxDst>0 )
            {
                return Shift.magnitude / MaxDst;
            }
            return 0;
        }
    }
    public float XPercent
    {
        get
        {
            if (MaxDst > 0)
            {
                return Mathf.Abs( Shift.x / MaxDst );
            }
            return 0;
        }
    }
    public float YPercent
    {
        get
        {
            if (MaxDst > 0)
            {
                return Mathf.Abs( Shift.y / MaxDst );
            }
            return 0;
        }
    }
}

public enum SkillEnum
{
    Idle,//闲置状态类
    Run,//玩家跑
    Dash,//玩家冲
    Attack,
    AttackFirst,//一阶打击
    AttackSecond,//二阶打击
    AttackThird,//三阶打击
    Falling,//坠落
    FallingEnd,//着地
    RocketCut,//升龙击
    ImpactCut_pre,//
    ImpactCut_Falling,
    ImpactCut_Damage,
    Default,//默认状态
}
public enum HurtTypeEnum
{
    Normal
}
public enum HitTypeEnum
{
    HitBack,
    ClickFly,
    None
}

public abstract class BaseState {
    public virtual Vector2 ClickFly
    {
        get
        {
            return Vector2.zero;
        }
    }
    HitTypeEnum _HitType;
    //体力消耗量 需要定义的话在对应的技能里重写
    public virtual int CostVITNum
    {
        get
        {
            return 50;
        }
    }
    float _CutTime = 0;
    public float CutTime
    {
        get
        {
            return _CutTime;
        }
        set
        {
            _CutTime = value;
        }
    }

    //持续时间
    float _RangeTime = 0.2f;
    public virtual float RangeTime
    {
        get
        {
            return _RangeTime;
        }
        set
        {
            _RangeTime = value;
        }
    }
    //减速后的时间速率
    float _SpeedRate = 0f;
    public virtual float SpeedRate
    {
        get
        {
            return _SpeedRate;
        }
        set
        {
            _SpeedRate = value;
        }
    }
    //已受击列表
    Dictionary<BaseActor, int> AttackedList;
    protected AttackEnum AttackTingState;
    protected enum AttackEnum
    {
        Start,
        Attackting,
        AttackEnd,
        None,
    }
    public virtual int Layer
    {
        get
        {
            return 1;
        }
    }
    protected Vector2 OldSpeed;
    protected float gravityScale;
    protected BaseActor _Actor;
    public virtual Vector2 Direction
    {
        get
        {
            Vector2 ReturnVect = Vector2.left;
            if (ReturnVect.x * _Actor.transform.localScale.x < 0)
            {
                ReturnVect.x = ReturnVect.x * -1;
            }
            return ReturnVect;
        }
    }
    public abstract SkillEnum SkillType
    {
        get;
    }
    public virtual HurtTypeEnum HurtType
    {
        get
        {
            return HurtTypeEnum.Normal;
        }
    }
    public BaseState(BaseActor InActor )
    {
        _Actor = InActor;
        _Actor.LockFace = false;
        AttackStart();
        AttackedList = new Dictionary<BaseActor, int>();
        _Actor.AnimCtrl.speed = 1;
        _Actor.ActorTransCtrl.localEulerAngles = Vector3.zero;
        _Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
        _Actor.IsHoly = false;
        CostVIT();
    }
    // Use this for initialization
    public virtual void Update()
    {
        JugeStateActive();
        if (_Actor.SkillHurtBox.enabled)
        {
            SkillAttack();
        }
        //卡肉时间过 重置
        CutMeat();
    }
    //攻击状态抉择
    public virtual void JugeStateActive( )
    {
        switch (AttackTingState)
        {
            case AttackEnum.Start:
                IsStarting();
                break;
            case AttackEnum.Attackting:
                IsAttackting();
                break;
            case AttackEnum.AttackEnd:
                IsAttackEnding();
                break;
            default:
                IsNoneState();
                break;
        }
    }
    //卡肉效果
    public virtual void CutMeat()
    {
        if (CutTime > 0)
        {
            if (CutTime < Time.time)
            {
                _Actor.AnimCtrl.speed = 1;
                CutTime = 0;
            }
            else
            {
                _Actor.RigidCtrl.velocity = _Actor.RigidCtrl.velocity * SpeedRate;
            }
        }
    }
    public virtual void IsStarting()
    {
        _Actor.LockFace = true;
    }
    public virtual void IsAttackting()
    {
        _Actor.RigidCtrl.velocity = Vector2.zero;
        _Actor.RigidCtrl.gravityScale = 0;
    }
    public virtual void IsAttackEnding()
    {
    }
    public virtual void IsNoneState( )
    {

    }
    
    //进入前摇
    public virtual void AttackStart()
    {
        AttackTingState = AttackEnum.None;
    }
    //进入后摇
    public virtual void Attacking()
    {
        AttackTingState = AttackEnum.Attackting;
    }
    //结束
    public virtual void AttackEnd()
    {
        AttackTingState = AttackEnum.AttackEnd;
        _Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
    }
    public virtual void NoneState()
    {
        AttackTingState = AttackEnum.None;
    }
    // Update is called once per frame
    public virtual void SkillAttack()
    {
        Collider2D[] TargetList = AttackList();
        foreach (Collider2D TargetCollider in TargetList)
        {
            if( TargetCollider == null )
            {
                return;
            }
            BaseActor TargetActor = TargetCollider.GetComponent<BaseActor>();
            if( TargetActor!= null && TargetActor.Alive && !AttackedList.ContainsKey(TargetActor) )
            {
                AttackedList.Add(TargetActor, 1);
                SkillEffect( TargetActor);
                SetCutMeet();
                //产生伤害
                MakeHurt(TargetActor);
            }
        }
    }
    public virtual void MakeHurt(BaseActor TargetActor)
    {
        float Damage = SkillManager.GetAttPercent(this.ToString())/100 * _Actor.ActorPropty.Attack;
        float CrtPercent = SkillManager.GetCritPercent(this.ToString());
        float RandResult = Random.Range(0, 1f);
        //算暴击伤害
        if(RandResult < CrtPercent)
        {
            Damage = Damage * 2;
        }
       
        TargetActor.Hurt(_Actor.ActorPropty.Attack);
    }
    //设置卡肉状态
    public virtual void SetCutMeet( )
    {
        CutTime = Time.time + RangeTime;
        _Actor.AnimCtrl.speed = SpeedRate;
    }
    public virtual Collider2D[] AttackList()
    {
        Collider2D[] ColliderList = new Collider2D[100];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        ContactFilter.SetLayerMask(Layer);
        _Actor.SkillHurtBox.OverlapCollider(ContactFilter, ColliderList);
        return ColliderList;
    }

    public virtual void SkillEffect( BaseActor TargetActor )
    {
        Vector2 FaceToVect = (_Actor.FootTransCtrl.position - TargetActor.FootTransCtrl.position );
        TargetActor.FaceForce(FaceToVect);
        TargetActor.HitMoveDir = Direction.normalized;

        AttackEffect(TargetActor);
        ReleaseEffect(TargetActor);
    }

    //攻击效果
    public virtual void AttackEffect( BaseActor TargetActor )
    {
        CutEffect Cut = new CutEffect();
        Cut.RangeTime = RangeTime;
        Cut.SpeedRate = SpeedRate;
        TargetActor.HitBack(Cut, ClickFly);
    }
    //释放特效
    public virtual void ReleaseEffect(BaseActor TargetActor)
    {
        Vector3 Offset = Vector3.zero;
        Offset.x = _Actor.SkillHurtBox.offset.x;
        Offset.y = _Actor.SkillHurtBox.offset.y;
        Vector3 EffectPS = _Actor.SkillHurtBox.transform.position + Offset;
        Offset = Vector3.zero;
        Offset.x = TargetActor.ColliderCtrl.offset.x;
        Offset.y = TargetActor.ColliderCtrl.offset.y;
        EffectPS = TargetActor.ColliderCtrl.transform.position + Offset + EffectPS;
        EffectPS = EffectPS * 0.5f;
        GameObject Effect = EffectManager.Manager.GenEffect("chong_qibo");
        Effect.transform.position = EffectPS;
    }
    //根据运动方向旋转
    public void RotateToDirection(Vector2 MoveDirection)
    {
        Vector2 NormDir = MoveDirection.normalized;
        float Rotate = 0;
        Rotate = Mathf.Atan2(NormDir.y, Mathf.Abs(NormDir.x)) * 180 / Mathf.PI;
        if (NormDir.x < 0)
        {
            Rotate = Rotate * -1;
        }
        Vector3 Rotation = Vector3.forward * Rotate;
        _Actor.ActorTransCtrl.eulerAngles = Rotation;
    }
    //消耗体力
    protected virtual void CostVIT( )
    {
        _Actor.CostVIT(CostVITNum);
    }

    //状态完结
    public virtual void CompleteFunc()
    { }
}
