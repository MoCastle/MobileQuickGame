using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager {
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
    ActorManager()
    { }
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

    string _Road = "Prefab\\Character\\";
    public BaseActor GenActor( string actorName)
    {
        BaseActor newActor = null;
        string totalRaod = _Road + actorName;
        GameObject newObj = GamePoolManager.Manager.GenObj(totalRaod);
        newActor = newObj == null ? null : newObj.GetComponent<BaseActor>();
        return newActor;
    }
}
