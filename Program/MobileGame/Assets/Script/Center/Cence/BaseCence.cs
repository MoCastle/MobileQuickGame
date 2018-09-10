using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct ScenceMsg
{
    //当前重生ID
    int CurBirthSpawn;
    
}

public class BaseCence {
    string CenceName;
    CenceCtrl CenceCtrl;
    BaseDir Director;
    public BaseCence( CenceCtrl InCenceCtrl,string InCenceName )
    {
        CenceCtrl = InCenceCtrl;
        CenceName = InCenceName;
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
            Director.StartGame();
        }
    }

    //末
    public virtual void End( )
    {
        Director.End();
    }

    //更新
    public virtual void Update( )
    {
        
    }
}
