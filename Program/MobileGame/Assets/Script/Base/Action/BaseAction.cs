using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction {
    SkillInfo _SkillInfo;
    protected Propty _ActorPropty;
    #region 生命周期

    public BaseAction(BaseActorObj baseActorObj, SkillInfo skillInfo)
    {
        /*
        AttackedList = new Dictionary<BaseActor, int>();
        _Actor.AnimCtrl.speed = 1;
        _Actor.ActorTransCtrl.localEulerAngles = Vector3.zero;
        _Actor.RigidCtrl.gravityScale = _Actor.GetGravityScale;
        _Actor.IsHoly = false;
        _SkillEffect = SkillManager.GenEffect();
        CostVIT();*/
        _SkillInfo = skillInfo;
        _ActionCtrl = baseActorObj.ActionCtrl;
        _ActorObj = baseActorObj;
        _ActorPropty = _ActorObj._ActorPropty;
        _AttackDict = new Dictionary<BaseActorObj, int>();
    }
    //状态完结
    public virtual void CompleteFunc()
    {
        _AttackDict.Clear();
        _ActorObj.PhysicCtrl.ResetData();
        _InputDIr = Vector2.zero;
    }
    
    // 每帧更新
    public virtual void Update()
    {
        /*
        JugeStateActive();
        */
        if (_ActorObj.SkillHurtBox.enabled)
        {
            SkillAttack();
        }

        //卡肉
        CutMeat();


        //硬直效果
        if (_HardTime != 0)
        {
            if (_HardTime > Time.time)
            {
                _ActionCtrl.AnimSpeed = 0;
            }
            else
            {
                _ActionCtrl.AnimSpeed = 1;
                _HardTime = 0;
            }
        }
        //方向
        ChangeDir();
        Move();
        LogicUpdate();
    }

    //逻辑每帧事件
    public virtual void LogicUpdate()
    {

    }
    #endregion

    int _ID;
    protected SkillEffect _SkillEffect;
    protected float _CutTimeClock;
    public float CutTime
    {
        get
        {
            return _SkillEffect.CutTime;
        }
    }

    protected virtual float HitBackValue
    {
        get
        {
            return _SkillEffect.HitBackValue;
        }
    }

    public virtual Vector2 ClickFly
    {
        get
        {
            return Vector2.zero;
        }
    }

    //体力消耗量 需要定义的话在对应的技能里重写
    public virtual int CostVITNum
    {
        get
        {
            return 50;
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
    Dictionary<BaseActorObj, int> _AttackDict;
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
            return _ActorObj.IDLayer;
        }
    }
    protected Vector2 OldSpeed;
    protected float gravityScale;
    protected BaseActorObj _ActorObj;
    public virtual Vector2 Direction
    {
        get
        {
            Vector2 ReturnVect = Vector2.left;
            if (ReturnVect.x * _ActorObj.transform.localScale.x < 0)
            {
                ReturnVect.x = ReturnVect.x * -1;
            }
            return ReturnVect;
        }
    }
    public virtual SkillEnum SkillType
    {
        get
        {
            return SkillEnum.Default;
        }
    }
    public virtual HurtTypeEnum HurtType
    {
        get
        {
            return HurtTypeEnum.Normal;
        }
    }
   
    
    //攻击状态抉择
    public virtual void JugeStateActive()
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
        if (_CutTimeClock > 0)
        {
            if (_CutTimeClock < Time.time)
            {
                _ActionCtrl.AnimSpeed = 1;
                _ActorObj.PhysicCtrl.ResetData();
                _CutTimeClock = 0;
            }
            else
            {
                _ActionCtrl.AnimSpeed = 0;
                _ActorObj.PhysicCtrl.CopyData();
                //_ActorObj.RigidCtrl.velocity = _ActorObj.RigidCtrl.velocity * SpeedRate;
            }
        }
    }
    public virtual void IsStarting()
    {
        //_ActorObj.LockFace = true;
    }
    public virtual void IsAttackting()
    {
        //_Actor.RigidCtrl.velocity = Vector2.zero;
       // _ActorObj.RigidCtrl.gravityScale = 0;
    }
    public virtual void IsAttackEnding()
    {
    }
    public virtual void IsNoneState()
    {

    }

    // Update is called once per frame
    public virtual void SkillAttack()
    {
        Collider2D[] TargetList = AttackList();
        foreach (Collider2D TargetCollider in TargetList)
        {
            if (TargetCollider == null)
            {
                return;
            }
            BaseActorObj TargetActor = TargetCollider.GetComponent<BaseActorObj>();
            if (TargetActor != null && TargetActor.Alive && !_AttackDict.ContainsKey(TargetActor))
            {
                _AttackDict.Add(TargetActor, 1);
                Vector3 effectPs = CountHurtPs(_ActorObj.SkillHurtBox, TargetCollider);
                GenEffect(effectPs);
                //SkillEffect(TargetActor);
                SetCutMeet(TargetActor);
                //产生伤害
                //MakeHurt(TargetActor);
                TargetActor.BeAttacked(_ActorObj,effectPs);
            }
        }
    }
    //生成特效
    public void GenEffect( Vector3 position )
    {
        //string effectName = skill
        if (_SkillInfo.EffectName == null || _SkillInfo.EffectName == "")
            return;
        GameObject effect = EffectManager.Manager.GenEffect(_SkillInfo.EffectName);
        effect.transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 180));
        effect.transform.position = position;
    }

    //计算伤害位置
    public Vector3 CountHurtPs( Collider2D attacker, Collider2D attacked )
    {
        Vector3 Offset = Vector3.zero;
        Offset.x = _ActorObj.SkillHurtBox.offset.x;
        Offset.y = _ActorObj.SkillHurtBox.offset.y;
        Vector3 EffectPS = _ActorObj.SkillHurtBox.transform.position + Offset;

        Offset = Vector3.zero;
        Offset.x = attacked.offset.x;
        Offset.y = attacked.offset.y;
        EffectPS += attacked.transform.position + Offset;
        EffectPS *= 0.5f;
        GameObject Effect = EffectManager.Manager.GenEffect("Hit");
        
        /*
        GameObject Blood = EffectManager.Manager.GenEffect("Blood");
        Blood.transform.position = Effect.transform.position;
        Vector3 OldScale = Blood.transform.localScale;
        OldScale.x = _ActorObj.transform.localScale.x * OldScale.x < 0 ? OldScale.x : OldScale.x * -1;
        Blood.transform.localScale = OldScale;
        */
        return EffectPS;
    }
    public virtual void MakeHurt(BaseActor TargetActor)
    {
        float Damage = SkillManager.GetAttPercent(this.ToString()) / 100;// * _ActorObj.ActorPropty.Attack;
        float CrtPercent = SkillManager.GetCritPercent(this.ToString()) / 1000;
        float RandResult = Random.Range(0, 1f);
        //算暴击伤害
        if (RandResult < CrtPercent)
        {
            Damage = Damage * 2;
        }

        TargetActor.Hurt((int)Damage);
    }
    //设置卡肉状态
    public virtual void SetCutMeet( BaseActorObj TargetActor )
    {
        float rangeTime = _SkillInfo.CutMeet + TargetActor._ActorPropty.Heavy*_SkillInfo.CutMeetRate;
        _CutTimeClock = Time.time + rangeTime;
        //_ActorObj.AnimCtrl.speed = SpeedRate;
    }
   
    
    //消耗体力
    protected virtual void CostVIT()
    {
       // _ActorObj.CostVIT(CostVITNum);
    }
    #region 攻击效果
    public virtual Collider2D[] AttackList()
    {
        Collider2D[] ColliderList = new Collider2D[100];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        ContactFilter.SetLayerMask(Layer);
        _ActorObj.SkillHurtBox.OverlapCollider(ContactFilter, ColliderList);
        return ColliderList;
    }

    public virtual void SkillEffect(BaseActor TargetActor)
    {
        Vector2 ImpactDir = TargetActor.FootTransCtrl.position - _ActorObj.FootTransCtrl.position;
        ImpactDir.y = 0;
        if (Mathf.Abs(ImpactDir.x) < _ActorObj.BodyCollider.bounds.size.x / 2)
        {
            ImpactDir = Direction;
        }

        ImpactDir = ImpactDir.normalized;
        Vector2 FaceToVect = ImpactDir;
        FaceToVect.x = FaceToVect.x * -1;
        TargetActor.FaceForce(FaceToVect);
        TargetActor.HitMoveDir = ImpactDir;

        AttackEffect(TargetActor);
        ReleaseEffect(TargetActor);
    }

    //攻击效果
    public virtual void AttackEffect(BaseActor TargetActor)
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
        //Offset.x = _ActorObj.SkillHurtBox.offset.x;
        //Offset.y = _ActorObj.SkillHurtBox.offset.y;
        // Vector3 EffectPS = _ActorObj.SkillHurtBox.transform.position + Offset;
        Offset = Vector3.zero;
        Offset.x = TargetActor.ColliderCtrl.offset.x;
        Offset.y = TargetActor.ColliderCtrl.offset.y;
        //EffectPS = TargetActor.ColliderCtrl.transform.position + Offset + EffectPS;
        // EffectPS = EffectPS * 0.5f;
        GameObject Effect = EffectManager.Manager.GenEffect("Hit");
        // Effect.transform.position = EffectPS;
        //随机旋转角度
        Effect.transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 180));

        GameObject Blood = EffectManager.Manager.GenEffect("Blood");
        Blood.transform.position = Effect.transform.position;
        Vector3 OldScale = Blood.transform.localScale;
        OldScale.x = _ActorObj.transform.localScale.x * OldScale.x < 0 ? OldScale.x : OldScale.x * -1;
        Blood.transform.localScale = OldScale;
    }
    #endregion

    #region 向量输入
    //运动方向
    protected virtual Vector2 MoveDir
    {
        get
        {
            return Vector2.right * (_ActorObj.transform.localScale.x > 0 ? 1 : -1);
        }
    }

    //速度
    protected float _Speed =0;
    //向某一方向移动
    public void SetSpeed( float speed )
    {
        _Speed = speed;
    }
    protected virtual void Move()
    {
        if(_CutTimeClock <= 0 &&(_Speed * _Speed)>0)
            _ActorObj.PhysicCtrl.SetSpeed(MoveDir.normalized*_Speed);
    }
    //方向锁
    public bool _DirLock;
    protected Vector2 _InputDIr;
    //输入向量
    public void InputDirect(Vector2 dirction)
    {
        _InputDIr = dirction;
        ChangeDir();
    }
    //转向
    protected void ChangeDir()
    {
        Vector3 scale = _ActorObj.transform.localScale;
        if (!_DirLock && _InputDIr.x * scale.x < 0)
        {
            scale.x *= -1;
            _ActorObj.transform.localScale = scale;
            _InputDIr.x = 0;
        }

    }
    #endregion

    

    #region 动画事件

    //朝向锁
    public void SetFaceLock( bool ifLock )
    {
        _DirLock = ifLock;
    }
    float _HardTime = 0;
    ActionCtrler _ActionCtrl;
    //硬直时间
    public void HardTime(float time)
    {
        _HardTime = Time.time + time;
        _ActionCtrl.AnimSpeed = 0;
    }
    public void CallPuppet( PuppetNpc puppet )
    {
        Vector3 newPs = _ActorObj.transform.position;
        newPs.y += _ActorObj.BodyCollider.offset.y;
        puppet.transform.position = newPs;
        puppet.FaceToDir(_ActorObj.FaceDir);
        puppet.Master = _ActorObj;
        puppet.SetIDLayer(_ActorObj.IDLayer);
        
    }
    #endregion

    #region 通用功能
    //根据运动方向旋转
    public void RotateToDirection(Vector2 faceDir)
    {
        faceDir = faceDir.normalized;
        float Rotate = 0;
        Rotate = Mathf.Atan2(faceDir.y, Mathf.Abs(faceDir.x)) * 180 / Mathf.PI;
        if (faceDir.x < 0)
        {
            Rotate = Rotate * -1;
        }
        Vector3 Rotation = Vector3.forward * Rotate;
        _ActorObj.transform.eulerAngles = Rotation;
    }

    #endregion
}
