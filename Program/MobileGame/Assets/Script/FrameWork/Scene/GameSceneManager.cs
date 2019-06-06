using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Scene;
using BaseFunc;
namespace FrameWork
{
    public class GameSceneManager : BaseFrameWorkManager, ISceneManager
    {
        IMainSceneDirector m_MainDirector;
        IResourceManager m_ResourceMgr;

        SceneStateFSM m_SceneStateFSM;
        public GameSceneManager() : base()
        {
            m_SceneStateFSM = new SceneStateFSM(this);
        }

        public override void Start()
        {
        }
        /*
        public void SetResourceManager(IResourceManager resourceManager)
        {
            m_ResourceMgr = resourceManager;
        }
        public override void onLoadSuccess(string sceneName)
        {
        }
        */
        /// <summary>
        /// 申请改变场景
        /// </summary>
        /// <param name="sceneName">新场景名</param>
        public void ChangeScene(string sceneName)
        {
            //LoadSceneCallbacks callBack = new LoadSceneCallbacks(onLoadSuccess);
            //m_ResourceMgr.LoadScene(sceneName, callBack);
            m_SceneStateFSM.SwitchSceneState(new LeaveSceneState(sceneName));
        }

        public override void Update()
        {
            m_SceneStateFSM.Update();
        }

        void onLoadSuccess(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public void SetResourceManager(IResourceManager resourceManager)
        {
            m_ResourceMgr = resourceManager;

        }

        public void OnSceneStarted(IMainSceneDirector mainSceneDirector)
        {
            m_MainDirector = mainSceneDirector;
            m_SceneStateFSM.SwitchSceneState(new RunSceneState());
        }

        class SceneStateFSM:BaseFSM
        {
            GameSceneManager m_SceneMgr;
            public GameSceneManager sceneMgr
            {
                get
                {
                    return m_SceneMgr;
                }
            }
            public SceneStateFSM(GameSceneManager inSceneMgr) :base()
            {
                m_SceneMgr = inSceneMgr;
            }

            public void SwitchSceneState(SceneState newState)
            {
                Switch(newState);
            }
            public void Update()
            {
                if(m_CurState != null)
                    m_CurState.Update();
            }
        }
        abstract class SceneState:BaseState
        {
            protected SceneStateFSM owner
            {
                get
                {
                    return m_Owner as SceneStateFSM;
                }
            }
            protected GameSceneManager gameSceneMgr
            {
                get
                {
                    return owner.sceneMgr;
                }
            }
        
        }
        class ChangeSceneState : SceneState
        {
            string nextSceneName;
            public ChangeSceneState(string name):base()
            {
                nextSceneName = name;
            }
            public override void End()
            {
            }

            public override void Start()
            {
                gameSceneMgr.onLoadSuccess(nextSceneName);
            }

            public override void Update()
            {
            }
        }

        class LeaveSceneState : SceneState
        {
            string nextSceneName;
            bool m_LeaveComplete;
            public LeaveSceneState(string name)
            {
                nextSceneName = name;
            }
            public override void End()
            {
            }

            public override void Start()
            {
                gameSceneMgr.m_MainDirector.LeaveScene();
                m_LeaveComplete = false;
            }

            public override void Update()
            {
                if (m_LeaveComplete)
                    return;
                if(gameSceneMgr.m_MainDirector.leaved)
                {
                    gameSceneMgr.onLoadSuccess(nextSceneName);
                    m_LeaveComplete = true;
                }
            }
        }

        class RunSceneState : SceneState
        {
            public override void End()
            {
            }

            public override void Start()
            {
            }

            public override void Update()
            {
            }
        }
    }
}

