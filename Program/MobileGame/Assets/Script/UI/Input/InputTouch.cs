using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using BaseFunc;
public enum InputType
{
    None,
    Touching,
    Click,
    Holding,
    Drag,
    Slip,
    Release
}

namespace UI
{
    public class InputTouch : BaseUI, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region 内部属性
        [SerializeField]
        Transform m_TouchRound;
        [SerializeField]
        Transform m_DistanceRound;
        [SerializeField]
        [Header("压扁缩放率")]
        float m_ShrinkRate;
        [SerializeField]
        [Header("最小压扁比例")]
        float m_MinShrinkRate;
        [SerializeField]
        [Header("长按限定时间")]
        float m_HoldingTime;
        [SerializeField]
        [Header("拖动判定距离")]
        float m_DragJudgeDistance;
        [SerializeField]
        [Header("滑动判定距离")]
        float m_SlipDistance;
        [SerializeField]
        [Header("滑动时间点")]
        float m_SlipTimePoint;
        float m_PushTimeStamp;
        float m_StD = 10;//向饱和距离
        Animator m_Animator;
        Vector2 PushStart = Vector2.zero;
        Vector2 PushingPs = Vector2.zero;
        bool _IsInputting = false;
        StructRoundArrCovered<Vector2> _InputRoundArr;
        InputStateFSM m_InputStateFSM;
        float m_TouchPointIconLength;
        float m_DragJudgeIconDistance;
        #endregion

        //输入间隔设置 屏幕的一半
        #region 触发
        public void OnDrag(PointerEventData eventData)
        {
            PushingPs = eventData.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_PushTimeStamp = Time.time;
            PushStart = eventData.position;
            PushingPs = eventData.position;
            m_DistanceRound.transform.position = new Vector3(PushStart.x, PushStart.y, 0);

            //这里把初始零向量给算上 不然会少算
            //InputRoundArr.Push(Vector2.zero);
            _IsInputting = true;
            m_InputStateFSM.OnPointDown();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _IsInputting = false;
            ClearAllPs();
            //该处待优化 平白创建一个对象 浪费资源
            _InputRoundArr = null;
            m_InputStateFSM.OnPointUp();
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

        public Text LogOutPut;

        void Awake()
        {
            PlayerCtrl.RefreshInputRoundArr();
            m_StD = 40;
            m_Animator = GetComponent<Animator>();
            m_InputStateFSM = new InputStateFSM(this);
            m_InputStateFSM.Init();
            m_TouchPointIconLength = m_TouchRound.GetComponent<RectTransform>().sizeDelta.x;
            m_DragJudgeIconDistance = m_DistanceRound.GetComponent<RectTransform>().sizeDelta.x;
        }

        void ClearAllPs()
        {
            //PushStart = Vector2.zero;
            //PushingPs = Vector2.zero;
        }


        // Update is called once per frame
        void Update()
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(1).IsName("touched"))
            {
                if ((m_PushTimeStamp + m_SlipTimePoint) < (Time.time))
                    m_Animator.Play("leave", 1);
            }

            LogOutPut.text = "ScreenWidth = " + m_StD + "\n " + PushingPs.ToString();

            m_InputStateFSM.Update();
        }

        //判断按压期间是否达到某指令
        Vector2 CheckMaxVectorInRound(Vector2 EndInput)
        {
            //InputRoundArr.InitTailEnum();
            //记录最后一个有效输入的放下
            Vector2 CurDir = Vector2.zero;
            Vector2 FirstPoint;
            Vector2 LastVector;
            /*
            while (!InputRoundArr.IfEndTailEnum)
            {
                Vector2 PopVector = InputRoundArr.GetTailEnumT();
                //记录当前触碰点与终止输入点的向量
                Vector2 Direction = EndInput - PopVector;

                LastVector = PopVector;
                if (Direction.sqrMagnitude > 0.01f)
                {

                    if (CurDir.sqrMagnitude < 0.1f && Direction.sqrMagnitude > (m_StD * 0.1) * (m_StD * 0.1))
                    {
                        CurDir = Direction;
                        FirstPoint = PopVector;
                    }//对夹角进行判断
                    if (CurDir == Vector2.zero)
                    {

                    }
                    else if (Vector2.Angle(CurDir, Direction) < 90)
                    {
                        //判断距离是否达标
                        if (Direction.magnitude > DragDistancePer * m_StD)
                        {
                            return Direction.normalized * m_StD;
                        }
                    }
                    else
                    {
                        //夹角过大 发生转向
                        return Vector2.zero;
                    }
                }
            }*/
            return Vector2.zero;
        }

        public void SwitchState(HandGesture inputType)
        {
            m_InputStateFSM.SwitchState(inputType);
        }

        public class InputStateFSM : BaseFSM
        {
            InputTouch m_InputTouch;
            public InputStateFSM(InputTouch inputTouch) : base()
            {
                m_InputTouch = inputTouch;
            }
            public void Init()
            {
                SwitchState(HandGesture.None);
            }
            public void SwitchState(HandGesture inputType)
            {
                BaseInputState state = null;
                switch (inputType)
                {
                    case HandGesture.Touching:
                        state = new TouchingState(m_InputTouch);
                        break;
                    case HandGesture.Click:
                        state = new ClickState(m_InputTouch);
                        break;
                    case HandGesture.Holding:
                        state = new HoldingState(m_InputTouch);
                        break;
                    case HandGesture.Drag:
                        state = new DragState(m_InputTouch);
                        break;
                    case HandGesture.Slip:
                        state = new SlipState(m_InputTouch);
                        break;
                    case HandGesture.Realease:
                        state = new ReleaseState(m_InputTouch);
                        break;
                    default:
                        state = new NoneInput(m_InputTouch);
                        break;
                }
                if (state != null)
                    Switch(state);
                else
                    Debug.Log("InputStateFSM.SwitchState " + inputType + "is None");
            }
            public void Update()
            {
                if (m_CurState == null)
                    return;
                m_CurState.Update();
            }
            public void OnPointDown()
            {
                ((BaseInputState)m_CurState).OnPointDown();
            }
            public void OnPointUp()
            {
                ((BaseInputState)m_CurState).OnPointUp();
            }
        }

        public abstract class BaseInputState : BaseState
        {
            protected InputTouch m_InputTouch;
            public BaseInputState(InputTouch inputTouch) : base()
            {
                m_InputTouch = inputTouch;
            }
            public virtual void OnPointUp()
            {
                m_InputTouch.m_Animator.SetTrigger("pointUp");

            }
            public virtual void OnPointDown()
            {
                m_InputTouch.m_Animator.SetTrigger("pointDown");
            }
            protected void MouseFollowing()
            {
                RectTransform touchRoundTransform = m_InputTouch.m_TouchRound.GetComponent<RectTransform>();
                RectTransform touchDistanceTransform = m_InputTouch.m_DistanceRound.GetComponent<RectTransform>();
                Vector2 startPS = m_InputTouch.PushStart;
                Vector2 curPS = m_InputTouch.PushingPs;
                Vector2 midlePS = (startPS + curPS) / 2;
                touchRoundTransform.position = midlePS;
                Vector2 mouseMoveVecotr = curPS - startPS;
                float touchPointRadiu = m_InputTouch.m_TouchPointIconLength;
                float distance = (curPS - startPS).magnitude;
                Vector2 newSizeDelta;
                //float m_TouchScale
                if (distance > m_InputTouch.m_TouchPointIconLength)
                {
                    distance /= touchRoundTransform.localScale.x;
                    newSizeDelta = touchRoundTransform.sizeDelta;
                    newSizeDelta.x = distance;
                    float shrinkRate = (touchPointRadiu/distance) ;
                    shrinkRate = shrinkRate * m_InputTouch.m_ShrinkRate;
                    shrinkRate = shrinkRate < m_InputTouch.m_MinShrinkRate ? m_InputTouch.m_MinShrinkRate : shrinkRate;

                    newSizeDelta.y = touchPointRadiu * shrinkRate;
                }
                else
                {
                    newSizeDelta = new Vector2(m_InputTouch.m_TouchPointIconLength, m_InputTouch.m_TouchPointIconLength);
                }
                touchRoundTransform.sizeDelta = newSizeDelta;
                Quaternion rotate = Quaternion.FromToRotation(Vector3.right, new Vector3(mouseMoveVecotr.x, mouseMoveVecotr.y, 0));
                touchRoundTransform.rotation = rotate;
            }
            protected void ResetFollowing()
            {
                RectTransform touchRoundTransform = m_InputTouch.m_TouchRound.GetComponent<RectTransform>();
                Vector2 origionSize = new Vector2(m_InputTouch.m_TouchPointIconLength, m_InputTouch.m_TouchPointIconLength);
                touchRoundTransform.sizeDelta = origionSize;
            }
            protected void PutIconBackClickPS()
            {
                RectTransform touchRoundTransform = m_InputTouch.m_TouchRound.GetComponent<RectTransform>();
                touchRoundTransform.transform.position = m_InputTouch.PushStart;

                float touchPointRadiu = m_InputTouch.m_TouchPointIconLength;
                Vector2 newSizeDelta = new Vector2(m_InputTouch.m_TouchPointIconLength, m_InputTouch.m_TouchPointIconLength);
                touchRoundTransform.sizeDelta = newSizeDelta;
            }

            protected void PutIconToUpPS()
            {
                RectTransform touchRoundTransform = m_InputTouch.m_TouchRound.GetComponent<RectTransform>();
                touchRoundTransform.transform.position = m_InputTouch.PushingPs;

                float touchPointRadiu = m_InputTouch.m_TouchPointIconLength;
                Vector2 newSizeDelta = new Vector2(m_InputTouch.m_TouchPointIconLength, m_InputTouch.m_TouchPointIconLength);
                touchRoundTransform.sizeDelta = newSizeDelta;
            }
            /// <summary>
            /// 输出指令
            /// </summary>
            /// <param name="isInputting"> 是否正处于按压状态</param>
            /// <param name="gesture"></param>
            protected void OutPutCommand(HandGesture gesture)
            {
                InputInfo OutPut = new InputInfo();
                OutPut.gesture = gesture;
                Vector2 InputVector = m_InputTouch.PushingPs - m_InputTouch.PushStart;
                if (InputVector.sqrMagnitude < Mathf.Pow(m_InputTouch.m_DragJudgeDistance, 2))
                {
                    InputVector = Vector2.zero;
                }
                OutPut.isLegal = true;
                OutPut.vector = InputVector;
                OutPut.startPS = m_InputTouch.PushStart;
                OutPut.endPS = m_InputTouch.PushingPs;
                PlayerCtrl.InputHandTouch(OutPut);
            }
        }

        public class NoneInput : BaseInputState
        {
            public NoneInput(InputTouch inputTouch) : base(inputTouch)
            {

            }

            public override void End()
            {
            }

            public override void Start()
            {
                m_InputTouch.m_Animator.SetBool("slipTimeCondition", false);
                OutPutCommand(HandGesture.None);
            }

            public override void Update()
            {
            }
        }

        public class TouchingState : BaseInputState
        {
            public TouchingState(InputTouch inputTouch) : base(inputTouch)
            {

            }

            public override void End()
            {
            }

            public override void Start()
            {
            }

            public override void Update()
            {
                if ((Time.time - m_InputTouch.m_PushTimeStamp) > m_InputTouch.m_HoldingTime)
                {
                    m_InputTouch.m_Animator.Play("holding");
                    return;
                }
                else if (Mathf.Pow(m_InputTouch.m_DragJudgeDistance, 2) < (m_InputTouch.PushingPs - m_InputTouch.PushStart).sqrMagnitude)
                {
                    m_InputTouch.m_Animator.Play("drag");
                    return;
                }
                MouseFollowing();
                OutPutCommand(HandGesture.None);
            }
        }

        public class ClickState : BaseInputState
        {
            public ClickState(InputTouch inputTouch) : base(inputTouch)
            {
            }

            public override void End()
            {
            }

            public override void Start()
            {
                PutIconBackClickPS();

                //Vector2 newSizeDelta = new Vector2(m_InputTouch.m_TouchPointIcon, m_InputTouch.m_TouchPointIcon);
                //m_InputTouch.m_TouchRound.GetComponent<RectTransform>().sizeDelta = newSizeDelta;
            }

            public override void Update()
            {
                OutPutCommand(HandGesture.Click);
            }
        }

        public class HoldingState : BaseInputState
        {
            public HoldingState(InputTouch inputTouch) : base(inputTouch) { }

            public override void End()
            {
                PutIconBackClickPS();
            }

            public override void Start()
            {
            }

            public override void Update()
            {
                MouseFollowing();
                OutPutCommand(HandGesture.Holding);
                if (Mathf.Pow(m_InputTouch.m_DragJudgeDistance, 2) < (m_InputTouch.PushingPs - m_InputTouch.PushStart).sqrMagnitude)
                {
                    m_InputTouch.m_Animator.Play("drag");
                    return;
                }
            }
        }

        public class DragState : BaseInputState
        {
            public DragState(InputTouch inputTouch) : base(inputTouch) { }

            public override void End()
            {
                PutIconBackClickPS();
            }

            public override void Start()
            {

            }

            public override void Update()
            {
                MouseFollowing();
                OutPutCommand(HandGesture.Drag);

                if (Mathf.Pow(m_InputTouch.m_DragJudgeDistance, 2) > (m_InputTouch.PushingPs - m_InputTouch.PushStart).sqrMagnitude)
                {
                    m_InputTouch.m_Animator.Play("holding");
                    return;
                }
            }

            public override void OnPointUp()
            {
                base.OnPointUp();
                if ((Time.time - m_InputTouch.m_PushTimeStamp < m_InputTouch.m_SlipTimePoint) && (Mathf.Pow(m_InputTouch.m_SlipDistance, 2) < (m_InputTouch.PushingPs - m_InputTouch.PushStart).sqrMagnitude))
                {
                    m_InputTouch.m_Animator.SetBool("slipTimeCondition", true);
                }
                else
                {
                    m_InputTouch.m_Animator.SetBool("slipTimeCondition", false);
                }
            }
        }


        public class SlipState : BaseInputState
        {
            public SlipState(InputTouch inputTouch) : base(inputTouch) { }

            public override void End()
            {
            }

            public override void Start()
            {
                OutPutCommand(HandGesture.Slip);
                PutIconToUpPS();
            }

            public override void Update()
            {
            }
        }

        public class ReleaseState:BaseInputState
        {
            public ReleaseState(InputTouch inputTouch) : base(inputTouch) { }

            public override void End()
            {
            }

            public override void Start()
            {
                OutPutCommand(HandGesture.Realease);
                PutIconToUpPS();
            }

            public override void Update()
            {
            }
        }

    }
}

