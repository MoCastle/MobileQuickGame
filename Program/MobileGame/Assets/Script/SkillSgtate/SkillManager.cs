using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillInfo
{
    public int ID;
    public string Name;
    public float Speed;
    public string EffectName;
    public float CutMeet;
    public float CutMeetRate;
    public Vector2 Dir;
    public HitEffectType HitType;
}

public struct ActionInfo
{
    public int SkillID;
    public string ActionName;
}

public class SkillManager {
    static SkillManager _Mgr;
    public static SkillManager Mgr
    {
        get
        {
            if(_Mgr == null)
            {
                _Mgr = new SkillManager();
            }
            return _Mgr;
        }
    }
    //技能列表信息
    List<SkillInfo> _SkillInfoList;

    //通用技能列表
    List<ActionInfo> _UsualActionList;
    public List<ActionInfo> UsualActionList
    {
        get
        {
            if(_UsualActionList == null)
            {
                _UsualActionList = new List<ActionInfo>();
                string usualSkillArr = UsuallyInfoMgr.Mgr.GetInfo(0);
                string[] actionList = usualSkillArr.Split(';');

                if (actionList.Length >= 1)
                {
                    foreach (string actionStr in actionList)
                    {

                        string[] actionDouble = actionStr.Split(' ');
                        ActionInfo actionInfo = new ActionInfo();
                        if (actionDouble[0] == "")
                        {
                            continue;
                        }

                        actionInfo.SkillID = int.Parse(actionDouble[1]); 
                        actionInfo.ActionName = actionDouble[0];
                        _UsualActionList.Add(actionInfo);
                    }
                }
            }
            return _UsualActionList;
        }
    }

    SkillManager()
    {
        InitSkillInfo();
    }
    //读取数据表
    public void InitSkillInfo()
    {
        _SkillInfoList = new List<SkillInfo>();
        List<string[]> skillInfoList = CfgReader.ReadCfg("SkillInfo");
        foreach (string[] skillInfoArr in skillInfoList)
        {
            if(skillInfoArr.Length < 1)
            {
                continue;
            }
            SkillInfo skillInfo = new SkillInfo();
            skillInfo.ID = int.Parse(skillInfoArr[0]);
            skillInfo.Name = skillInfoArr[1];
            skillInfo.EffectName = skillInfoArr[2];
            skillInfo.CutMeet = float.Parse( skillInfoArr[3] == ""?"0": skillInfoArr[3]);
            skillInfo.CutMeetRate = float.Parse(skillInfoArr[3] == "" ? "0" : skillInfoArr[4]);
            _SkillInfoList.Add(skillInfo);
        }
    }
    //获取技能列表信息
    public SkillInfo GetSkillInfo( int id)
    {
        return _SkillInfoList[id];
    }


    public static SkillObj Obj;
    
	// Use this for initialization
    //获取伤害百分比
	public static float GetAttPercent(string Name)
    {
        return SkillInfoReader.Cfg.GetDamPerByKey(Name);
    }
    //获取暴击百分比
    public static float GetCritPercent(string Name)
    {
        return SkillInfoReader.Cfg.GetVioPerByKey(Name);
    }

    public static SkillEffect GenEffect()
    {
        SkillEffect newEffect = new SkillEffect();
        //击退值
        newEffect.HitBackValue = 1;
        //攻击效果类型
        newEffect.HitType = HitTypeEnum.None;
        //伤害值
        newEffect.Damage = 0;
        //效果方向
        newEffect.Dir = Vector2.zero;
        //卡肉时间
        newEffect.CutTime = 0;
        return newEffect;
    }

    public static void AttackActor( BaseActor attacker, BaseActor beAttacked,SkillEffect skillEffect )
    {
        //伤害计算
        //效果施加
        
    }
}
