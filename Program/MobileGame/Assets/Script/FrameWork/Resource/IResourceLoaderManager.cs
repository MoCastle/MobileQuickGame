using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Resource;
namespace FrameWork
{
    public interface IResourceManager
    {
        void LoadScene(string name, LoadSceneCallbacks loadSceneCallbacks);
    }
}

