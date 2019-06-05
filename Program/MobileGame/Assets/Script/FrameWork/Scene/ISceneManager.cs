using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Resource;
using FrameWork.Scene;
namespace FrameWork
{
    public interface ISceneManager
    {
        /// <summary>
        /// 设置资源管理
        /// </summary>
        /// <param name="resourceManager"> 资源管理</param>
        void SetResourceManager(IResourceManager resourceManager);
        
        /// <summary>
        /// 进入场景通知
        /// </summary>
        /// <param name="mainSceneDirector"></param>
        void OnSceneStarted(IMainSceneDirector mainSceneDirector);
    }
}

