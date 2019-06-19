using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class HitFlyBuff : BaseBuff
{
    private SkillInfo m_SkillInfo;
    private float m_Direction;
    private BaseActorObj m_Attackter;
    public BaseActorObj attackter
    {
        get
        {
            return m_Attackter;
        }
    }

    public float hardTime
    {
        get
        {
            return m_SkillInfo.BeAttackHardTime;
        }
    }
    #region 接口
    public Vector2 Speed
    {
        get
        {
            return m_SkillInfo.HitFlyDirection * m_SkillInfo.HitFlyValue;
        }
    }
    #endregion
    public HitFlyBuff(SkillInfo skillinfo = null, float directon = 0, BaseActorObj actor = null) : base(BuffType.HitFly)
    {
        m_Direction = directon;
        Init(skillinfo, directon, actor);
    }

    public void Init(SkillInfo skillinfo, float direction, BaseActorObj attackter)
    {
        base.Init(skillinfo.BeAttackHardTime);
        m_SkillInfo = skillinfo;
        m_Direction = direction;
        m_Attackter = attackter;
    }

    
    public override void End()
    {
    }

    public override void Start()
    {
        BaseActorObj actor = m_Actor;
        if (actor != null)
        {
            actor.Anim.SetTrigger("ClickFly");
            actor.FaceToDir(Vector2.right * (m_Attackter.transform.position.x - actor.transform.position.x));
        }
    }
}
