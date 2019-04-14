using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
using BaseFunc;
public class BaseAction : BaseState
{
    #region 内部成员
    SkillInfo m_SkillInfo;
    protected Propty m_ActorPropty;
    protected int m_ID;
    protected float m_CutTimeClock;
    protected float m_RangeTime = 0.2f;
    //减速后的时间速率
    protected float m_SpeedRate = 0f;
    //已受击列表
    Dictionary<BaseActorObj, int> m_AttackDict;
    protected Vector2 m_OldSpeed;
    protected float m_GravityScale;
    protected BaseActorObj m_ActorObj;
    //速度
    protected float m_HSpeed;
    protected float m_VSpeed;
    protected Vector2 m_MoveSpeed;
    //移动
    //方向锁
    public bool m_DirLock;
    protected Vector2 m_InputDIr;
    private float m_HardTime;
    ActionCtrler m_ActionCtrl;
    #endregion
    #region 接口
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
    public virtual float RangeTime
    {
        get
        {
            return m_RangeTime;
        }
        set
        {
            m_RangeTime = value;
        }
    }
    public virtual float SpeedRate
    {
        get
        {
            return m_SpeedRate;
        }
        set
        {
            m_SpeedRate = value;
        }
    }
    public virtual int Layer
    {
        get
        {
            return m_ActorObj.IDLayer;
        }
    }
    public virtual Vector2 Direction
    {
        get
        {
            Vector2 ReturnVect = Vector2.left;
            if (ReturnVect.x * m_ActorObj.transform.localScale.x < 0)
            {
                ReturnVect.x = ReturnVect.x * -1;
            }
            return ReturnVect;
        }
    }
    #endregion
    #region 流程
    //运动方向
    protected virtual Vector2 MoveDir
    {
        get
        {
            return Vector2.right * (m_ActorObj.transform.localScale.x > 0 ? 1 : -1);
        }
    }

    public BaseAction(BaseActorObj baseActorObj, SkillInfo skillInfo)
    {
        /*
        CostVIT();*/
        m_SkillInfo = skillInfo;
        m_ActionCtrl = baseActorObj.ActionCtrl;
        m_ActorObj = baseActorObj;
        m_ActorPropty = m_ActorObj._ActorPropty;
        m_AttackDict = new Dictionary<BaseActorObj, int>();
    }
    //状态完结
    public virtual void CompleteFunc()
    {
        m_AttackDict.Clear();
        m_InputDIr = Vector2.zero;
        m_ActionCtrl.AnimSpeed = 1;
        m_DirLock = false;
    }

    // 每帧更新
    public override void Update()
    {
        SkillAttack();
        //卡肉
        HardTimeCount();
        //方向
        ChangeDir();
        Move();
        LogicUpdate();
    }
    public override void Start()
    {
        throw new System.NotImplementedException();
    }

    public override void End()
    {
        throw new System.NotImplementedException();
    }
    //逻辑每帧事件
    public virtual void LogicUpdate()
    {

    }
    #endregion
    #region 技能
    //进入硬直状态
    public void EnterHardTime(float hardTime)
    {
        m_HardTime = hardTime;
        m_ActionCtrl.AnimSpeed = 0;
        m_ActorObj.Physic.PausePhysic();
    }

    //硬直计时
    protected virtual void HardTimeCount()
    {
        if (m_HardTime > 0)
        {
            if (m_HardTime < Time.time)
            {
                m_ActionCtrl.AnimSpeed = 1;
                m_ActorObj.Physic.CountinuePhysic();
                m_HardTime = 0;
            }
            else
            {
                m_ActionCtrl.AnimSpeed = 0;
                m_ActorObj.Physic.PausePhysic();
            }
        }
    }

    // 攻击判定
    public virtual void SkillAttack()
    {
        if (!m_ActorObj.SkillHurtBox.enabled)
        {
            return;
        }
        Collider2D[] TargetList = AttackList();
        foreach (Collider2D TargetCollider in TargetList)
        {
            if (TargetCollider == null)
            {
                return;
            }
            BaseActorObj targetActor = TargetCollider.transform.parent.GetComponent<BaseActorObj>();
           
            if (targetActor != null && targetActor.Alive && !m_AttackDict.ContainsKey(targetActor))
            {
                EffectOnActor(targetActor, TargetCollider);
            }
        }
    }

    public virtual void EffectOnActor(BaseActorObj targetActor, Collider2D TargetCollider)
    {
        m_AttackDict.Add(targetActor, 1);
        Vector3 effectPs = CountHurtPs(m_ActorObj.SkillHurtBox, TargetCollider);
        GenEffect(effectPs);
        SetCutMeet(targetActor);
        //产生伤害
        float moveDir = 1;
        if ((targetActor.transform.position.x - m_ActorObj.transform.position.x) < 0)
        {
            moveDir *= -1;
        }
        BaseBuff buff = null;
        switch(m_SkillInfo.HitType)
        {
            case HitEffectType.ClickFly:
                buff = new HitFlyBuff(this.m_SkillInfo, moveDir, this.m_ActorObj);
                break;
            default:
                buff = new HitBuff(this.m_SkillInfo, moveDir, this.m_ActorObj);
                break;
        }
        buff.AddToCharacter(targetActor.character);
        this.SetHardTime(this.m_SkillInfo.AttackHardTime);
    }
    //生成特效
    public void GenEffect(Vector3 position)
    {
        //string effectName = skill
        if (m_SkillInfo.EffectName == null || m_SkillInfo.EffectName == "")
            return;
        GameObject effect = EffectManager.Manager.GenEffect(m_SkillInfo.EffectName);
        effect.transform.rotation *= Quaternion.Euler(0, 0, Random.Range(0, 180));
        effect.transform.position = position;
    }
    //计算伤害位置
    public Vector3 CountHurtPs(Collider2D attacker, Collider2D attacked)
    {
        Vector3 Offset = Vector3.zero;
        Offset.x = m_ActorObj.SkillHurtBox.offset.x;
        Offset.y = m_ActorObj.SkillHurtBox.offset.y;
        Vector3 EffectPS = m_ActorObj.SkillHurtBox.transform.position + Offset;

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

    //设置卡肉状态
    public virtual void SetCutMeet(BaseActorObj TargetActor)
    {
        float rangeTime = m_SkillInfo.BeAttackHardTime;
        m_CutTimeClock = Time.time + rangeTime;
        //_ActorObj.AnimCtrl.speed = SpeedRate;
    }

    //消耗体力
    protected virtual void CostVIT()
    {
        // _ActorObj.CostVIT(CostVITNum);
    }
    #endregion
    #region 向量输入
    //向某一方向移动
    public void SetSpeed(float speed)
    {
        //speed *= m_ActorObj.FaceDir.x > 0 ? 1 : -1;
        m_HSpeed = speed;
    }
    public void SetMoveSpeed(Vector2 speed)
    {
        m_HSpeed = speed.x;
        m_VSpeed = speed.y;
    }
    //设置最终速度
    public void SetFinalSpeed(float speed)
    {
        m_HSpeed = 0;
        m_VSpeed = 0;
        m_ActorObj.Physic.SetSpeed(MoveDir * speed);
    }

    //输入向量
    public void InputDirect(Vector2 dirction)
    {
        m_InputDIr = dirction;
        ChangeDir();
    }

    //转向
    protected void ChangeDir()
    {
        Vector3 scale = m_ActorObj.transform.localScale;
        if (!m_DirLock && m_InputDIr.x * scale.x < 0)
        {
            m_ActorObj.FaceToDir(m_InputDIr);
        }
    }
    #endregion
    #region 动画事件
    //朝向锁
    public virtual void SetFaceLock(bool ifLock)
    {
        m_DirLock = ifLock;
    }

    //硬直时间
    public virtual void SetHardTime(float time)
    {
        m_HardTime = Time.time + time;
        m_ActionCtrl.AnimSpeed = 0;
        m_ActorObj.Physic.PausePhysic();
        m_ActionCtrl.AnimSpeed = 0;
    }

    public virtual void CallPuppet(PuppetNpc puppet)
    {
        Vector3 newPs = m_ActorObj.transform.position;
        newPs.y += m_ActorObj.BodyCollider.offset.y;
        puppet.transform.position = newPs;
        puppet.AICtrler.SetTargetActor(((EnemyObj)m_ActorObj).AICtrler.TargetActor);
        puppet.Master = m_ActorObj;
        puppet.SetIDLayer(m_ActorObj.IDLayer);

    }

    public virtual Collider2D[] AttackList()
    {
        Collider2D[] ColliderList = new Collider2D[100];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        ContactFilter.SetLayerMask(Layer);
        m_ActorObj.SkillHurtBox.OverlapCollider(ContactFilter, ColliderList);
        return ColliderList;
    }
    #endregion
    #region 移动
    protected virtual void Move()
    {
        if (m_CutTimeClock <= 0)
        {
            if (m_HSpeed * m_HSpeed > 0 || m_VSpeed * m_VSpeed > 0)
            {
                if (m_VSpeed * m_VSpeed > 0)
                {
                    Vector2 moveSpeed = new Vector2(m_HSpeed, m_VSpeed);
                    moveSpeed.x *= m_ActorObj.FaceDir.x > 0 ? 1 : -1;
                    m_ActorObj.Move(moveSpeed);
                }
                else
                {
                    m_ActorObj.Move(MoveDir * m_HSpeed);
                }
            }
        }
    }

    //根据运动方向旋转
    public void RotateToDirection(Vector2 faceDir)
    {
        m_ActorObj.FaceToDir(faceDir);
    }
    #endregion
}
