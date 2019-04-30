using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObj : MonoBehaviour {
    [Title("默认追踪时间", "black")]
    public float TraceTime;
    [SerializeField]
    [Title("默认追踪对象", "black")]
    Transform _TraceTarget;
    public Transform TraceTarget
    {
        get
        {/*
            if(_TraceTarget==null&& _CurDir!=null)
            {
                _TraceTarget = _CurDir.PlayerActor.transform;
            }*/
            return _TraceTarget;
        }
        set
        {
            _TraceTarget = value;
        }
    }

    [Title("相机控制器", "black")]
    public BaseCameraControler Ctrler;
    #region 内部属性
    //BattleDir _CurDir;
    #endregion
    private void Start()
    {
        //_CurDir = SceneMgr.Mgr.CurDir as BattleDir;
    }
    private void Update()
    {
        /*
        if (Ctrler == null&&TraceTarget!=null)
        {
            Ctrler = new TraceCameraControler(transform,TraceTime, TraceTarget);
        }
        Ctrler.Update();
        */
    }


}
