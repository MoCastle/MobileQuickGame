using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScenceObj : MonoBehaviour {
    //当前导演
    protected BaseDir CurDir;

    protected abstract void Action();
    
    public virtual void Start()
    {
        BaseDir Dir = GameCtrl.GameCtrler.CenceCtroler.CurCence.Director;
        if(Dir.IsAction)
        {
            Action();
            return;
        }
        CurDir = GameCtrl.GameCtrler.CenceCtroler.CurCence.Director;
        CurDir.ActionEvent += Action;
    }
}
