using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene
{
    public class SceneDoor : MonoBehaviour
    {
        
        #region 内部属性
        [Title("跳转到场景的第几个门", "black")]
        public int m_Idx;
        [Title("场景名", "black")]
        public string m_SceneName;
        [SerializeField]
        [Header("关闭传送")]
        private bool m_CloseSend;
        float CountLeaveTime = -1;
        BoxCollider2D m_Collider;
        #endregion
        #region 对外接口
        BoxCollider2D Colloder
        {
            get
            {
                if (m_Collider == null)
                {
                    m_Collider = GetComponent<BoxCollider2D>();
                }
                return m_Collider;
            }
        }
        #endregion


        public Action CollisionEnter;

        Transform playerTrans;
        public void GenPlayer(Transform inPlayerTrans)
        {
            playerTrans = inPlayerTrans;
            playerTrans.transform.position = this.transform.position;
        }
        private void Start()
        {
            if (!m_CloseSend)
            {
                Colloder.enabled = true;
                CollisionEnter = JumpScene;
            }
            else
            {
                Colloder.enabled = false;
            }
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Transform saveTrans = playerTrans;
                if (saveTrans != null)
                {
                    return;
                }
                this.playerTrans = collision.transform;
                if (CollisionEnter != null && CountLeaveTime < Time.time)
                    CollisionEnter();
                CountLeaveTime = -1;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                CountLeaveTime = Time.time + 0.2f;
                playerTrans = null;
            }
        }

        void JumpScene()
        {
            BattleSceneInfo info = new BattleSceneInfo();
            //BattleSceneMap.Singleton.GoTo(m_SceneName,m_Idx);
        }
    }
}

