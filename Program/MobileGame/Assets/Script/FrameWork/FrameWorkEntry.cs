using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;

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
        public void AddManager<T>(T manager) where T : BaseFrameWorkManager
        {
            Type type = typeof(T);
            m_FMDict.Add(type.Name, manager);
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


