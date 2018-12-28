using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler {
    /*
    Vector2 LeftTopPs;
    Vector2 RightDownPs;
    float Distance;
    Vector2 ViewSize;
    Vector2 TriggerSize;
    Vector2 CameraOffset;

    //相机向前偏移相关
    #region
    float ShiftNum = 1;
    float RealShiftNum
    {
        get
        {
            return Follower.FaceDirection.x * ShiftNum < 0 ? ShiftNum * -1 : ShiftNum;
        }
    }
    float CurShiftNum = 0;
    #endregion
    public CameraControler( BaseDir ScenceDir, BaseCameraSetter InCameraSetter )
    {
        Dir = ScenceDir;
        CameraSetter = InCameraSetter;
        //算出左上右下四个边界
        
        BoxCollider2D TheTrigger = CameraSetter.GetComponent<BoxCollider2D>();
        Vector2 TriggerOffset = TheTrigger.offset;

        Vector2 SetterPs = CameraSetter.transform.position;
        SetterPs += TriggerOffset;


        LeftTopPs = SetterPs;
        RightDownPs = SetterPs;

        TriggerSize = TheTrigger.size;
        LeftTopPs.x = LeftTopPs.x - TriggerSize.x/2;
        LeftTopPs.y = LeftTopPs.y + TriggerSize.y/2;
        RightDownPs.x = RightDownPs.x + TriggerSize.x/2;
        RightDownPs.y = RightDownPs.y - TriggerSize.y/2;
        Distance = Mathf.Abs(TheTrigger.transform.position.z - MainCamera.transform.position.z);

        //计算相机左上右下四个边界
        ViewSize = new Vector2();
        if (MainCamera.orthographic)
        {
            ViewSize.x = MainCamera.orthographicSize;
        }
        else
        {
            float halfFOV = (MainCamera.fieldOfView * 0.5f) * Mathf.Deg2Rad;
            ViewSize.y = Distance * Mathf.Tan(halfFOV);
        }
        ViewSize.x = ViewSize.y * MainCamera.aspect;

        CameraOffset = new Vector2();
        if (!MainCamera.orthographic)
        {
            Vector3 CameraRotate = MainCamera.transform.localRotation.eulerAngles;
            if ( Mathf.Abs(CameraRotate.x)> 0.001f)
            {
                float OffsetY = 0;
                OffsetY = Distance * Mathf.Abs(Mathf.Tan(CameraRotate.x * Mathf.Deg2Rad)) * (CameraRotate.x < 0 ? 1 : -1);
                CameraOffset.y += OffsetY;
            }
            
            if(Mathf.Abs(CameraRotate.y) > 0.001f)
            {
                float OffsetX = 0;
                OffsetX = Distance * Mathf.Abs(Mathf.Tan(CameraRotate.y * Mathf.Deg2Rad)) * (CameraRotate.y > 0 ? 1 : -1);
                CameraOffset.x += OffsetX;
            }
        }
        
    }

    public Camera MainCamera
    {
        get
        {
            return Dir.MainCamera;
        }
    }
    public Transform CameraTrans
    {
        get
        {
            return Dir.CameraTrans;
        }
    }

    BaseDir Dir;
    BaseActor Follower
    {
        get
        {
            return Dir.Player;
        }
    }
    BaseCameraSetter CameraSetter;
    
	// Update is called once per frame
	public void Update () {
		if( CameraTrans!=null && Follower != null && CameraSetter!= null )
        {
            Vector3 NewCameraPs = Follower.TransCtrl.position;
            NewCameraPs.z = CameraTrans.position.z;

            NewCameraPs = OffSet(NewCameraPs);
            NewCameraPs = PositionFix(NewCameraPs);
            NewCameraPs.x += CameraOffset.x;
            NewCameraPs.y += CameraOffset.y;
            CameraTrans.position = NewCameraPs;
        }
	}

    //进行偏移
    public Vector3 OffSet( Vector3 InPosition )
    {
        if( Mathf.Abs( CurShiftNum - RealShiftNum) <0.1f )
        {
            CurShiftNum = RealShiftNum;
        } else
        {
            CurShiftNum += (RealShiftNum - CurShiftNum) * 0.03f;
        }

        InPosition.x = InPosition.x + CurShiftNum;
        return InPosition;
    }
    Vector3 PositionFix( Vector3 InPosition )
    {
        
        Vector2 ViewLeftTop = InPosition;
        ViewLeftTop.x = ViewLeftTop.x - ViewSize.x;
        ViewLeftTop.y = ViewLeftTop.y + ViewSize.y;
        Vector2 ViewRightDown = InPosition;
        ViewRightDown.x = ViewRightDown.x + ViewSize.x;
        ViewRightDown.y = ViewRightDown.y - ViewSize.y;

        //位置修正 左右边界
        if( ViewSize.x*2> TriggerSize.x)
        {
            InPosition.x = (LeftTopPs.x + RightDownPs.x) / 2;
        }
        else if ( LeftTopPs.x > ViewLeftTop.x )
        {
            InPosition.x = LeftTopPs.x + ViewSize.x;
        }else if( RightDownPs.x< ViewRightDown.x )
        {
            InPosition.x = RightDownPs.x - ViewSize.x;
        }

        //上下边界修正
        if( ViewSize.y*2 > TriggerSize.y )
        {
            InPosition.y = (LeftTopPs.y + RightDownPs.y) / 2;
        }
        else if( LeftTopPs.y < ViewLeftTop.y )
        {
            InPosition.y = LeftTopPs.y - ViewSize.y;
        }else if( RightDownPs.y > ViewRightDown.y )
        {
            InPosition.y = RightDownPs.y + ViewSize.y;
        }

        return InPosition;
    }
    */
}
