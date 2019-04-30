using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
using Base;
namespace Game
{

    public class ActorManager : Base.BaseSingleton<ActorManager>
    {
        Dictionary<int, Propty> _ActorsInfoDict;
        string m_Road = "Cfg\\";
        string m_ActorRoad = "Prefab\\Character\\";

        public ActorManager()
        {

        }

        public BaseActorObj GenActor(string actorName)
        {
            BaseActorObj newActor = null;
            string totalRaod = m_ActorRoad + actorName;
            GameObject newObj = GamePoolManager.Manager.GenObj(totalRaod);
            newActor = newObj == null ? null : newObj.GetComponent<BaseActorObj>();
            newActor.Birth();
            return newActor;
        }

    }
}

