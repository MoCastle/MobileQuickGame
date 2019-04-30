using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameScene
{
    public class BattleSceneDirectorEntity : MonoBehaviour
    {
        [SerializeField]
        [Header("玩家")]
        private PlayerObj m_Player;
        [SerializeField]
        [Header("传送门")]
        private Transform m_DoorCollision;

        public PlayerObj player
        {
            get
            {
                return m_Player;
            }
        }

        private void Start()
        {
            if(m_Player == null)
            {
                BaseActorObj actor = Resources.Load<BaseActorObj>("Prefab/Character/Player") ;
                m_Player = GameObject.Instantiate(actor as PlayerObj);
            }
            BattleSceneMap.Singleton.EnterStart(this);
        }

        public void PutPlayer( PlayerObj player )
        {
            m_Player = player;
        }

        public SceneDoor GetDoor(int Idx)
        {
            SceneDoor door = m_DoorCollision != null ? m_DoorCollision.GetChild(Idx).GetComponent<SceneDoor>() : null;
            return  door;
        }

        private void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }
        public Action onUpdate;
    }
}

