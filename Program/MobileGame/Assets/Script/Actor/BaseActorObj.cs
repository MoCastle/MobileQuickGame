using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GamePhysic;

public abstract class BaseActorObj : MonoBehaviour
{
    #region 内部变量
    [SerializeField]
    [Header("角色物理")]
    PhysicComponent m_Physic;
    [SerializeField]
    [Header("动画控制器")]
    Animator m_Animator;
    [SerializeField]
    [Header("动画事件")]
    protected CharacterAnimEvent m_AnimEvent;
    #endregion
    #region 对外接口
    public PhysicComponent Physic
    {
        get
        {
            return m_Physic;
        }
    }
    #endregion
    #region 流程
    private void Awake()
    {
        ActorInfo info = ActorManager.Mgr.GetActorInfo(0);
        _ActionCtrler = new ActionCtrler(this, GetComponent<Animator>(), info.ActorActionList);
        LogicAwake();
    }
    private void Start()
    {
        m_Physic = GetComponent<PhysicComponent>();
        RegistPhysicEvent();
    }
    

    //出生
    public void Birth()
    {
        ActorPropty.ResetPropty();
        _ActionCtrler.SetTriiger("Birth");
    }
    protected abstract void LogicAwake();

    // Update is called once per frame
    public void Update()
    {
        m_Physic.PhysicUpdate();

        _ActionCtrler.Update();
        LogicUpdate();
        //ActorPropty.ModVIT(1);
    }
    public virtual void LogicUpdate()
    {

    }
    #endregion
    #region 玩家角色信息提取接口 以后可能会挪到非对象脚本里

    public HitEffect BeHitEffect;

    #endregion
    #region 物理
    private void RegistPhysicEvent()
    {
        m_Physic.OnGrond += OnGround;
        m_Physic.OnLeaveGround += OnLeaveGround;
        m_Physic.OnJustOnGroundChange += OnJustOnGroundChange;
    }
    #endregion

    //敌人识别层级
    protected int _IDLayer;
    public int IDLayer
    {
        get
        {
            return _IDLayer;
        }
    }

    //生存状态
    //生命状态
    protected bool _Alive = true;
    public bool Alive
    {
        get
        {
            return _Alive;
        }
    }

    //技能判定盒
    BoxCollider2D _SkillHurtBox;
    public BoxCollider2D SkillHurtBox
    {
        get
        {
            if (_SkillHurtBox == null)
            {
                _SkillHurtBox = TransCtrl.Find("SkillCheck").GetComponent<BoxCollider2D>();
            }
            return _SkillHurtBox;
        }
    }
    BaseCharacter _Character;
    public BaseCharacter Character
    {
        get
        {

            if (_Character == null)
            {
                _Character = new BaseCharacter(this);
                _Character.Propty = this.ActorPropty;
            }
            return _Character;
        }
        set
        {
            _Character = value;
            ActorPropty = Character.Propty;
        }
    }

    [SerializeField]
    [Title("人物属性", "black")]
    public Propty _ActorPropty;
    public Propty ActorPropty
    {
        get
        {
            if (_ActorPropty.ActorInfo.Name == "")
            {
                _ActorPropty.ActorInfo.Name = gameObject.name;
            }
            return _ActorPropty;
        }
        set
        {
            _ActorPropty = value;
        }
    }

    BoxCollider2D _ColliderCtrl;
    public BoxCollider2D BodyCollider
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

    public Transform TransCtrl
    {
        get
        {
            return transform;
        }
    }

    BoxCollider2D _PlatFoot;
    BoxCollider2D PlatFoot
    {
        get
        {
            if (!_PlatFoot)
            {
                _PlatFoot = TransCtrl.Find("PlaneFoot").GetComponent<BoxCollider2D>();
            }
            return _PlatFoot;
        }
    }

    Transform _FootTransCtrl;
    public Transform FootTransCtrl
    {
        get
        {
            if (_FootTransCtrl == null)
            {
                _FootTransCtrl = TransCtrl.Find("FootCheck");
            }
            return _FootTransCtrl;
        }
    }

    public bool _IsOnGround = false;
    public bool IsOnGround
    {
        get
        {
            return _IsOnGround;
        }
        set
        {
            if (_IsOnGround != value)
            {
                _IsOnGround = value;
                _ActionCtrler.SetBool("IsOnGround", _IsOnGround);
                IsJustOnGround = value;
            }
        }
    }
    bool _IsJustOnGround;
    float _TimeJustOnGround;
    public bool IsJustOnGround
    {
        get
        {
            return _IsJustOnGround;
        }
        set
        {
            if (_IsJustOnGround != value)
            {
                _IsJustOnGround = value;
                _ActionCtrler.SetBool("IsJustOnGround", _IsJustOnGround);
                if (_IsJustOnGround)
                {
                    _TimeJustOnGround = Time.time;
                }
            }
        }
    }

    //重新可以踩在平台上
    public void ReOpenPlatFoot()
    {
        PlatFoot.isTrigger = false;

    }
    public void ClosePlatFoot()
    {
        PlatFoot.isTrigger = true;
    }

    ActionCtrler _ActionCtrler;
    public ActionCtrler ActionCtrl
    {
        get
        {
            return _ActionCtrler;
        }
    }

    // Use this for initialization


    #region 向外提供接口
    public Vector2 CurFaceDir
    {
        get
        {
            return Vector2.right * (transform.localScale.x > 0 ? 1 : -1);
        }
    }
    //朝向 y等0时仅改变左右朝向
    public void FaceToDir(Vector2 dir)
    {
        if (dir.x * transform.localScale.x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if (dir.y != 0)
        {
            dir = dir.normalized;
            float Rotate = 0;
            Rotate = Mathf.Atan2(dir.y, Mathf.Abs(dir.x)) * 180 / Mathf.PI;
            if (dir.x < 0)
            {
                Rotate = Rotate * -1;
            }
            Vector3 Rotation = Vector3.forward * Rotate;
            transform.eulerAngles = Rotation;
        }

    }
    #endregion 受击相关
    //扣血相关
    void Hurt(float Damage)
    {
        _ActorPropty.ModLifeValue(Damage * -1);
    }
    #region

    #endregion
    //角色面向防线
    public Vector2 FaceDir
    {
        get
        {
            Vector2 dir = Vector2.right * (transform.localScale.x > 0 ? 1 : -1);
            return dir;
        }
    }
    public Vector3 FaceDir3D
    {
        get
        {
            Vector3 dir = Vector3.right * (transform.localScale.x > 0 ? 1 : -1);
            return dir;
        }
    }
    #region 事件
    public void OnGround()
    {
        m_Animator.SetBool("IsOnGround", true);
    }
    public void OnLeaveGround()
    {
        m_Animator.SetBool("IsOnGround", false);
    }
    public void OnJustOnGroundChange(bool justOnGround)
    {
        m_Animator.SetBool("IsJustOnGround", _IsJustOnGround);
    }
    #endregion
    #region 动画事件
    public void DebugInfo()
    {
    }
    public bool IsHoly = false;
    public virtual void BeAttacked(BaseActorObj attacker, Vector3 position, HitEffect hitEffect, float Damage = 0)
    {
        if (IsHoly)
        {
            return;
        }
        //受击特效
        if (_ActorPropty.ActorInfo.BloodName != null && _ActorPropty.ActorInfo.BloodName != "")
        {
            GameObject blood = EffectManager.Manager.GenEffect(_ActorPropty.ActorInfo.BloodName);
            float dir = attacker.transform.position.x - this.transform.position.x;
            Vector3 scale = blood.transform.localScale;
            if (scale.x * dir < 0)
            {
                scale.x *= -1;
                blood.transform.localScale = scale;
            }
            blood.transform.position = position;
        }
        Vector2 faceDir = attacker.transform.position - transform.position;
        faceDir.y = 0;
        FaceToDir(faceDir);
        Hurt(Damage);
        if (_ActorPropty.IsDeath)
        {
            _ActionCtrler.SetTriiger("Death");
            Character.OnDeath();
        }
        //受击效果
        BeHitEffect = hitEffect;
        //动画状态处理
        switch (hitEffect.HitType)
        {
            case HitEffectType.HitBack:
                _ActionCtrler.SetTriiger("HitBack");

                break;
            case HitEffectType.ClickFly:
                _ActionCtrler.SetTriiger("ClickFly");

                break;
        }

    }
    public virtual void EnterState()
    {
    }

    //通知硬直事件
    public void HardTime(float hardTime)
    {
        _ActionCtrler.CurAction.HardTime(hardTime);
    }

    //设置运动速度 匀速运动
    public void SetSpeed(float speed)
    {
        _ActionCtrler.CurAction.SetSpeed(speed);
    }

    //设置瞬时垂直速度
    public void SetImdVSpeed(float speed)
    {
        m_Physic.SetSpeed(new Vector2(m_Physic.MoveSpeed.x, speed));
    }

    //设置瞬时水平速度
    public void SetImdHSpeed(float speed)
    {
        m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
    }

    //设置空中瞬时垂直速度
    public void SetInAirImdVSpeed(float speed)
    {
        if (this.IsOnGround)
        {
            return;
        }
        m_Physic.SetSpeed(new Vector2(m_Physic.MoveSpeed.x, speed));
    }
    //设置空中瞬时水平速度
    public void SetInAirImdHSpeed(float speed)
    {
        if (this.IsOnGround)
        {
            return;
        }
        m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
    }

    public void StopMove(float speed)
    {
        if (-0.01f < speed && speed < 0.01f)
            _ActionCtrler.CurAction.SetSpeed(0);
        else
            _ActionCtrler.CurAction.SetFinalSpeed(speed);

    }
    public void SetFaceLock(int ifLock)
    {
        _ActionCtrler.CurAction.SetFaceLock(ifLock == 1);
    }
    //瞬移
    public void MoveActor(float distance)
    {
        transform.position += FaceDir3D * distance;

    }
    //转换状态
    public virtual void SwitchAction()
    {
    }
    //召唤
    public virtual void CallPuppet(string objName)
    {
        BaseActorObj Obj = ActorManager.Mgr.GenActor(objName);
        PuppetNpc puppet = Obj as PuppetNpc;
        _ActionCtrler.CurAction.CallPuppet(puppet);

    }
    //被打断
    public virtual void BeBreak()
    {

    }
    //离开完成 执行离开事件
    public virtual void LeaveComplete()
    {

    }
    public void Leave()
    {

    }
    #endregion
}
