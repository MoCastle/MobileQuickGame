using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;
using Game;

namespace GameScene
{
    public class LeaveBattleScenePlay : BaseScenePlay
    {
        public string m_NextSceneName;
        public LeaveBattleScenePlay(string sceneName):base()
        {
            m_NextSceneName = sceneName;
        }
        public override void End()
        {
        }

        public override void Start()
        {
            GameSceneManager.Singleton.ChangeScene(m_NextSceneName);
        }

        public override void Update()
        {
        }
    }
}
