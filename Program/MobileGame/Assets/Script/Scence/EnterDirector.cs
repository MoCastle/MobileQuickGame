using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDirector : MonoBehaviour {
    public Rigidbody2D PlayerTrans;
    PlayerCtrl PlayerCtrler;
    // Use this for initialization
    void Start () {
        Debug.Log("EnterdirectStart");
        PlayerCtrler = PlayerCtrl.PlayerCtrler;
        Debug.Log("GetPlayerCtrl");
        PlayerCtrler.SetPlayer(PlayerTrans);
        Debug.Log("SetPlayer");
        GameWorldTimer.Continue();
        Debug.Log("EnterBattle");
    }
	
}
