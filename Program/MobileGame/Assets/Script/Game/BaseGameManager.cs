using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base;
using FrameWork;

namespace Game
{
    public abstract class BaseGameManager<GameManagerType, FrameManagerType> : BaseSingleton<GameManagerType> where GameManagerType : BaseSingleton<GameManagerType>
    {
        protected FrameManagerType m_FrameManager;
        public FrameManagerType FrameManager
        {
            get
            {
                return m_FrameManager;
            }
        }
        public BaseGameManager():base()
        {

        }
        abstract protected void SetFrameManager( );
    }
}


