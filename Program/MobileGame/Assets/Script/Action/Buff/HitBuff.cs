using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class HitBuff : BaseBuff
{
    SkillInfo m_SkillInfo;
    float m_direction;
    public BaseActorObj Attackter;

    public float hardTime
    {
        get
        {
            return m_SkillInfo.BeAttackHardTime;
        }
    }
    public float addSpeed
    {
        get
        {
            return m_SkillInfo.HitFlyValue * m_direction;
        }
    }
    public HitBuff(SkillInfo skillinfo = null, float directon = 0, BaseActorObj actor = null) : base(BuffType.Hit)
    {
        skillinfo = skillinfo == null ? new SkillInfo() : skillinfo;
        Init(skillinfo, directon, actor);
    }
    public void Init(SkillInfo skillinfo, float direction, BaseActorObj attackter)
    {
        base.Init(skillinfo.BeAttackHardTime);
        m_SkillInfo = skillinfo;
        m_direction = direction;
        Attackter = attackter;
    }

    public override void End()
    {
    }

    public override void Start()
    {
        BaseActorObj actor = m_Actor;
        if (actor != null)
        {
            actor.Anim.SetTrigger("HitBack");
            actor.FaceToDir(Vector2.right * (Attackter.transform.position.x - actor.transform.position.x ));
        }
    }
}
