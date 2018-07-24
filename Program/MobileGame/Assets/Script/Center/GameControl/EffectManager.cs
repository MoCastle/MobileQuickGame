using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class EffectManager {
    GamePoolManager GamePool
    {
        get
        {
            return GamePoolManager.Manager;
        }
    }
    static EffectManager _Manager;
    public static EffectManager Manager
    {
        get
        {
            if( _Manager == null )
            {
                _Manager = new EffectManager( );
            }
            return _Manager;
        }
    }

    EffectManager()
    {
        GameCtrl GameCtrler = GameCtrl.GameCtrler;
    }

    //生成特效
    public GameObject GenEffect( string InName)
    {
        //检查是否有注册过
        GameObject SampleEffect = LoadEffect.LoadEffectObj(InName);
        if( SampleEffect == null )
        {
            return null;
        }
        if( !GamePool.IsSpawned(SampleEffect.transform) )
        {
            GamePool.Regist(InName, SampleEffect.transform);
        }
        Transform Target = GamePool.Spawn(InName);
        return Target.gameObject;
    }

    public void PutBackEffect( GameObject Effect )
    {
        GamePool.Despawn(Effect.transform);
    }
}
