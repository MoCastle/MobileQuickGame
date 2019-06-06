using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameRun;

namespace PlayerAgent
{
    public class Quest:BasePlayerAgent
    {
        Dictionary<string, BaseGameQuest> gameQuesctDict;
        public Quest()
        {

        }

        public T AddQuest<T>(string name) where T:BaseGameQuest,new()
        {
            Type type = typeof(T);
            T newQuest = new T();
            newQuest.name = name;
            gameQuesctDict[name] = newQuest;

            return newQuest;
        }
        public T GetQuest<T>(string name) where T : BaseGameQuest
        {
            T newQuest = gameQuesctDict[name] as T;
            return newQuest;
        }
    }
}

