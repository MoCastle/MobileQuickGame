using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//场景控制器
public class CenceCtrl {
    //当前场景导演
    public BaseCence CurCence;
    //当前场景名称
    public string CurCenceName;
    bool _IfEnterCence = false;

    public CenceCtrl( )
    {
        EnterCence("StartGame", new EnterCence( this ) );
    }

    //跳转到某个场景
	public void EnterCence( string CenceName,BaseCence InCence )
    {
        if( InCence==null )
        {
            return;
        }
        Application.LoadLevel(CenceName);
        if(CurCence != null)
        {
            CurCence.End();
        }
        _IfEnterCence = false;
        CurCence = InCence;
    }
   
    public void Update( )
    {
        //加载中
        if( Application.isLoadingLevel )
        {
            return;
        }
        if( CurCence !=null &&!_IfEnterCence )
        {
            _IfEnterCence = true;
            CurCence.Start();
            return;
        }
        CurCence.Update();
        
    }
}
