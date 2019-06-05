using System;
using System.Collections;
using System.Collections.Generic;
using BaseFunc;

namespace PlayerAgent
{
    public class Player :BaseSingleton<Player>
    {
        Dictionary<string,BasePlayerAgent> m_PlayerAgentMap;
        public Quest quest
        { 
            get
            {
                return GetAgent<Quest>();
            }
        }
        public Player()
        {
        }
        public void Init()
        {
        }
        public T GetAgent<T>() where T : BasePlayerAgent
        {
            Type type = typeof(T);
            T targetAgent = m_PlayerAgentMap[type.Name] as T;
            return targetAgent;
        }
    }
}