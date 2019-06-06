using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using GameRun;
using Cinemachine;

namespace GameScene
{
    public class BattleSceneMainDirectorEntity : BaseMainDirectorEntity
    {
        [SerializeField]
        [Header("玩家")]
        private PlayerObj m_Player;
        [SerializeField]
        [Header("传送门")]
        private Transform m_DoorCollision;
        [SerializeField]
        [Header("相机")]
        private CinemachineVirtualCamera m_Camera;
        [SerializeField]
        [Header("进出场景需要的动画")]
        private Animator m_EnterLeaveScene;
        [SerializeField]
        [Header("NPC列表")]
        private Transform m_NPCList;
        #region 外部属性
        public CinemachineVirtualCamera camera
        {
            get
            {
                return m_Camera;
            }
            set
            {
                m_Camera = value;
            }
        }
        private bool m_Leaved;
        public PlayerObj player
        {
            get
            {
                return m_Player;
            }
        }
        public override bool leaved
        {
            get
            {
                return m_Leaved;
            }
        }
        #endregion
        #region 流程
        private void Awake()
        {
            Animator enterLeaveScene = GameObject.Instantiate<Animator>(m_EnterLeaveScene);
            m_EnterLeaveScene = enterLeaveScene;

            m_EnterLeaveScene.Play("EnterScene");
            UIManager.Singleton.ShowUI("ScrollArea");
            m_Leaved = false;
            SetPlayer();
            
            if (m_Camera != null)
            {
                m_Camera.LookAt = m_Player.transform;
                m_Camera.Follow = m_Player.transform.Find("CameraPoint");
            }
            SceneDoor startDoor = GetDoor(0);
            startDoor.GenPlayer(player.transform);
            SceneManager.Singleton.SceneStarted(this);
        }
        private void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }
        #endregion
        #region 接口
        public void PutPlayer(PlayerObj player)
        {
            m_Player = player;
        }
        public void SetDoorList(GameObject doorList)
        {
            m_DoorCollision = doorList.transform;
        }
        public void SetNPCList(GameObject npcList)
        {
            m_NPCList = npcList.transform;
        }
        public SceneDoor GetDoor(int Idx)
        {
            SceneDoor door = m_DoorCollision != null ? m_DoorCollision.GetChild(Idx).GetComponent<SceneDoor>() : null;
            return door;
        }
        #endregion
        #region 初始设置
        private void SetPlayer()
        {
            if (m_Player == null)
            {
                BaseActorObj actor = Resources.Load<BaseActorObj>("Prefab/Character/Player");
                m_Player = GameObject.Instantiate(actor as PlayerObj);
            }
        }
        #endregion
        public override void LeaveScene()
        {
            ScreenMask mask = m_EnterLeaveScene.GetComponent<ScreenMask>();
            mask.onAnimEnd += LeavedAnimPlayed;
            m_EnterLeaveScene.Play("LeaveScene");
        }

        public void LeavedAnimPlayed()
        {
            m_Leaved = true;
        }

        public Action onUpdate;
    }
}

