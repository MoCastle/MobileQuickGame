using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceCameraControler :BaseCameraControler {
    //平滑时间
    float SmoothTime;
    //追踪目标
    Transform _TraceTarget;
    Transform _Tracer;
    Vector3 _SmoothSpeed;

    public TraceCameraControler(Transform tracer,float smoothTime, Transform traceTarget)
    {
        SmoothTime = smoothTime;
        _TraceTarget = traceTarget;
        _Tracer = tracer;
    }
    public override void Update()
    {
        Vector3 newPosition = Vector3.SmoothDamp(_Tracer.position, _TraceTarget.transform.position, ref _SmoothSpeed, SmoothTime);
        newPosition.z = _Tracer.position.z;
        _Tracer.position = newPosition;
    }
}
