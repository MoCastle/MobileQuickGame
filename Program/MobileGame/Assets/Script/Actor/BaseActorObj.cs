using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
namespace GameScene
{
    public enum FaceDirection
    {
        Left,
        Right
    }
    public abstract class BaseActorObj : MonoBehaviour
    {
        #region 内部变量
        private LinkedList<BaseBuff> m_BuffList;
        //生存状态
        //生命状态
        protected bool _Alive = true;
        [SerializeField]
        [Title("人物属性", "black")]
        public Propty m_ActorPropty;
        Transform m_FootTransCtrl;
        PhysicComponent m_Physic;
        [SerializeField]
        [Header("动画事件")]
        protected CharacterAnim m_CharacterAnim;
        //技能判定盒
        [SerializeField]
        [Header("技能判定")]
        BoxCollider2D m_SkillHurtBox;
        private FaceDirection m_HDirection;
        //敌人识别层级
        protected int m_IDLayer;
        BoxCollider2D m_ColliderCtrl;
        protected ActionCtrler m_ActionCtrler;
        #endregion
        #region 对外接口
        public bool Alive
        {
            get
            {
                return _Alive;
            }
        }
        //物理
        public PhysicComponent Physic
        {
            get
            {
                return m_Physic;
            }
        }
        public bool IsOnGround
        {
            get
            {
                return m_Physic.IsOnGround;
            }
        }
        //角色
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
        public Vector2 CurFaceDir
        {
            get
            {
                Vector2 faceDir = this.m_CharacterAnim.transform.rotation * Vector2.up;
                faceDir.x *= HDirection == FaceDirection.Right ? 1 : -1;
                return faceDir;
            }
        }
        public BoxCollider2D SkillHurtBox
        {
            get
            {
                return m_SkillHurtBox;
            }
        }
        #endregion
        #region 属性
        public CharacterAnim Anim
        {
            get
            {
                return m_CharacterAnim;
            }
        }
        public int IDLayer
        {
            get
            {
                return m_IDLayer;
            }
        }
        public Propty propty
        {
            get
            {
                if (m_ActorPropty.name == "")
                {
                    m_ActorPropty.name = gameObject.name;
                }
                return m_ActorPropty;
            }
            set
            {
                m_ActorPropty = value;
            }
        }
        public BoxCollider2D BodyCollider
        {
            get
            {
                if (m_ColliderCtrl == null)
                {
                    m_ColliderCtrl = GetComponent<BoxCollider2D>();
                }
                return m_ColliderCtrl;
            }
        }
        public Transform TransCtrl
        {
            get
            {
                return transform;
            }
        }
        public Transform FootTransCtrl
        {
            get
            {
                if (m_FootTransCtrl == null)
                {
                    m_FootTransCtrl = TransCtrl.Find("FootCheck");
                }
                return m_FootTransCtrl;
            }
        }
        public ActionCtrler ActionCtrl
        {
            get
            {
                return m_ActionCtrler;
            }
        }
        //角色面向防线
        public Vector2 FaceDir
        {
            get
            {
                Vector2 dir = Vector2.right * (transform.localScale.x > 0 ? 1 : -1);
                return dir;
            }
        }
        public Vector3 FaceDir3D
        {
            get
            {
                Vector3 dir = Vector3.right * (transform.localScale.x > 0 ? 1 : -1);
                return dir;
            }
        }
        public bool endGameComplete
        {
            get
            {
                return m_ActionCtrler.gameGoing == false && m_ActionCtrler.JudgIsTag("init");
            }
        }
        #endregion
        #region 角色控制
        //左右移动
        /// <summary>
        /// 向一方向移动
        /// </summary>
        /// <param name="speed"> 速度 </param>
        /// <param name="faceToMoveDir"> 是否面向移动方向 </param>
        public void Move(Vector2 speed, Boolean faceToMoveDir = false)
        {
            m_Physic.MoveSpeed = speed;
            if (faceToMoveDir)
            {
                FaceToDir(speed);
            }
        }
        //朝向 y等0时仅改变左右朝向
        public void FaceToDir(Vector2 dir)
        {
            Vector3 scale = transform.localScale;

            if (dir.x * transform.localScale.x < 0)
            {
                scale.x *= -1;
                this.HDirection = dir.x > 0 ? FaceDirection.Right : FaceDirection.Left;
                transform.localScale = scale;
            }
            Vector2 faceDir = dir.normalized;
            //faceDir.x = Math.Abs(faceDir.x);
            Quaternion rotation = Quaternion.FromToRotation(FaceDir, faceDir);
            this.m_CharacterAnim.transform.rotation = rotation;

        }
        #endregion
        #region 流程
        public virtual void EnterGame()
        {
            m_ActionCtrler.gameGoing = true;
        }
        public virtual void Pause()
        {
            m_ActionCtrler.gameGoing = false;
        }
        public virtual void ContinueGame()
        {
            m_ActionCtrler.gameGoing = true;
        }
        public virtual void StopGame()
        {
            m_ActionCtrler.gameGoing = false;
        }
        private void Awake()
        {
            m_BuffList = new LinkedList<BaseBuff>();
            Init();
        }
        protected abstract void Init();
        private void Start()
        {
            m_Physic = GetComponent<PhysicComponent>();
            RegistPhysicEvent();
            RegistAnimEvent();
        }

        //出生
        public void Birth()
        {
            propty.ResetPropty();
            m_ActionCtrler.SetTriiger("Birth");
        }

        // Update is called once per frame
        public void Update()
        {
            m_Physic.PhysicUpdate();

            m_ActionCtrler.Update();
            LogicUpdate();
            //ActorPropty.ModVIT(1);
        }
        public virtual void LogicUpdate()
        {

        }
        #endregion
        #region 静态方法
        public static BaseActorObj GetActorByColliderTransfor(Transform transform)
        {
            return GetActorByAnimatorTransform(transform.parent);
        }
        public static BaseActorObj GetActorByAnimatorTransform(Transform transform)
        {
            return transform.parent.GetComponent<BaseActorObj>();
        }
        #endregion
        #region 玩家角色信息提取接口 以后可能会挪到非对象脚本里

        public HitEffect BeHitEffect;

        #endregion
        #region 物理
        private void RegistPhysicEvent()
        {
            m_Physic.OnGrond += OnGround;
            m_Physic.OnLeaveGround += OnLeaveGround;
            m_Physic.OnJustOnGroundChange += OnJustOnGroundChange;
        }
        #endregion
        #region 动画
        protected virtual void RegistAnimEvent()
        {
            CharacterAnim character = m_CharacterAnim;
            character.OnEnterHardTime += this.HardTime;
            character.OnSetFaceLock += this.SetFaceLock;
            character.OnMoveActor += this.MoveActor;
            character.OnCallPuppet += this.CallPuppet;

            character.OnSetSpeed += this.SetSpeed;
            character.OnStopMove += this.StopMove;
            character.OnSetDirMoveSpeed += this.SetMoveSpeed;

            character.OnSetImdVSpeed += this.SetImdVSpeed;
            character.OnSetImdHSpeed += this.SetImdHSpeed;
            character.OnSetImdVSpeed += this.SetImdVSpeed;
            character.OnSetImdMoveSpeed += this.SetImdSpeed;
            character.OnSetInAirImdVSpeed += this.SetInAirImdVSpeed;
        }
        #endregion
        #region BUFF
        public void AddBuff( BaseBuff buff )
        {
            m_BuffList.AddFirst(buff);
            buff.Start();
        }
        public BaseBuff GetBuffByType(BuffType buffType)
        {
            foreach( BaseBuff buff in m_BuffList )
            {
                if(buff.type == buffType)
                {
                    return buff;
                }
            }
            return null;
        }
        public void RemoveBuff( BaseBuff buff)
        {
            m_BuffList.Remove(buff);
        }
        #endregion
        // Use this for initialization
        #region 向外提供接口


        #endregion 受击相关
        //扣血相关
        void Hurt(float Damage)
        {
            m_ActorPropty.ModLifeValue(Damage * -1);
        }
        


        #region 事件
        public void OnGround()
        {
            m_CharacterAnim.SetBool("IsOnGround", true);
        }
        public void OnLeaveGround()
        {
            m_CharacterAnim.SetBool("IsOnGround", false);
        }
        public void OnJustOnGroundChange(bool justOnGround)
        {
            m_CharacterAnim.SetBool("IsJustOnGround", justOnGround);
        }
        #endregion
        #region 动画事件
        public void DebugInfo()
        {
        }
        public bool IsHoly = false;
        public virtual void BeAttacked(BaseActorObj attacker, Vector3 position, HitEffect hitEffect, float Damage = 0)
        {
            if (IsHoly)
            {
                return;
            }
            /*
            //受击特效
            if (_ActorPropty. != null && _ActorPropty.ActorInfo.BloodName != "")
            {
                GameObject blood = EffectManager.Manager.GenEffect(_ActorPropty.ActorInfo.BloodName);
                float dir = attacker.transform.position.x - this.transform.position.x;
                Vector3 scale = blood.transform.localScale;
                if (scale.x * dir < 0)
                {
                    scale.x *= -1;
                    blood.transform.localScale = scale;
                }
                blood.transform.position = position;
            }*/
            Vector2 faceDir = attacker.transform.position - transform.position;
            faceDir.y = 0;
            FaceToDir(faceDir);
            Hurt(Damage);
            if (m_ActorPropty.IsDeath)
            {
                m_ActionCtrler.SetTriiger("Death");
            }
            //受击效果
            BeHitEffect = hitEffect;
            //动画状态处理
            switch (hitEffect.HitType)
            {
                case HitEffectType.HitBack:
                    m_ActionCtrler.SetTriiger("HitBack");

                    break;
                case HitEffectType.ClickFly:
                    m_ActionCtrler.SetTriiger("ClickFly");

                    break;
            }

        }
        public virtual void EnterState()
        {
        }

        //通知硬直事件
        public void HardTime(float hardTime)
        {
            m_ActionCtrler.CurAction.SetHardTime(hardTime);
        }

        //设置运动速度 匀速运动
        public void SetSpeed(float speed)
        {
            m_ActionCtrler.CurAction.SetSpeed(speed);
        }

        //设置瞬时速度
        public void SetImdSpeed(Vector2 speed)
        {
            speed.x *= FaceDir.x;
            m_Physic.SetSpeed(speed);
        }

        //设置移动速度
        public void SetMoveSpeed(Vector2 moveSpeed)
        {
            m_ActionCtrler.CurAction.SetMoveSpeed(moveSpeed);
        }
        //瞬时速度 一定要加在设置移动速度前 不然会被覆盖掉
        //设置瞬时垂直速度
        public void SetImdVSpeed(float speed)
        {
            m_Physic.SetSpeed(new Vector2(m_Physic.MoveSpeed.x, speed));
        }

        //设置瞬时水平速度
        public void SetImdHSpeed(float speed)
        {
            speed *= FaceDir.x;
            m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
        }

        //设置空中瞬时垂直速度
        public void SetInAirImdVSpeed(float speed)
        {
            if (this.IsOnGround)
            {
                return;
            }
            m_Physic.SetSpeed(new Vector2(m_Physic.MoveSpeed.x, speed));
        }
        //设置空中瞬时水平速度
        public void SetInAirImdHSpeed(float speed)
        {
            if (this.IsOnGround)
            {
                return;
            }
            speed *= FaceDir.x;
            m_Physic.SetSpeed(new Vector2(speed, m_Physic.MoveSpeed.y));
        }

        public void StopMove(float speed)
        {
            if (-0.01f < speed && speed < 0.01f)
                m_ActionCtrler.CurAction.SetSpeed(0);
            else
                m_ActionCtrler.CurAction.SetFinalSpeed(speed);

        }
        public void SetFaceLock(int ifLock)
        {
            m_ActionCtrler.CurAction.SetFaceLock(ifLock == 1);
        }
        //瞬移
        public void MoveActor(float distance)
        {
            transform.position += FaceDir3D * distance;

        }
        //转换状态
        public virtual void SwitchAction(BaseAction action)
        {
            m_ActionCtrler.SwitchAction(action);
        }
        //召唤
        public virtual void CallPuppet(string objName)
        {
            BaseActorObj Obj = ActorManager.Mgr.GenActor(objName);
            PuppetNpc puppet = Obj as PuppetNpc;
            m_ActionCtrler.CurAction.CallPuppet(puppet);

        }
        //被打断
        public virtual void BeBreak()
        {

        }
        //离开完成 执行离开事件
        public virtual void LeaveComplete()
        {

        }
        public void Leave()
        {

        }
        #endregion
    }

}

