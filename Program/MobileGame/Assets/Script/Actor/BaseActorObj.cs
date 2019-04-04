using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GamePhysic;

public abstract class BaseActorObj : MonoBehaviour
{
    #region 内部变量
    PhysicComponent m_Physic;
    [SerializeField]
    [Header("动画事件")]
    protected CharacterAnim m_CharacterAnim;
    //技能判定盒
    [SerializeField]
    [Header("技能判定")]
    BoxCollider2D m_SkillHurtBox;

    
    #endregion
    #region 对外接口
    public PhysicComponent Physic
    {
        get
        {
            return m_Physic;
        }
    }
    public bool IsOnGround
    {
        get
        {
            return m_Physic.IsOnGround;
        }
    }
    public BoxCollider2D SkillHurtBox
    {
        get
        {
            return m_SkillHurtBox;
        }
    }
    #endregion
    #region 流程
    private void Awake()
    {
        _ActionCtrler = new ActionCtrler(this, m_CharacterAnim.Animator);//, info.ActorActionList);
        LogicAwake();
    }
    private void Start()
    {
        m_Physic = GetComponent<PhysicComponent>();
        RegistPhysicEvent();
        RegistAnimEvent();
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
    #region 动画
    protected virtual void RegistAnimEvent()
    {
        CharacterAnim character = m_CharacterAnim;
        character.OnEnterHardTime += this.HardTime;
        character.OnSetImdVSpeed += this.SetImdVSpeed;
        character.OnSetSpeed += this.SetSpeed;
        character.OnSetImdVSpeed += this.SetImdVSpeed;
        character.OnSetImdHSpeed += this.SetImdHSpeed;
        character.OnStopMove += this.StopMove;
        character.OnSetFaceLock += this.SetFaceLock;
        character.OnMoveActor += this.MoveActor;
        character.OnSwitchAction += this.SwitchAction;
        character.OnCallPuppet += this.CallPuppet;
        character.OnBeBreak += this.BeBreak;
        character.OnLeaveComplete += this.LeaveComplete;
        character.OnLeave += this.Leave;
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
        m_CharacterAnim.SetBool("IsOnGround", true);
    }
    public void OnLeaveGround()
    {
        m_CharacterAnim.SetBool("IsOnGround", false);
    }
    public void OnJustOnGroundChange(bool justOnGround)
    {
        m_CharacterAnim.SetBool("IsJustOnGround", justOnGround);
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
