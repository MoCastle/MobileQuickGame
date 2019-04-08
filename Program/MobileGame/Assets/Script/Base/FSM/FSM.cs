using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BaseFunc
{
    public class BaseFSM
    {
        #region 内部属性
        BaseState m_CurState;
        #endregion
        #region 流程
        public BaseFSM()
        {

        }

        public virtual void Switch( BaseState state )
        {
            if(m_CurState!=null)
            {
                m_CurState.End();
            }
            state.SetOwner(this);
            m_CurState = state;
        }
        #endregion
    }

    public abstract class BaseState
    {
        #region 内部属性
        BaseFSM m_Owner;
        #endregion
        #region 流程
        public BaseState()
        {
        }

        public abstract void Start();
        public abstract void End();
        public abstract void Update();

        public void SetOwner( BaseFSM owner )
        {
            m_Owner = owner;
        }
        #endregion
    }
}
