using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameScene
{
    public class BattleSceneDirector : BaseSceneDirector
    {
        private int m_CurBirthID;
        BattleSceneDirectorEntity m_DirectorObj;

        public PlayerObj player
        {
            get
            {
                return m_DirectorObj.player;
            }
        }

        public BattleSceneDirector() : base()
        {
        }

        public void EnterScene(BattleSceneDirectorEntity entity)
        {
            m_DirectorObj = entity;
            m_DirectorObj.onUpdate = Update;
            JumpTo(m_CurBirthID);
            SwitchPlay(new StartBattlePlay());
        }

        public void SwitchPlay(BaseScenePlay play)
        {
            Switch(play);
        }

        public void JumpTo(int idx)
        {
            SceneDoor door = m_DirectorObj.GetDoor(idx);
            PlayerObj player = m_DirectorObj.player;
            if (door && player)
                door.GenPlayer(player.transform);
        }

        public void ChangeScene(string sceneName,int idx = -1)
        {
            if(idx>0)
                m_CurBirthID = idx;
            SwitchPlay(new LeaveBattleScenePlay(sceneName));
        }

        public void Update()
        {
            if (m_CurState != null)
                m_CurState.Update();
        }
    }
}
