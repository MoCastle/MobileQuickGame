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
        /// 加载完成事件
        /// </summary>
        /// <param name="sceneName"></param>
        void onLoadSuccess(string sceneName);
        /// <summary>
        /// 设置资源管理
        /// </summary>
        /// <param name="resourceManager"> 资源管理</param>
        void SetResourceManager(IResourceManager resourceManager);
        /// <summary>
        /// 进入新场景
        /// </summary>
        /// <param name="scene"></param>
        void ChangeScene(string sceneName);
        /// <summary>
        /// 进入场景通知
        /// </summary>
        /// <param name="mainSceneDirector"></param>
        void OnEnterScene(IMainSceneDirector mainSceneDirector);
    }
}

