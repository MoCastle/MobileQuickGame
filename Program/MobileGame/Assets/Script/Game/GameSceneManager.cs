using System.Collections;
using System.Collections.Generic;
using FrameWork.Scene;
using FrameWork;
using GameTool;
namespace Game
{
    public class GameSceneManager : BaseGameManager<GameSceneManager, ISceneManager>
    {
        public GameSceneManager() : base()
        {
            SetFrameManager();
            m_FrameManager.SetResourceManager(GameResourceManager.Singleton.FrameManager);
        }
        public void ChangeScene(string sceneName)
        {
            sceneName = Path.ScenePath + sceneName + ".unity";
            m_FrameManager.ChangeScene(sceneName);
        }

        public void onLoadSuccess(string sceneName)
        {
        }

        protected override void SetFrameManager()
        {
            FrameWorkEntry frmWork = GameEntry.Singleton.FrameWork;
            if (GameEntry.Singleton.IsEditor)
            {
                EditorSceneManager sceneManager = new EditorSceneManager();
                frmWork.AddManager<EditorSceneManager>(sceneManager);
                m_FrameManager = sceneManager;
            }
            else
            {
                SceneManager sceneManager = new SceneManager();
                frmWork.AddManager<SceneManager>(sceneManager);
                m_FrameManager = sceneManager;
            }
        }
        public void EnterBattleScene( string name, int DoorID )
        {
            
        }
    }
}

