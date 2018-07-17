using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputTouch : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public float StD = 100;//向饱和距离
    public float DragSpaceTime;
    public float DragDistancePer;
    public int DragSpaceFrame;
    float SpaceTime
    {
        get
        {
            return DragSpaceFrame * Time.fixedDeltaTime + PushCount;
        }
    }
    float PushCount = 0;
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
        StD = GetComponent<RectTransform>().sizeDelta.x / 2;
    }
    void ClearAllPs( )
    {
        PushStart = Vector2.zero;
        PushingPs = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        PushingPs = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PushStart = eventData.position;
        PushingPs = eventData.position;
        _IsInputting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _IsInputting = false;
        OutPutCommand( );
        ClearAllPs();
        _InputRoundArr = null;
    }

	// Update is called once per frame
	void Update( )
    {
        LogOutPut.text = PushingPs.ToString();
        if( _IsInputting )
        {
            OutPutCommand();
            bool IfPush = true;
            if( PushCount > 0)
            {
                if(SpaceTime> Time.time)
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
            }

            if( IfPush )
            {
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
            InputVector = CheckMaxVectorInRound(InputVector);
        }
        OutPut.Shift = InputVector;
        OutPut.MaxDst = StD;
        OutPut.EndPs = PushingPs;
        PlayerCtrl.InputHandTouch(OutPut);
    }
    //判断按压期间是否达到某指令
    Vector2 CheckMaxVectorInRound( Vector2 EndInput )
    {
        InputRoundArr.InitTailEnum();
        //记录最后一个有效输入的放下
        Vector2 CurDir = Vector2.zero; ;
        while( !InputRoundArr.IfEndTailEnum )
        {
            Vector2 PopVector = InputRoundArr.GetTailEnumT();
            //记录当前触碰点与终止输入点的向量
            Vector2 Direction = EndInput - PopVector;
            if (Direction.sqrMagnitude > 0.01f )
            {
                if(CurDir.sqrMagnitude < 0.01f)
                {
                    CurDir = Direction;
                }//对夹角进行判断
                if (Vector2.Angle(CurDir, Direction) < 90)
                {
                    //判断距离是否达标
                    if (Direction.sqrMagnitude > Mathf.Pow(DragDistancePer * StD,2))
                    {
                        Debug.Log("InputTouch: CheckOver direction Distance:" + Direction.sqrMagnitude + "    Target Distance:" + Mathf.Pow(DragDistancePer * StD, 2));
                        return Direction.normalized* StD;
                    }
                    Debug.Log("InputTouch:direction Distance:" + Direction.sqrMagnitude + "    Target Distance:" + Mathf.Pow(DragDistancePer * StD, 2));
                }
                else
                {
                    Debug.Log("InputTouch:CheckMaxVectorInRound:OutOfRotate:" + Vector2.Angle(CurDir, Direction));
                    //夹角过大 发生转向
                    return Vector2.zero;
                }
            }
        }
        return Vector2.zero;
    }
}
