using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFunc;
using FrameWork;

namespace GameRun
{
    public class UIManager : BaseSingleton<UIManager>
    {
        GameUIManager uiMgr;
        public UIManager()
        {
            uiMgr = GameEntry.Singleton.GetManager<GameUIManager>();
        }
        public void ShowUI(string UIName)
        {
            uiMgr.ShowUI(UIName);
        }
    }
}

