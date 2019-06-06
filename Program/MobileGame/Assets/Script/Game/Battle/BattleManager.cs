using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;

namespace GameRun
{
    public class BattleManager : BaseSingleton<BattleManager>
    {
        int birthID;
        public void JumpEnterScene(string sceneName,int id)
        {
            birthID = id;
            SceneManager.Singleton.ChangeScene(sceneName);
        }
    }
}

