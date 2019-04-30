using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork.Resource
{
    public interface IResourceManager
    {
        void LoadScene(string name, LoadSceneCallbacks loadSceneCallbacks);
    }
}

