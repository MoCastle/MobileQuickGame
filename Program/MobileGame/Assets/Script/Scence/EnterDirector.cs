using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDirector : MonoBehaviour {
    public Rigidbody2D PlayerTrans;
    PlayerCtrl PlayerCtrler;
    // Use this for initialization
    void Start () {
        PlayerCtrler = PlayerCtrl.PlayerCtrler;
        PlayerCtrler.SetPlayer(PlayerTrans);
    }
	
}
