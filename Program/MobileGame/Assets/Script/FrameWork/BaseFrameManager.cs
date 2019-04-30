using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork
{
    public abstract class BaseFrameWorkManager
    {
        private bool m_Removed;
        public bool Removed
        {
            get
            {
                return m_Removed;
            }
        }
        public BaseFrameWorkManager()
        { }
        public abstract void Update();
        public abstract void Start();
    }


}
