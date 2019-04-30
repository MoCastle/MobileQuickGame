using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Base;
namespace PlayerAgent
{
    public class SceneAgent 
    {
        private string m_CurSceneName;
        private Vector3 m_CurPosition;
        private int m_DoorIdx;

        public string CurSceneName
        {
            get
            {
                if (m_CurSceneName == "")
                    m_CurSceneName = SceneManager.GetActiveScene().name;
                return m_CurSceneName;
            }
        }

        public SceneAgent()
        {
            m_CurSceneName = "";
        }

        public void EnterNewScene(string sceneName,int doorIdx)
        {
            m_CurSceneName = sceneName;
            m_DoorIdx = doorIdx;
        }
    }
}
