using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;
using FrameWork;
using GameScene;

namespace GameRun
{
    public class SceneManager : BaseSingleton<SceneManager>
    {
        private GameSceneManager m_GameSceneMgr;
        public SceneManager():base( )
        {
            m_GameSceneMgr = GameEntry.Singleton.GetManager<GameSceneManager>();
        }
        public void SceneStarted(BaseMainDirectorEntity mainDirectorEntity) {
            m_GameSceneMgr.OnSceneStarted(mainDirectorEntity);
        }
        /// <summary>
        /// 改变到的场景
        /// </summary>
        /// <param name="name">场景名</param>
        public void ChangeScene(string name)
        {
            m_GameSceneMgr.ChangeScene(name);
        }
    }
}
