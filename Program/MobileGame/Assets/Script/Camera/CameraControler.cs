using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler {
    public CameraControler( BaseDir ScenceDir )
    {
        Dir = ScenceDir;
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
	// Update is called once per frame
	public void Update () {
		if( CameraTrans!=null && Follower != null )
        {
            Vector3 NewCameraPs = Follower.TransCtrl.position;
            NewCameraPs.z = CameraTrans.position.z;
            CameraTrans.position = NewCameraPs;
        }
	}
}
