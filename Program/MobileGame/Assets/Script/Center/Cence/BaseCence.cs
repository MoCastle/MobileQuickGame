using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ScenceMsg
{
    public int JumpID;
    public string CenceName;
    public ScenceMsg( string InCenceName = "",int InJumpID = -1 )
    {
        CenceName = InCenceName;
        JumpID = InJumpID;
    }
}

public class BaseCence {
    string CenceName;
    protected CenceCtrl CenceCtrl;
    BaseDir Director;
    ScenceMsg _Msg;
    public ScenceMsg Msg
    {
        get
        {
            return _Msg;
        }
    }

    public BaseCence( CenceCtrl InCenceCtrl, ScenceMsg InMsg )
    {
        CenceCtrl = InCenceCtrl;
        CenceName = InMsg.CenceName;
        _Msg = InMsg;
    }

    //始
    public void Start ( )
    {
        GameObject DirObj = GameObject.Find("Director");
        if( DirObj !=null )
        {
            Director = DirObj.GetComponent<BaseDir>();
        }
        if( Director == null )
        {
            Debug.Log("Director Obj Wrong");
        }else
        {
            Director.StartGame(Msg);
        }
    }

    //末
    public virtual void End( )
    {
        if( Director!= null )
            Director.End();
    }

    //更新
    public virtual void Update( )
    {
        
    }
    
}
