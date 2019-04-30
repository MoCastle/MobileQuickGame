using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using GameScene;
public class ActorManager {
    Dictionary<int, Propty> _ActorsInfoDict;

    ActorManager()
    {
        //InitActorModelInfo();
        //InitActorInfo();
    }

    public Propty GetActorInfo(int ID)
    {
        return _ActorsInfoDict[ID];
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
