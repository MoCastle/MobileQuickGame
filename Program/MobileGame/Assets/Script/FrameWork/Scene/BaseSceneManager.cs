using System.Collections;
using System.Collections.Generic;
using FrameWork.Resource;

namespace FrameWork.Scene
{
    public abstract class BaseSceneManager : BaseFrameWorkManager, ISceneManager
    {
        protected IResourceManager m_ResourceMgr;

        public BaseSceneManager() : base()
        {
        }

        public abstract void LoadScene(string sceneName);
        public abstract void onLoadSuccess(string sceneName);
        public void SetResourceManager(IResourceManager resourceManager)
        {
            m_ResourceMgr = resourceManager;
        }
        public void ChangeScene(string sceneName)
        {
            LoadScene(sceneName);
        }
        public override void Update()
        {
        }

    }
}
