using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePhysic;

namespace GamePhysic
{

    public class PhysicComponent : MonoBehaviour
    {
        #region 填写数据
        [SerializeField]
        ActorPhysicData m_PhysicData;
        #endregion
        #region 内部成员
        Rigidbody2D m_RigidBody;
        [SerializeField]
        [Header("头部碰撞盒")]
        BoxCollider2D m_Collider2D;
        [SerializeField]
        [Header("脚步碰撞盒")]
        BoxCollider2D m_FootCollider2D;
        BoxCollider2D m_CurPlatFloor;

        //物理
        const int g_RayNum = 3;
        float m_EvgGraphic;
        bool m_IsOnGround;
        LayerMask m_GroundMask;
        LayerMask m_PlaneMask;
        Quaternion m_NormalTrans;

        //速度
        Vector2 m_MoveSpeed;
        float m_LastFallingTime;
        Vector2 m_RealSpeed;
        #endregion
        #region 内部接口
        LayerMask GroundLayer
        {
            get
            {
                return m_GroundMask + m_PlaneMask;
            }
        }
        //重力加速度
        float EvgGraphic
        {
            get
            {
#if UNITY_EDITOR
                return m_PhysicData.MaxFallingSpeed / m_PhysicData.FallingTime;
#else
                            return m_EvgGraphic;
#endif
            }
        }
        //物理检测
        Vector2 ColliderPosition
        {
            get
            {
                return m_Collider2D.offset + (Vector2)this.transform.position;
            }
        }
        Vector2 ColliderSize
        {
            get
            {
                Vector2 size = this.transform.localScale;
                size.x = m_Collider2D.size.x * size.x + 0.1f;
                size.y = m_Collider2D.size.y * size.y;
                return size;
            }
        }
        Vector2 GroundRayLP
        {
            get
            {
                return ColliderPosition + Vector2.left * ColliderSize.x / 2 + Vector2.down * ColliderSize.y / 2 + Vector2.up * 0.1f;
            }
        }
        Vector2 GroundRayRP
        {
            get
            {
                return ColliderPosition + Vector2.right * ColliderSize.x / 2 + Vector2.down * ColliderSize.y / 2 + Vector2.up * 0.1f;
            }
        }
        float RayGraphicLength
        {
            get
            {
                float length = 0.4f;
                length = IsOnGround && (m_RigidBody.velocity.y  > length) ? m_RigidBody.velocity.y : length;
                return -length;
            }
        }

        //运动
        Vector2 RealSpeed
        {
            get
            {
                return m_RealSpeed;
            }
            set
            {
                if (m_LastFallingTime > 0 && value.y > 0)
                {
                    //没有在下落时 清理时间戳
                    if (m_RealSpeed.y < 0)
                    {
                        m_LastFallingTime = 0;
                    }
                }
                else if (m_LastFallingTime <= 0 && value.y < 0)
                {
                    //ToDo
                    m_LastFallingTime = Time.time;
                }
                m_RealSpeed = value;
            }
        }
        float Direction
        {
            get
            {
                return this.transform.localScale.x > 0 ? 1 : -1;
            }
        }
        //下坠时间比例
        float FallingTimeScale
        {
            get
            {
                //ToDo
                float time = Time.time - m_LastFallingTime;
                return time < m_PhysicData.FallingTime ? (time / m_PhysicData.FallingTime) : 1;
            }
        }
        #endregion
        #region 对外接口
        //判断是否在地面
        public bool IsOnGround
        {
            get
            {
                return m_IsOnGround;
            }
            set
            {
                if(m_IsOnGround && !value )
                {
                    onLeaveGround();
                }
                m_IsOnGround = value;
            }
        }

        //输入速度
        public Vector2 MoveSpeed
        {
            get
            {
                return m_MoveSpeed;
            }
            set
            {
                m_MoveSpeed = value;
            }
        }

        #endregion
        #region 物理
        //判断是否在地上
        void SetGoroundInfo()
        {
            Vector2 startPS = (Direction < 0 ? GroundRayLP : GroundRayRP);
            Vector2 endPS = (Direction < 0 ? GroundRayRP : GroundRayLP);
            bool onGround = false;
            for( int loopTime =0; loopTime < g_RayNum; ++loopTime)
            {
                Vector2 rayPS = Vector2.Lerp( startPS, endPS, ( float )loopTime / ( g_RayNum - 1 ));
#if UNITY_EDITOR
                Debug.DrawRay( rayPS, Vector2.up * RayGraphicLength, new Color(0.9f,0.5f,0,3f ));
#endif
                RaycastHit2D hitPlane = Physics2D.Raycast( rayPS, Vector2.up, RayGraphicLength, GroundLayer);
                if ( hitPlane.collider )
                {
                    onGround = true;
                    m_NormalTrans = Quaternion.FromToRotation(Vector2.up, hitPlane.normal);
                    break;
                }
            }
            IsOnGround = onGround;
#if UNITY_EDITOR
            Debug.DrawRay(this.transform.position, m_NormalTrans * (Vector2.up * 5), new Color(0.3f, 0.9f, 0, 1));
#endif
        }
        #endregion
        #region 流程
        private void Awake()
        {
            Reset();
            m_RigidBody = GetComponent<Rigidbody2D>();
            m_GroundMask = 1 << LayerMask.NameToLayer("Ground");
            m_PlaneMask = 1 << LayerMask.NameToLayer("PlatForm");
            if (m_Collider2D == null || m_FootCollider2D == null)
            {
                string errorInfo = m_Collider2D == null ? "BodyCollider" : "FootCollider";
                Debug.Log("Error:" + errorInfo + " Is Null");
            }
            m_FootCollider2D.isTrigger = false;
        }

        void Reset()
        {
            m_IsOnGround = false;
            m_LastFallingTime = 0;
        }

        void FramePrepare()
        {
            ReduceSpeed();
        }

        void CountLastFrame()
        {
            SetGoroundInfo();
        }

        private void Start()
        {
            m_MoveSpeed = new Vector2(0, 40);
        }

        private void Update()
        {
            FramePrepare();
            CountLastFrame();
            CountSpeed();
            //m_MoveSpeed = new Vector2(20, 0);
            Debug.Log(m_RigidBody.velocity);
        }

        #endregion
        #region 速度计算
        //衰减速度
        void ReduceSpeed()
        {
            m_RealSpeed *= (1 - m_PhysicData.ReduceRate);
        }

        //每帧计算一次下落速度
        float CountFallSpeed()
        {
            float fallSpeed = 0; ;
            if (RealSpeed.y <= 0)
                fallSpeed = -m_PhysicData.FallingGravityCurve.Evaluate(FallingTimeScale) * m_PhysicData.MaxFallingSpeed;
            else
            {
                float minuSpeed = EvgGraphic * Time.deltaTime;
                fallSpeed = RealSpeed.y > minuSpeed ? RealSpeed.y - minuSpeed : 0;
            }
            return (IsOnGround && fallSpeed< (EvgGraphic / 10))? - EvgGraphic / 10 : fallSpeed;
        }

        //计算实际速度
        void CountSpeed()
        {
            Vector2 realSpeed = RealSpeed;
            if (m_MoveSpeed.y == 0)
                realSpeed.y = CountFallSpeed();
            else
                realSpeed.y = m_MoveSpeed.y;
            if (m_MoveSpeed.x != 0)
                realSpeed.x = m_MoveSpeed.x;
            m_MoveSpeed = Vector2.zero;

            RealSpeed = realSpeed;
            //m_RigidBody.velocity = RealSpeed;
            m_RigidBody.velocity = m_NormalTrans * RealSpeed;
        }
        #endregion
        #region 事件
        void onLeaveGround()
        {
            m_LastFallingTime = 0;
            Debug.Log("LeaveGround");
        }
        #endregion
    }
}

