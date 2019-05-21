using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Resource;

namespace FrameWork.Scene
{
    public class SceneManager : BaseSceneManager
    {
        public SceneManager() : base()
        {
        }

        public override void Start()
        {
        }

        public void SetResourceManager(IResourceManager resourceManager)
        {
            m_ResourceMgr = resourceManager;
        }

        public override void LoadScene(string sceneName)
        {
            LoadSceneCallbacks callBack = new LoadSceneCallbacks(onLoadSuccess);
            m_ResourceMgr.LoadScene(sceneName, callBack);
        }

        public override void onLoadSuccess(string sceneName)
        {
        }
    }
}

