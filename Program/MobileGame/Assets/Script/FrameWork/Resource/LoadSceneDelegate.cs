using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork.Resource
{
    public delegate void LoadSceneSuccessCallback(string sceneAssetName);
    /// <summary>
    /// 加载场景更新回调函数。
    /// </summary>
    /// <param name="sceneAssetName">要加载的场景资源名称。</param>
    /// <param name="progress">加载场景进度。</param>
    /// <param name="userData">用户自定义数据。</param>
    public delegate void LoadSceneUpdateCallback(string sceneAssetName, float progress);
}

