using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDirector : MonoBehaviour {
    public BaseActor Player;
    public Transform _MainCamera;
    // Use this for initialization
    void Start () {
        
        GameWorldTimer.Continue();
    }
    private void Update()
    {
        Vector3 NewPs = Player.TransCtrl.position;
        Vector3 OldPs = _MainCamera.transform.position;
        OldPs.x = NewPs.x;
        OldPs.y = NewPs.y;
        _MainCamera.transform.position = OldPs;
    }

}
