﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BaseFunc;
public class InputStateFSM:BaseFSM
{
    InputStateFSM()
    { }
}
public class ClickState:BaseState
{
    ClickState()
    { }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class DragState : BaseState
{
    DragState()
    { }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class SlipState : BaseState
{
    SlipState()
    { }

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class InputTouch : BaseUI, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    #region 内部属性
    [SerializeField]
    Transform m_TouchRound;
    [SerializeField]
    Transform m_DistanceRound;
    [SerializeField]
    [Header("长按限定时间")]
    float m_HoldingTime;
    float m_StD = 10;//向饱和距离
    Animator m_Animator;
    #endregion

    //输入间隔设置 屏幕的一半
    #region 触发
    public void OnDrag(PointerEventData eventData)
    {
        PushingPs = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PushStart = eventData.position;
        PushingPs = eventData.position;
        //这里把初始零向量给算上 不然会少算
        InputRoundArr.Push(Vector2.zero);
        _IsInputting = true;
        m_Animator.SetTrigger("touchTrigger");
        m_Animator.SetBool("touching", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _IsInputting = false;
        OutPutCommand();
        ClearAllPs();
        //该处待优化 平白创建一个对象 浪费资源
        _InputRoundArr = null;
        m_Animator.SetBool("touching",false);
    }
    #endregion

    #region
    //缓存队列的总时间
    public float DragSpaceTime;
    public float DragDistancePer;
    //记录时间间隔
    public int DragSpaceFrame;
    float NextCountTime
    {
        get
        {
            return DragSpaceFrame * Time.fixedDeltaTime + PushCount;
        }
    }
    //帧率记录
    float PushCount = 0;
    #endregion


    StructRoundArrCovered<Vector2> _InputRoundArr;
    public StructRoundArrCovered<Vector2> InputRoundArr
    {
        get
        {
            if(_InputRoundArr == null)
            {
                _InputRoundArr = new StructRoundArrCovered<Vector2>((int)(DragSpaceTime / ((float)DragSpaceFrame * Time.fixedDeltaTime) + 1));
            }
            return _InputRoundArr;
        }
    }

    Vector2 PushStart = Vector2.zero;
    Vector2 PushingPs = Vector2.zero;
    bool _IsInputting = false;
    public Text LogOutPut;
    void Awake()
    {
        PlayerCtrl.RefreshInputRoundArr();
        //StD = GetComponent<RectTransform>().sizeDelta.x / 2;
        //StD = Screen.width / 2;
        m_StD = 40;
        m_Animator = GetComponent<Animator>();
    }
    void ClearAllPs( )
    {
        PushStart = Vector2.zero;
        PushingPs = Vector2.zero;
    }
    

	// Update is called once per frame
	void Update( )
    {
        LogOutPut.text = "ScreenWidth = " + m_StD + "\n " + PushingPs.ToString();
        if ( _IsInputting )
        {
            OutPutCommand();
            bool IfPush = true;
            /*
            //计算到达做一次记录的时间间隔
            if( PushCount > 0)
            {
                if(NextCountTime> Time.time)
                {
                    IfPush = false;
                }else
                {
                    PushCount = Time.time;
                }
            }
            else
            {
                PushCount = Time.time;
            }*/

            if( IfPush )
            {
                //向队列中放入触屏过程中的向量
                Vector2 InputVector = PushingPs - PushStart;
                InputRoundArr.Push(InputVector);
            }
            
        }
    }

    void OutPutCommand( )
    {
        Vector2 InputVector = PushingPs - PushStart;
        InputInfo OutPut = new InputInfo( true );
        OutPut.IsPushing = _IsInputting;
        if(!OutPut.IsPushing)
        {
            //最后手指按压时的向量给传进去
            InputVector = CheckMaxVectorInRound(InputVector);
        }
        OutPut.Shift = InputVector;
        OutPut.MaxDst = m_StD;
        OutPut.EndPs = PushingPs;
        PlayerCtrl.InputHandTouch(OutPut);
    }
    //判断按压期间是否达到某指令
    Vector2 CheckMaxVectorInRound( Vector2 EndInput )
    {
        InputRoundArr.InitTailEnum();
        //记录最后一个有效输入的放下
        Vector2 CurDir = Vector2.zero;
        Vector2 FirstPoint;
        Vector2 LastVector;
        while ( !InputRoundArr.IfEndTailEnum )
        {
            Vector2 PopVector = InputRoundArr.GetTailEnumT();
            //记录当前触碰点与终止输入点的向量
            Vector2 Direction = EndInput - PopVector;

            LastVector = PopVector;
            if (Direction.sqrMagnitude > 0.01f )
            {
                
                if (CurDir.sqrMagnitude < 0.1f && Direction.sqrMagnitude > (m_StD*0.1)* (m_StD * 0.1))
                {
                    CurDir = Direction;
                    FirstPoint = PopVector;
                }//对夹角进行判断
                if (CurDir == Vector2.zero)
                {

                }
                else if( Vector2.Angle(CurDir, Direction) < 90)
                {
                    //判断距离是否达标
                    if (Direction.magnitude > DragDistancePer * m_StD)
                    {
                        return Direction.normalized* m_StD;
                    }
                }
                else
                {
                    //夹角过大 发生转向
                    return Vector2.zero;
                }
            }
        }
        return Vector2.zero;
    }
}