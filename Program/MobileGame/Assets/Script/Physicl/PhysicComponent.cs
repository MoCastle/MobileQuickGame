using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePhysic;
using UnityEngine.UI;

namespace GameScene
{

    public class PhysicComponent : MonoBehaviour
    {
        #region 填写数据
        [SerializeField]
        [Header("物理碰撞数据")]
        ActorPhysicData m_PhysicData;
        #endregion
        #region 内部成员
        Rigidbody2D m_RigidBody;
        [SerializeField]
        [Header("身体碰撞盒")]
        BoxCollider2D m_Collider2D;
        [SerializeField]
        [Header("平台碰撞盒")]
        BoxCollider2D m_FootCollider2D;

        //暂停事件补偿
        float m_PauseTime;
        //当前脚下
        Collider2D m_StepStone;
        bool m_IsPausing;

        //物理
        const int g_RayNum = 3;
        float m_EvgGraphic;
        [SerializeField]
        [Header("平台碰撞盒")]
        bool m_IsOnGround;
        bool m_IsJustOnGround;
        float m_JustOnGroundTime;
        LayerMask m_GroundMask;
        LayerMask m_PlatFormMask;
        Quaternion m_NormalTrans;
        Vector2 m_BackUpSpeed;
        float m_GravityScale = 1;
        //速度
        Vector2 m_MoveSpeed;
        float m_LastFallingTime;
        [SerializeField]
        [Header("当前运动速度 角色先后左右上下")]
        Vector2 m_RealSpeed;
        #endregion
        #region 内部接口
        LayerMask GroundLayer
        {
            get
            {
                return m_GroundMask + m_PlatFormMask;
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
                Vector2 offset = m_Collider2D.offset;
                Vector2 size = m_Collider2D.transform.lossyScale;
                offset.x *= size.x;
                offset.y *= size.y;
                return offset + (Vector2)m_Collider2D.transform.position;
            }
        }
        Vector2 ColliderSize
        {
            get
            {
                Vector2 size = m_Collider2D.transform.lossyScale;
                size.x = m_Collider2D.size.x * size.x;
                size.y = m_Collider2D.size.y * size.y;
                return size;
            }
        }
        Vector2 GroundRayLP
        {
            get
            {
                return ColliderPosition + Vector2.left * ColliderSize.x / 2 * 0.9f + Vector2.down * ColliderSize.y / 2 * 0.9f;// + Vector2.up * 0.1f * ColliderSize.y;
            }
        }
        Vector2 GroundRayRP
        {
            get
            {
                return ColliderPosition + Vector2.right * ColliderSize.x / 2 * 0.9f + Vector2.down * ColliderSize.y / 2 * 0.9f;// + Vector2.up * 0.1f* ColliderSize.y;
            }
        }
        float RayGraphicLength
        {
            get
            {
                float length = m_Collider2D.size.y * m_Collider2D.transform.lossyScale.y * 0.4f;
                //length = IsOnGround && (m_RigidBody.velocity.y > length) ? m_RigidBody.velocity.y : length;
                return -length;
            }
        }
        bool IsJustOnGround
        {
            get
            {
                return m_IsJustOnGround;
            }
            set
            {
                if (m_IsJustOnGround == value)
                    return;
                m_IsJustOnGround = value;
                OnJustOnGroundChange(m_IsJustOnGround);
            }
        }

        //运动 ToDo
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
                else if (m_LastFallingTime <= 0 && value.y <= 0)
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
                return this.RealSpeed.x > 0 ? 1 : -1;
            }
        }
        //下坠时间比例
        float FallingTimeScale
        {
            get
            {
                //ToDo
                float time = Time.time - m_LastFallingTime;
                if (m_PauseTime > 0 && !m_IsPausing)
                {
                    m_LastFallingTime += m_PauseTime;
                }
                return time < m_PhysicData.FallingTime ? (time / m_PhysicData.FallingTime) : 1;
            }
        }
        #endregion
        #region 对外接口
        public float GravityScale
        {
            get
            {
                return m_IsPausing ? 0 : m_GravityScale;
            }
            set
            {
                m_GravityScale = value;
            }
        }
        //判断是否在地面
        public bool IsOnGround
        {
            get
            {
                return m_IsOnGround;
            }
            set
            {
                if (m_IsOnGround && !value)
                {
                    onLeaveGround();
                }
                if (m_IsOnGround != value)
                {
                    if (value == true)
                        onTouchGround();
                    else
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

        //左右朝向
        public FaceDirection HDirection
        {
            get
            {
                return this.transform.localScale.x > 0 ? FaceDirection.Right : FaceDirection.Left;
            }
            set
            {
                if (value == this.HDirection)
                    return;
                Vector3 scale = this.transform.localScale;
                switch (value)
                {
                    case FaceDirection.Right:
                        scale.x = 1;
                        break;
                    default:
                        scale.x = -1;
                        break;
                }
            }
        }
        #endregion
        #region 物理
        //判断是否在地上
        void SetGoroundInfo()
        {
            CheckOnGround();
        }

        void CheckOnGround()
        {
            if (!IsOnGround && m_RealSpeed.y > 0)
            {
                return;
            }
            Vector2 startPS = (Direction < 0 ? GroundRayLP : GroundRayRP);
            Vector2 endPS = (Direction < 0 ? GroundRayRP : GroundRayLP);
            bool onGround = false;
            for (int loopTime = 0; loopTime < g_RayNum; ++loopTime)
            {
                Vector2 rayPS = Vector2.Lerp(startPS, endPS, (float)loopTime / (g_RayNum - 1));
#if UNITY_EDITOR
                Debug.DrawRay(rayPS, Vector2.up * RayGraphicLength, new Color(0.9f, 0.5f, 0, 3f));
#endif
                RaycastHit2D hitPlane = Physics2D.Raycast(rayPS, Vector2.up, RayGraphicLength, GroundLayer);
                if (hitPlane.collider)
                {
                    if (m_FootCollider2D.isTrigger)
                    {
                        if (m_StepStone == hitPlane.collider)
                        {
                            continue;
                        }
                        else
                        {
                            m_FootCollider2D.isTrigger = false;
                        }
                    }
                    m_NormalTrans = Quaternion.FromToRotation(Vector2.up, hitPlane.normal);
                    m_StepStone = hitPlane.collider;
                    onGround = true;
                    break;
                }
            }
            IsOnGround = onGround;
            if (!IsOnGround)
            {
                m_NormalTrans = Quaternion.FromToRotation(Vector2.up, Vector2.up);
            }
#if UNITY_EDITOR
            Debug.DrawRay(this.transform.position, m_NormalTrans * (Vector2.up * 5), new Color(0.3f, 0.9f, 0, 1));
#endif
        }

        /// <summary>
        /// 让角色离开平台
        /// </summary>
        public void LeavePlatform()
        {
            if (m_StepStone == null)
            {
                return;
            }
            if (Math.Pow(2, m_StepStone.gameObject.layer) == m_PlatFormMask)
            {
                m_FootCollider2D.isTrigger = true;
            }
        }

        //衰减速度
        void ReduceSpeed()
        {
            m_RealSpeed *= (1 - m_PhysicData.ReduceRate);
        }

        //每帧计算一次下落速度
        float CountFallSpeed()
        {
            float fallSpeed = 0;
            if (RealSpeed.y <= 0)
            {
                float speed = m_PhysicData.FallingGravityCurve.Evaluate(FallingTimeScale) * m_GravityScale;
                speed = speed < 0.01f ? 0 : speed;
                fallSpeed = -speed * m_PhysicData.MaxFallingSpeed;
            }
            else
            {
                
                float minuSpeed = EvgGraphic * Time.deltaTime;
                fallSpeed = RealSpeed.y > minuSpeed ? RealSpeed.y - minuSpeed : 0;

            }
            float resalFallspeed = (IsOnGround && fallSpeed < (-EvgGraphic / 10)) ? -EvgGraphic / 10 : fallSpeed;
            return resalFallspeed;
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
            m_RigidBody.velocity = m_NormalTrans * RealSpeed;
        }

        #endregion
        #region 流程
        private void Awake()
        {
            Reset();
            m_RigidBody = GetComponent<Rigidbody2D>();
            m_GroundMask = 1 << LayerMask.NameToLayer("Ground");
            m_PlatFormMask = 1 << LayerMask.NameToLayer("PlatForm");
            m_EvgGraphic = m_PhysicData.MaxFallingSpeed / m_PhysicData.FallingTime;
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
            if (IsOnGround)
                ReduceSpeed();
        }

        void CountLastFrame()
        {
            if (m_IsJustOnGround)
            {
                if (m_JustOnGroundTime < Time.time)
                {
                    m_IsJustOnGround = false;
                }
            }
            SetGoroundInfo();
        }

        /// <summary>
        /// 每帧更新物理逻辑
        /// </summary>
        public void PhysicUpdate()
        {
            FramePrepare();
            CountSpeed();
            CountLastFrame();
            EndFrame();
        }

        public void EndFrame()
        {
            if (m_PauseTime > 0 && !m_IsPausing)
            {
                m_PauseTime = 0;
            }
        }
        #endregion
        #region 设置速度
        public void SetSpeed(Vector2 speed)
        {
            Debug.Log(speed);
            if (m_IsPausing)
                m_BackUpSpeed = speed;
            else
            {
                if (speed.y > 0)
                    onMoveUp();
                MoveSpeed = speed;
            }
        }

        public void SetSpeed(float speed)
        {
            Vector2 dir = Vector2.right * transform.localScale.x;
            if (m_IsPausing)
                m_BackUpSpeed = dir * speed;
            else
                MoveSpeed = dir * speed;
        }

        /// <summary>
        /// 暂停物理
        /// </summary>
        public void PausePhysic()
        {
            if (m_IsPausing)
                return;
            m_IsPausing = true;
            m_BackUpSpeed = RealSpeed;
            m_PauseTime = Time.time;
            m_RealSpeed = Vector2.zero;
        }

        /// <summary>
        /// 继续物理
        /// </summary>
        public void CountinuePhysic()
        {
            if (!m_IsPausing)
                return;
            m_IsPausing = false;
            m_MoveSpeed = m_BackUpSpeed;
            m_PauseTime = Time.time - m_PauseTime;
        }
        #endregion
        #region 事件
        /// <summary>
        /// 触底事件
        /// </summary>
        public event Action OnGrond;
        /// <summary>
        /// 离地事件
        /// </summary>
        public event Action OnLeaveGround;
        public event Action<bool> OnJustOnGroundChange;
        void onTouchGround()
        {
            m_IsJustOnGround = true;
            m_JustOnGroundTime = Time.time + 0.1f;
            if (OnGrond != null)
            {
                OnGrond();
            }
        }
        void onLeaveGround()
        {
            m_LastFallingTime = 0;
            if (OnLeaveGround != null)
            {
                OnLeaveGround();
            }
        }
        void onMoveUp()
        {
            m_LastFallingTime = 0;
        }
        #endregion
    }
}

