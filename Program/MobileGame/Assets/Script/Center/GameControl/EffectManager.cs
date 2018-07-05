using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class EffectManager {
    SpawnPool GamePool;
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
        GamePool = GameCtrler.Pool;
    }

    PrefabPool GetPool( string InName )
    {
        PrefabPool ReturnPool = new PrefabPool();
        if ( !GamePool.prefabPools.TryGetValue( InName,out ReturnPool) )
        {
            ReturnPool = null;
            GameObject Effect = LoadEffect.LoadEffectObj(InName);
            if( Effect != null )
            {
                Transform NewEffect = LoadEffect.LoadEffectObj(InName).transform;
                ReturnPool = new PrefabPool(NewEffect);
                ReturnPool.cullDespawned = true;
                ReturnPool.cullDelay = 5;
                GamePool.CreatePrefabPool(ReturnPool);
            }
        }
        return ReturnPool;
    }
    //注册特效
    void Regist(string InName)
    {
        PrefabPool ReturnPool = new PrefabPool();
        if (!GamePool.prefabPools.TryGetValue(InName, out ReturnPool))
        {
            ReturnPool = null;
            GameObject Effect = LoadEffect.LoadEffectObj(InName);
            if (Effect != null)
            {
                Transform NewEffect = LoadEffect.LoadEffectObj(InName).transform;
                ReturnPool = new PrefabPool(NewEffect);
                ReturnPool.cullDespawned = true;
                ReturnPool.cullDelay = 5;
                GamePool.CreatePrefabPool(ReturnPool);
            }
        }
    }
    //生成特效
	public GameObject GenEffect( string InName)
    {
        Transform Target = GamePool.Spawn(InName);
        if(Target == null )
        {
            Regist(InName);
            Target = GamePool.Spawn(InName);
        }
        return Target.gameObject;
    }
}
