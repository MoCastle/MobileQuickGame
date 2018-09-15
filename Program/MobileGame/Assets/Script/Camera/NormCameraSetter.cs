using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormCameraSetter : BaseCameraSetter {
    void OnTriggerEnter2D(Collider2D other )
    {
        if( other.gameObject.tag == "Player" )
            GameCtrl.GameCtrler.CurDir.CameraCtrler = new CameraControler( GameCtrl.GameCtrler.CurDir,this);
    }
}
