using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePhysic
{
    //需要存盘的物理信息
    [System.Serializable]
    public struct ActorPhysicData
    {
        [Header("最大下落速度")]
        public float MaxFallingSpeed;
        [Header("下落时间到最快速度的时间")]
        public float FallingTime;
        [Header("下落时加速曲线")]
        public AnimationCurve FallingGravityCurve;
        [Header("速度衰减率/秒")]
        public float ReduceRate;
    }
}