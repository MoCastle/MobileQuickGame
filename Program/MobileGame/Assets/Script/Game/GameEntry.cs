using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork;
using BaseFunc;
namespace GameRun
{
    public class GameEntry : BaseSingleton<GameEntry>
    {
        private GameObject m_DriverObject;
        private FrameWorkEntry m_FrameWorkEntry;

        #region 接口
        public FrameWorkEntry FrameWork
        {
            get
            {
                return m_FrameWorkEntry;
            }
        }
        public bool IsEditor { get; set; }
        #endregion
        public GameEntry() : base()
        {
            m_DriverObject = new GameObject("GameDriver");
            GameObject.DontDestroyOnLoad(m_DriverObject);
            GameDriver driverComp = m_DriverObject.AddComponent<GameDriver>();
            driverComp.Init(this);

            m_FrameWorkEntry = new FrameWorkEntry();

            IsEditor = true;
            Init();
        }
        public void Init()
        {
            m_FrameWorkEntry.AddManager<GameUIManager>();
            m_FrameWorkEntry.AddManager<GameSceneManager>();
        }

        public void Update()
        {
            m_FrameWorkEntry.Update();
        }
        public T GetManager<T> () where T: BaseFrameWorkManager
        {
            return m_FrameWorkEntry.GetManager<T>() as T;
        }

    }
}

