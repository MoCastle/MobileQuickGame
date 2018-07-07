using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActorManager {
    //根据运动方向获取旋转
	public static Vector3 GetActorRotation( Vector2 MoveDirection )
    {
        Vector2 NormDir = MoveDirection.normalized;
        float Rotate = 0;
        Rotate = Mathf.Atan2(NormDir.y, Mathf.Abs(NormDir.x)) * 180 / Mathf.PI;
        if (NormDir.x < 0)
        {
            Rotate = Rotate * -1;
        }
        Vector3 Rotation = Vector3.forward * Rotate;
        return Rotation;
    }
}
