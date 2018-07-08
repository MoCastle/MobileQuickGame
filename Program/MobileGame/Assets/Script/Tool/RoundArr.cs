using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//往里放命令不会把前面的挤础去
public class StructRoundArr<T> where T : struct
{
    protected T[] TArray = null;
    protected int _HeadPoint = 0;
    protected int _TailPoint = 0;

    public StructRoundArr(int ArrayLength)
    {
        TArray = new T[ArrayLength];
        _HeadPoint = 0;
        _TailPoint = 0;
    }
    public T HeadInfo
    {
        get
        {
            return TArray[_HeadPoint];
        }
    }

    public T TailInfo
    {
        get
        {
            return TArray[_TailPoint];
        }
        set
        {
            TArray[_TailPoint] = value;
        }
    }

    //往里压
    public void Push( T Target )
    {
        TArray[_TailPoint] = Target;
        if ( CountAddPoint(_TailPoint) != _HeadPoint )
        {
            AddTailPoint();
        }
        
    }

    //向外弹
    public T Pop(  )
    {
        T Output = TArray[_HeadPoint];
        TArray[_HeadPoint] = new T();
        if (_HeadPoint != _TailPoint)
        {
            AddHeadPoint();
        }
        return Output;
    }

    //指针操作
    public int CountAddPoint( int Point )
    {
        Point = Point + 1;
        Point = Point % ( TArray.Length );
        return Point;
    }
    //指针前推操作
    public int CountReducePoint(int Point)
    {
        if( TArray.Length<1 )
        {
            return 0;
        }
        Point = Point - 1;
        if(Point<0)
        {
            Point = TArray.Length - Point;
        }
        return Point;
    }

    public void AddHeadPoint( )
    {
        _HeadPoint = CountAddPoint( _HeadPoint );
    }
    public void AddTailPoint()
    {
        _TailPoint = CountAddPoint(_TailPoint);
    }
}

public class StructRoundArrCovered<T>:StructRoundArr<T> where T : struct
{
    public bool IfEndTailEnum
    {
        get
        {
            return CurPoint == _HeadPoint && TArray.Length < 1;
        }
    }
    int CurPoint;
    public StructRoundArrCovered(int ArrayLength):base(ArrayLength)
    {
    }
    //往里压
    public void Push(T Target)
    {
        if (CountAddPoint(_TailPoint) == _HeadPoint)
        {
            Pop();
        }
        base.Push(Target);
    }

    //反向遍历
    public void InitTailEnum( )
    {
        CurPoint = _TailPoint;
    }
    //反向获取当前元素
    public T GetTailEnumT()
    {
        T Target = TArray[CurPoint];
        CurPoint = CountReducePoint(CurPoint);
        return Target;
    }
}