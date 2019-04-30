using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Base;
using PlayerAgent;
using Game;

namespace GameScene
{
    public class BattleSceneMap : BaseSingleton<BattleSceneMap>
    {
        private BattleSceneDirector m_CurDirector;

        public BattleSceneDirector CurDirector
        {
            get
            {
                if (m_CurDirector == null)
                    m_CurDirector = new BattleSceneDirector();
                return m_CurDirector;
            }
        }

        public BattleSceneMap()
        {

        }

        public void GoTo(string name, int Idx)
        {
            Scene curScene = SceneManager.GetActiveScene();

            string curSceneName = curScene.name;
            if (name == null || name == curScene.name || name == "")
            {
                m_CurDirector.JumpTo(Idx);
            }else
            {
                m_CurDirector.ChangeScene(name, Idx);
            }
        }

        public void EnterStart(BattleSceneDirectorEntity dir)
        {
            CurDirector.EnterScene(dir);
        }
    }

}
