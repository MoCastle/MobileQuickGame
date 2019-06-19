using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class NpcBaseAction : BaseAction {
    
    protected EnemyObj npcActor;
    protected float countTime;

    protected NPCActionControler owner
    {
        get
        {
            return m_Owner as NPCActionControler;
        }
    }

    // Use this for initialization
    public NpcBaseAction(BaseActorObj baseActorObj, SkillInfo skillInfo) :base(baseActorObj, skillInfo) {
        npcActor = baseActorObj as EnemyObj;
    }

    protected Vector3 CountNextPoint()
    {
        Vector3 targetPosition;
        Vector3 leftVector = owner.leftGuardPoint - npcActor.transform.position;
        Vector3 rightVector = owner.rightGuardPoint - npcActor.transform.position;

        targetPosition = leftVector.sqrMagnitude > rightVector.sqrMagnitude ? owner.leftGuardPoint : owner.rightGuardPoint;
        return targetPosition;
    }

    protected PlayerObj SearchingPlayer(BoxCollider2D collider)
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        ContactFilter.SetLayerMask(LayerMask.GetMask("Player"));
        collider.OverlapCollider(ContactFilter, ColliderList);
        PlayerObj player = null;
        if(ColliderList[0]!=null)
        {
            player = BaseActorObj.GetActorByColliderTransfor( ColliderList[0].transform) as PlayerObj;
        }
        return player;
    }

    protected void SearchingPlayerByGuardArea()
    {
        PlayerObj player = SearchingPlayer(npcActor.GuardinArea);
        if (player != null)
            owner.target = player;
    }

    /// <summary>
    /// 设置需要生成得随机时间
    /// </summary>
    /// <param name="maxRandomValue">时间最大值</param>
    /// <param name="minRandomValue">最小值</param>
    public void SetRandomTime(float maxRandomValue,float minRandomValue)
    {
        countTime = maxRandomValue - minRandomValue;
        countTime *= Random.value;
    }
    public override void Start()
    {
        base.Start();
        if(countTime>0)
        {
            owner.timeTamp = Time.time + countTime;
        }
    }
}
