using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public struct ActorInfo
{
    public int ID;
    public string Name;
    [Title("最大生命值", "black")]
    public float Life;
    public float Physical;
    public float Speed;
    public float Power;
    [Title("肉量", "black")]
    public float Heavy;
    [Title("肉质", "black")]
    public float HeavyRate;
    [Title("受击特效", "black")]
    public string BloodName;

    public List<ActionInfo> ActorActionList;
}

public class ActorManager {
    List<List<ActionInfo>> _ActionList;
    Dictionary<int, ActorInfo> _ActorsInfoDict;

    ActorManager()
    {
        InitActorModelInfo();
        InitActorInfo();
    }

    public ActorInfo GetActorInfo(int ID)
    {
        return _ActorsInfoDict[ID];
    }

    public void InitActorModelInfo( )
    {
        _ActionList = new List<List<ActionInfo>>();
        List<string[]> ModelInfo = CfgReader.ReadCfg("ActorModelInfo");
        foreach (string[] modInfo in ModelInfo)
        {
            string actionInfo = modInfo[1] != null ? modInfo[1] : "";
            string[] actionList = actionInfo.Split(';');

            if(actionList.Length >= 1)
            {
                List<ActionInfo> actorActionList = new List<ActionInfo>();
                foreach (string actionStr in actionList)
                {

                    String[] actionDouble = actionStr.Split(' ');
                    ActionInfo animStruct = new ActionInfo();
                    if (actionDouble[0] == "")
                    {
                        continue;
                    }
                    
                    animStruct.ActionName = actionDouble[0];
                    animStruct.SkillID = int.Parse(actionDouble[1]);
                    actorActionList.Add(animStruct);
                }
                _ActionList.Add(actorActionList);
            } 
        }
    }
    public void InitActorInfo()
    {
        _ActorsInfoDict = new Dictionary<int, ActorInfo>();
        List<string[]> actorInfo = CfgReader.ReadCfg("ActorInfo");
        foreach (string[] str in actorInfo)
        {
            ActorInfo readActorInfo = new ActorInfo();
            readActorInfo.ID = int.Parse(str[0]);
            readActorInfo.Name = str[1];
            readActorInfo.Life = float.Parse(str[2]);
            readActorInfo.Physical = float.Parse(str[3]);
            readActorInfo.Speed = float.Parse(str[4]);
            readActorInfo.Power = float.Parse(str[5]);
            int actorModelID = int.Parse(str[6]);
            readActorInfo.ActorActionList = _ActionList[actorModelID];
            readActorInfo.Heavy = float.Parse(str[7]);
            readActorInfo.HeavyRate = float.Parse(str[8]);

            
            _ActorsInfoDict.Add(readActorInfo.ID, readActorInfo);
        }
    }

    string _Road = "Cfg\\";
    string _ActorRoad = "Prefab\\Character\\";
    static ActorManager _Mgr;
    public static ActorManager Mgr
    {
        get
        {
            if(_Mgr == null)
            {
                _Mgr = new ActorManager();
            }
            return _Mgr;
        }
    }
    
    //根据运动方向获取旋转
	public Vector3 GetActorRotation( Vector2 MoveDirection )
    {
        Vector2 NormDir = MoveDirection.normalized;
        float Rotate = 0;
        Rotate = Mathf.Atan2(NormDir.y, Mathf.Abs(NormDir.x)) * 180 / Mathf.PI;
        if (NormDir.x < 0)
        {
            Rotate = Rotate * -1;
        }
        Vector3 Rotation = Vector3.forward * Rotate;
        return Rotation;
    }

    
    public BaseActorObj GenActor( string actorName)
    {
        BaseActorObj newActor = null;
        string totalRaod = _ActorRoad + actorName;
        GameObject newObj = GamePoolManager.Manager.GenObj(totalRaod);
        newActor = newObj == null ? null : newObj.GetComponent<BaseActorObj>();
        newActor.Birth();
        return newActor;
    }


}
