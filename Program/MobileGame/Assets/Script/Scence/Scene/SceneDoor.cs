using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDoor : MonoBehaviour {
    #region 对外接口
    [Title("跳转到场景的第几个门", "black")]
    public int Idx;
    [Title("场景名", "black")]
    public string SceneName;
    #endregion
    #region 内部属性
    float CountLeaveTime = -1;
    #endregion
    BoxCollider2D _Collider;
    BoxCollider2D Colloder
    {
        get
        {
            if(_Collider == null)
            {
                _Collider = GetComponent<BoxCollider2D>();
            }
            return _Collider;
        }
    }

    public Action CollisionEnter;

    Transform playerTrans;
	public void GenPlayer(Transform inPlayerTrans)
    {
        playerTrans = inPlayerTrans;
        playerTrans.transform.position = this.transform.position;
    }
    private void Start()
    {
        if(Idx>=0&& SceneName!="")
        {
            Colloder.enabled = true;
            CollisionEnter = JumpScene;
        }
        else
        {
            Colloder.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Transform saveTrans = playerTrans;
            if (saveTrans != null)
            {
                return;
            }
            this.playerTrans = collision.transform;
            if (CollisionEnter != null&& CountLeaveTime <Time.time)
                CollisionEnter();
            CountLeaveTime = -1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CountLeaveTime = Time.time + 0.2f;
            playerTrans = null;
        }
    }

    void JumpScene()
    {
        BattleSceneInfo info = new BattleSceneInfo();
        info.Name = SceneName;
        info.Idx = Idx;
        BattleMgr.Mgr.EnterBattle(info);
    }
}
