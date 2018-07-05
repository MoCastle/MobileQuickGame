using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathologicalGames;

public class EffectManager {
    SpawnPool GamePool;
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
    //生成特效
	public GameObject GenEffect( string InName)
    {
        PrefabPool EffectPool = GetPool(InName);
        if( EffectPool != null )
        {

        }
        return null;
    }
}
