using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SkillObjInfo
{
    [Title("技能类名", "yellow")]
    public string SkillClassName;
    [Title("百分比", "yellow")]
    public float AttackPercent;
    [Title("暴击率", "yellow")]
    public float CriticalPercent;
    public SkillObjInfo( int InAttackPercent = 1 )
    {
        AttackPercent = 100;
        CriticalPercent = 50;
        SkillClassName = "";
    }
}

public class SkillObj : MonoBehaviour {
    [Title("通用技能列表", "black")]
    public AnimStruct[] _SkillEnum;
    [Title("技能列表", "black")]
    public SkillObjInfo[] SkArray;
    public Dictionary<string, int> SkillInfoDic = new Dictionary<string, int>();
    private void Awake()
    {
        for (int i = 0; i < SkArray.Length; ++i)
        {
            if (!SkillInfoDic.ContainsKey(SkArray[i].SkillClassName))
            {
                SkillInfoDic.Add(SkArray[i].SkillClassName, i);
            }
        }
    }
    public SkillObjInfo GetSkillObjInfo( string Name )
    {
        if( Name!= null && SkillInfoDic.ContainsKey(Name))
        {
            return SkArray[SkillInfoDic[Name]];
        }
        return new SkillObjInfo();
    }
}
