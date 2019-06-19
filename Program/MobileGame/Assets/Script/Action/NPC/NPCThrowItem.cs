using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class NPCThrowItem : NpcBaseAction
{
    Vector2 MovDir;
	// Use this for initialization
	public NPCThrowItem(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo) {
		
	}
    public override void SetFaceLock(bool ifLock)
    {
        base.SetFaceLock(ifLock);
        
    }
    void GetMoveDir()
    {
    }
    public override void CallPuppet(PuppetNpc puppet)
    {
        
        if(!m_DirLock )
        {
            GetMoveDir();
        }
        if(MovDir.magnitude <0.1f)
        {
            MovDir = npcActor.FaceDir;
        }
        base.CallPuppet(puppet);
        puppet.transform.rotation = Quaternion.FromToRotation(Vector2.right, MovDir);
    }
}
