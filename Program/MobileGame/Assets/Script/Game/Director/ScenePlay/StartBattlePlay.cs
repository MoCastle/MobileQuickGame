using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;
using Game;
namespace GameScene
{
    public class StartBattlePlay : BaseScenePlay
    {
        private BattleSceneDirector m_SceneDirector;
        public BattleSceneDirector SceneDirector
        {
            get
            {
                if(m_SceneDirector == null)
                {
                    m_SceneDirector = m_Owner as BattleSceneDirector;
                }
                return m_SceneDirector;
            }
        }

        public StartBattlePlay():base()
        {

        }

        public override void End()
        {
        }

        public override void Start()
        {
            GameUIManager uiMgr = GameUIManager.Singleton;
            uiMgr.ClearAll();
            uiMgr.ShowUI("ScrollArea");
            uiMgr.ShowUI("MainWindow");
        }

        public override void Update()
        {
        }
    }
}

