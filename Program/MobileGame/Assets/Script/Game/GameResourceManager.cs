using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;
using FrameWork;
using FrameWork.Resource;

namespace GameRun
{
    public class GameResourceManager :BaseGameManager<GameResourceManager,IResourceManager>  {
        public GameResourceManager()
        {
            FrameWorkEntry m_FrameWork = GameEntry.Singleton.FrameWork;
            if (GameEntry.Singleton.IsEditor)
            {
                m_FrameManager = new EditorResourceManager();
                //m_FrameWork.AddManager<EditorResourceManager>(m_FrameManager as EditorResourceManager);
            }
            else
            {
                m_FrameManager = new ResourceManager();
                //m_FrameWork.AddManager<ResourceManager>(m_FrameManager as ResourceManager);
            }
        }

        public void LoadScene(string name,LoadSceneCallbacks callback)
        {
            m_FrameManager.LoadScene(name, callback);
        }

        protected override void SetFrameManager()
        {
            FrameWorkEntry frmWork = GameEntry.Singleton.FrameWork;
            if (GameEntry.Singleton.IsEditor)
            {
                EditorResourceManager resourceManager = new EditorResourceManager();
                //frmWork.AddManager<EditorResourceManager>(resourceManager);
                m_FrameManager = resourceManager;
            }
            else
            {
                ResourceManager resourceManager = new ResourceManager();
                //frmWork.AddManager<ResourceManager>(resourceManager);
                m_FrameManager = resourceManager;
            }
        }
    }
}

