using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillManager {
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
}
