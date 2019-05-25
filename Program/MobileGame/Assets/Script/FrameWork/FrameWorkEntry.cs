using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;

namespace FrameWork
{
    public class FrameWorkEntry
    {
        #region 内部属性
        Dictionary<string, BaseFrameWorkManager> m_FMDict;
        LinkedList<BaseFrameWorkManager> m_FMManagerList;
        #endregion
        
        public FrameWorkEntry() : base()
        {
            m_FMDict = new Dictionary<string, BaseFrameWorkManager>();
            m_FMManagerList = new LinkedList<BaseFrameWorkManager>();
        }
        public void AddManager<T>() where T : BaseFrameWorkManager,new ()
        {
            Type type = typeof(T);
            m_FMDict.Add(type.Name, new T());
        }
        public void Update()
        {
            m_FMManagerList.Clear();
            foreach (KeyValuePair<string, BaseFrameWorkManager> keyValue in m_FMDict)
            {
                m_FMManagerList.AddFirst(keyValue.Value);
            }
            foreach (BaseFrameWorkManager mgr in m_FMManagerList)
            {
                mgr.Update();
            }
        }
        public BaseFrameWorkManager GetManager<T>()
        {
            Type type = typeof(T);
            return m_FMDict[type.Name];
        }
    }
}


