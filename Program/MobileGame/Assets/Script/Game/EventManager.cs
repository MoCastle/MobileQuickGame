using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;
using FrameWork;

namespace GameRun
{
    public enum EventEnum
    {
        EnterScene,
        LeaveScene
    }
    public class EventManager :BaseSingleton<EventManager>
    {
        GameEventManager m_GameEventMgr;

        public EventManager():base()
        {
            m_GameEventMgr = GameEntry.Singleton.GetManager<GameEventManager>();
        }

        public void Regist(EventEnum eventType,EventHandler eventHandler)
        {
            int id = (int)eventType;
            m_GameEventMgr.Regist(id, eventHandler);
        }

        public void UnRegist(EventEnum eventType, EventHandler eventHandler)
        {
            int id = (int)eventType;
            m_GameEventMgr.UnRegist(id, eventHandler);
        }

        public void Fire(EventEnum eventType)
        {
            m_GameEventMgr.Fire((int)eventType);
        }
    }
}

