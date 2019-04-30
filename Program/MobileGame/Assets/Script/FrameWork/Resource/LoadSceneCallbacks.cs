using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork.Resource
{

    public class LoadSceneCallbacks
    {

        private readonly LoadSceneSuccessCallback m_LoadSceneSuccessCallback;
        private readonly LoadSceneUpdateCallback m_LoadSceneUpdateCallback;

        /// <summary>
        /// 初始化加载场景回调函数集的新实例。
        /// </summary>
        /// <param name="loadSceneSuccessCallback">加载场景成功回调函数。</param>
        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback)
        {
            m_LoadSceneSuccessCallback = loadSceneSuccessCallback;
            m_LoadSceneUpdateCallback = null;
        }



        /// <summary>
        /// 获取加载场景成功回调函数。
        /// </summary>
        public LoadSceneSuccessCallback LoadSceneSuccessCallback
        {
            get
            {
                return m_LoadSceneSuccessCallback;
            }
        }


        /// <summary>
        /// 获取加载场景更新回调函数。
        /// </summary>
        public LoadSceneUpdateCallback LoadSceneUpdateCallback
        {
            get
            {
                return m_LoadSceneUpdateCallback;
            }
        }

    }
}

