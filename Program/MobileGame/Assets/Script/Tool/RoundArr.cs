using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoundArr<T>:IEnumerable
{
    //迭代器定义
    #region
    IEnumerator IEnumerable.GetEnumerator()
    {
        bool Start = true;
        for(int CurEnumPoint = CurFirstPoint; !Empty&&(Start||CurEnumPoint != CountAddCurPoint(CurPoint)); CurEnumPoint = CountAddFirstPoint(CurEnumPoint) )
        {
            Start = false;
            yield return TArray[CurEnumPoint];
        }
    }
    #endregion
    /*
    //枚举器定义
    #region 
    bool StartEnum = false;
    int CurEnumPoint = -1;
    public object Current
    {
        get
        {
            return TArray[CurEnumPoint];
        }
    }
    public bool MoveNext()
    {
        //空的 不用看了
        if (Empty)
        {
            return false;
        }
        CurEnumPoint = CountAddFirstPoint(CurEnumPoint);
        
        if(!StartEnum && CurEnumPoint == CurPoint )
        {
            return false;
        }
        StartEnum = false;
        return true;
    }

    public void Reset()
    {
        CurEnumPoint = CurFirstPoint - 1;
        StartEnum = true;
    }
    #endregion*/

    //可重写部分
    //当前指针位置
    protected abstract int CurPoint
    {
        get;
    }
    //当第一个元素指针位置
    protected abstract int CurFirstPoint
    {
        get;
    }
    //把当前指针向下移
    protected abstract void AddCurPoint();
    //将第一个元素指针向后推
    protected abstract void AddFirstPoint();
    //计算指针下移后的位置
    protected abstract int CountAddCurPoint(int InTailPoint);
    protected abstract int CountAddFirstPoint(int FirstPoint);

    //内部数据
    //缓存数据内容
    protected T[] TArray = null;
    //头元素指针
    protected int _HeadPoint = 0;
    //尾元素指针
    protected int _TailPoint = 0;
    //每次移出元素时做判断
    protected bool _Empty = true;

    //外部功能
    public RoundArr( int ArrayLength )
    {
        //初始化环状队列长度
        TArray = new T[ArrayLength];
        _HeadPoint = 0;
        _TailPoint = 0;
    }
    //获取头元素
    public T HeadInfo
    {
        get
        {
            return TArray[_HeadPoint];
        }
    }
    //尾元素
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
    //获取当前元素指针所指元素
    public T CurInfo()
    {
        return TArray[CurPoint];
    }
    
    //塞入元素
    public void AddInfo( T InInfo )
    {
        //如果原本就是空的 直接放进去 否则指针向后移找位置
        if(!Empty)
        {
            AddCurPoint();
        }
        
        TArray[CurPoint] = InInfo;
        //一有数据必然不为空
        _Empty = false;
    }

    //弹出元素
    public T PopInfo()
    {
        T ReturnInfo = TArray[CurPoint];
        AddFirstPoint();
        //一旦拿出元素的时候头尾相碰 则说明已经空了
        if(_HeadPoint == _TailPoint)
        {
            _Empty = true;
        }
        return ReturnInfo;
    }
    
    //判断是否还有元素 取元素前必须先做判断
    public bool Empty
    {
        get
        {
            return _Empty;
        }
    }
}

//从头部插入元素 当前元素指针指向尾部元素 且不断放入将覆盖环头
public class HeadInRoundArr<T>:RoundArr<T>
{

    public HeadInRoundArr(int ArrayLength):base(ArrayLength)
    {
    }

    
    //私有成员
    protected override int CurPoint
    {
        get
        {
            return _TailPoint;
        }
    }

    //当前第一个元素指针
    protected override int CurFirstPoint
    {
        get
        {
            return _HeadPoint;
        }
    }



    //放入无限制 随便放 大不了把头顶掉
    protected override void AddCurPoint()
    {
        _TailPoint = CountAddCurPoint(_TailPoint);
        //头部元素将被覆盖
        if (_TailPoint == _HeadPoint)
        {
            AddFirstPoint();
        }
    }

    //计算尾指针下移后的位置
    protected override int CountAddCurPoint(int InTailPoint)
    {
        InTailPoint = InTailPoint + 1;
        InTailPoint = InTailPoint % TArray.Length;
        return InTailPoint;
    }

    //头向后移动
    protected override void AddFirstPoint()
    {
        //到底了 推不动了
        _HeadPoint = CountAddFirstPoint(_HeadPoint);
    }
    
    protected override int CountAddFirstPoint(int FirstPoint)
    {
        //到底了 推不动了
        if (Empty)
        {
            return FirstPoint;
        }
        FirstPoint = FirstPoint + 1;
        FirstPoint = FirstPoint % TArray.Length;
        return FirstPoint;
    }
}

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
    //获取头信息
    public T HeadInfo
    {
        get
        {
            return TArray[_HeadPoint];
        }
    }
    //获取尾信息
    public T TailInfo
    {
        get
        {
            int Point = _TailPoint;
            if( CountReducePoint( Point ) != _HeadPoint )
            {
                Point = Point - 1;
            }
            if(Point< 0)
            {
                Point = TArray.Length - 1;
            }
            return TArray[Point];
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
            Point = TArray.Length + Point;
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
            
            return TailCurPoint == _HeadPoint || TArray.Length < 1;
        }
    }
    int _CurPoint;
    int TailCurPoint
    {
        get
        {
            return _CurPoint;
        }
        set
        {
            _CurPoint = value;
            if( _CurPoint < 0 || _CurPoint > TArray.Length )
            {
                string trackStr = new System.Diagnostics.StackTrace().ToString();
                Debug.Log("TrasckStrace:trackStr" + "Wrong:StructRoundArrCovered PointWrong:" + _CurPoint);
                _CurPoint = _TailPoint;
            }
        }
    }
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

    //反向遍历初始化
    public void InitTailEnum( )
    {
        TailCurPoint = _TailPoint;
        if( TailCurPoint != _HeadPoint )
        {
            TailCurPoint = (TailCurPoint + TArray.Length - 1)%TArray.Length;
        }
    }
    //反向获取当前元素
    public T GetTailEnumT()
    {
        if( TailCurPoint > TArray.Length -1 || TailCurPoint < 0 )
        {
            Debug.Log( "RoundArr GetTailEnum:CurPoint" + TailCurPoint + "   Tarray.Length:"+ TArray.Length);
            return new T();
        }
        T Target = TArray[TailCurPoint];
        TailCurPoint = CountReducePoint(TailCurPoint);
        return Target;
    }
}