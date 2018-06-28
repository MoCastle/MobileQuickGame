using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : BaseActor
{
    BoxCollider2D _GuardingArea;
    BoxCollider2D GuardingArea
    {
        get
        {
            if( _GuardingArea == null )
            {
                _GuardingArea = transform.FindChild("GuardingArea").GetComponent<BoxCollider2D>();
            }
            return _GuardingArea;
        }
    }
    BaseAI AICtrler;
    public float RunSpeed = 1;
    public void Awake()
    {
        AICtrler = new GuardAI(this);
    }
    public void Update()
    {
        AICtrler.Update();
    }
    public void CheckGetPlayer( )
    {
        Collider2D[] ColliderList = new Collider2D[1];
        ContactFilter2D ContactFilter = new ContactFilter2D();
        LayerMask Layer = 1 << LayerMask.NameToLayer("Player");
        ContactFilter.SetLayerMask(Layer);
        GuardingArea.OverlapCollider(ContactFilter, ColliderList);
        return;
    }
}
