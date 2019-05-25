using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Resource;
namespace FrameWork
{
    struct EditorLoadSceneInfo
    {
        public LoadSceneCallbacks loadSceneCallbacks;
        public string name;
    }
    public class EditorResourceManager : BaseFrameWorkManager, IResourceManager
    {
        private LinkedList<EditorLoadSceneInfo> m_LoadSceneList;

        public EditorResourceManager()
        {
            m_LoadSceneList = new LinkedList<EditorLoadSceneInfo>();
        }

        public void LoadScene(string name, LoadSceneCallbacks loadSceneCallbacks)
        {
            EditorLoadSceneInfo loadInfo = new EditorLoadSceneInfo();
            loadInfo.loadSceneCallbacks = loadSceneCallbacks;
            loadInfo.name = name;
            m_LoadSceneList.AddFirst(loadInfo);
        }

        public override void Start()
        {
        }

        private void LoadingScene()
        {
            LinkedListNode<EditorLoadSceneInfo> firstNode = m_LoadSceneList.First;
            while(firstNode!=null)
            {
                firstNode.Value.loadSceneCallbacks.LoadSceneSuccessCallback(firstNode.Value.name);
                firstNode = firstNode.Next;
            }
            m_LoadSceneList.Clear();
        }

        public override void Update()
        {
            LoadingScene();
        }
    }
}

