/*作者:Mo
 *基础导演类 控制进入场景流程
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDir : MonoBehaviour {
    public Camera MainCamera;
    protected GameCtrl _GM;
    protected UIManager _UIMgr;
    protected SceneMgr _SceneMgr;
    //装内存池取出来的角色
    protected LinkedList<BaseActorObj> ActorList;

	protected virtual void Awake()
    {
        _GM = GameCtrl.GameCtrler;
        _UIMgr = UIManager.Mgr;
        _SceneMgr = SceneMgr.Mgr;
        _UIMgr.ClearAll();
        ActorList = new LinkedList<BaseActorObj>();
    }
    
    public virtual void EnterScene()
    {
        _UIMgr.ClearAll();
    }
    public virtual void Leave()
    {
        while(ActorList.Count>0)
        {
            BaseActorObj Actor = ActorList.First.Value;
            ActorList.RemoveFirst();
            GamePoolManager.Manager.Despawn(Actor.transform);
        }
    }

}
