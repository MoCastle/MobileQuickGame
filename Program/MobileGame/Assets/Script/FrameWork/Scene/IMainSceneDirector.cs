using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.Scene
{
    public interface IMainSceneDirector
    {
        bool leaved { get; }
        void LeaveScene();
    }
}

