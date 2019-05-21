using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;
using FrameWork.Resource;
namespace FrameWork.Scene
{
    public class EditorSceneManager : BaseSceneManager
    {

        public EditorSceneManager()
        {
        }


        public override void Start()
        {
        }

        public override void LoadScene(string sceneName)
        {
            LoadSceneCallbacks callBack = new LoadSceneCallbacks(onLoadSuccess);
            m_ResourceMgr.LoadScene(sceneName, callBack);
        }

        public override void onLoadSuccess(string sceneName)
        {
            /*
            EditorApplication.LoadLevelInPlayMode(sceneName);
            Debug.Log(sceneName);
            EditorApplication.isPlaying = true;
            //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            */
        }
    }
}

