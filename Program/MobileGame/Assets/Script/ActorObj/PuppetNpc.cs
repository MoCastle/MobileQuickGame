using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;

public class PuppetNpc : EnemyObj {
    public BaseActorObj Master;
    
    public void SetMaster(BaseActorObj master )
    {
        Master = master;
    }
    #region 对外提供接口
    //改敌人识别
    public void SetIDLayer(int id)
    {
        m_IDLayer = id;
    }
    //该属性
    public void SetProppty( Propty propty )
    {
        m_ActorPropty = propty;
    }
    #endregion

}
