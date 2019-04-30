using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace GameScene
{
    [Serializable]
    public struct DoorData
    {
        public Vector3 Position;
        public string SceneName;
        public int Idx;
        public void SetData(SceneDoor door)
        {
            door.transform.position = Position;
            //door.sceneName = SceneName;
            door.m_Idx = Idx;
        }
        public void WriteData(SceneDoor door)
        {
            Position = door.transform.position;
            //SceneName = door.sceneName;
            Idx = door.m_Idx;
        }
    }

    [Serializable]
    public struct SceneData
    {
        //场景名字
        public string SceneName;
        public CharacterData[] EnemyArr;
        public DoorData[] DoorArr;
    }

    public class SceneDataManager
    {
        static SceneDataManager m_Mgr;
        public static SceneDataManager Mgr
        {
            get
            {
                if (m_Mgr == null)
                {
                    m_Mgr = new SceneDataManager();
                }
                return m_Mgr;
            }
        }
        public SceneData GetSceneData(string sceneName)
        {
            SceneData returnData;
            m_SceneData.TryGetValue(sceneName, out returnData);
            return returnData;
        }

        SceneDataManager()
        {
            m_SceneData = new Dictionary<string, SceneData>();
            Load();
        }
        Dictionary<string, SceneData> m_SceneData;
        private void Load()
        {
            byte[] bytes = LoaderFile.LoadBytes(PathManager.SceneData, Application.platform == RuntimePlatform.Android);
            string jsStr = Encoding.UTF8.GetString(bytes);
            SlzDictionary<string, SceneData> slzDictionary = JsonUtility.FromJson<SlzDictionary<string, SceneData>>(jsStr);
            m_SceneData = slzDictionary.ToDictionary();
        }
    }
}

