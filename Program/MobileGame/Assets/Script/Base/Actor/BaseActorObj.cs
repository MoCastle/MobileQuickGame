using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActorObj : MonoBehaviour {
    #region 玩家角色信息提取接口 以后可能会挪到非对象脚本里

    public HitEffect BeHitEffect;

    #endregion
    //敌人识别层级
    protected int _IDLayer;
    public int IDLayer
    {
        get
        {
            return _IDLayer;
        }
    }

    //生存状态
    //生命状态
    protected bool _Alive = true;
    public bool Alive
    {
        get
        {
            return _Alive;
        }
    }

    //技能判定盒
    BoxCollider2D _SkillHurtBox;
    public BoxCollider2D SkillHurtBox
    {
        get
        {
            if (_SkillHurtBox == null)
            {
                _SkillHurtBox = TransCtrl.FindChild("SkillCheck").GetComponent<BoxCollider2D>();
            }
            return _SkillHurtBox;
        }
    }

    [SerializeField]
    [Title("人物属性", "black")]
    public Propty _ActorPropty;
    public Propty ActorPropty
    {
        get
        {
            return _ActorPropty;
        }
    }

    public bool _OnPlat;
    public bool OnPlat
    {
        get
        {
            return _OnPlat;
        }
    }

    BoxCollider2D _ColliderCtrl;
    public BoxCollider2D BodyCollider
    {
        get
        {
            if (_ColliderCtrl == null)
            {
                _ColliderCtrl = GetComponent<BoxCollider2D>();
            }
            return _ColliderCtrl;
        }
    }

    public Transform TransCtrl
    {
        get
        {
            return transform;
        }
    }

    BoxCollider2D _PlatFoot;
    BoxCollider2D PlatFoot
    {
        get
        {
            if (!_PlatFoot)
            {
                _PlatFoot = TransCtrl.FindChild("PlaneFoot").GetComponent<BoxCollider2D>();
            }
            return _PlatFoot;
        }
    }

    Transform _FootTransCtrl;
    public Transform FootTransCtrl
    {
        get
        {
            if (_FootTransCtrl == null)
            {
                _FootTransCtrl = TransCtrl.FindChild("FootCheck");
            }
            return _FootTransCtrl;
        }
    }

    public bool _IsOnGround = false;
    public bool IsOnGround
    {
        get
        {
            return _IsOnGround;
        }
        set
        {
            if (_IsOnGround != value)
            {
                _IsOnGround = value;
                _ActionCtrler.SetBool("IsOnGround", _IsOnGround);
                IsJustOnGround = value;
            }
        }
    }
    bool _IsJustOnGround;
    float _TimeJustOnGround;
    public bool IsJustOnGround
    {
        get
        {
            return _IsJustOnGround;
        }
        set
        {
            if (_IsJustOnGround != value)
            {
                _IsJustOnGround = value;
                _ActionCtrler.SetBool("IsJustOnGround", _IsJustOnGround);
                if (_IsJustOnGround)
                {
                    _TimeJustOnGround = Time.time;
                }
            }
        }
    }

    //重新可以踩在平台上
    public void ReOpenPlatFoot()
    {
        PlatFoot.isTrigger = false;

    }
    public void ClosePlatFoot()
    {
        PlatFoot.isTrigger = true;
    }

    ActionCtrler _ActionCtrler;
    PhysicCtrler _PhysicCtrl;
    public ActionCtrler ActionCtrl
    {
        get
        {
            return _ActionCtrler;
        }
    }
    public PhysicCtrler PhysicCtrl
    {
        get
        {
            return _PhysicCtrl;
        }
    }
	// Use this for initialization
	void Start () {
        
    }
    private void Awake()
    {
        ActorInfo info = ActorManager.Mgr.GetActorInfo(0);
        _ActionCtrler = new ActionCtrler(this, GetComponent<Animator>(), info.ActorActionList);
        _PhysicCtrl = new PhysicCtrler(this);
        LogicAwake();
    }
    protected abstract void LogicAwake();

    // Update is called once per frame
    public void Update()
    {
        _ActionCtrler.Update();
        /*
        if (GameCtrler.IsPaused)
        {
            return;
        }*/

        //着地状态检测
        float Width = BodyCollider.size.x;
        Vector2 Position = FootTransCtrl.position;
        Vector2 Size = Vector2.right * (0.4f - 0.01f);
        Size.y = 0.5f;
        LayerMask Layer = 1 << LayerMask.NameToLayer("Ground");
        if (!PlatFoot.isTrigger)
        {
            Layer += 1 << LayerMask.NameToLayer("PlatForm");
        }

        Collider2D Collider = Physics2D.OverlapBox(Position, Size, 0, Layer);
        if (Collider && FootTransCtrl.position.y > Collider.transform.position.y)
        {
            //判断角色是否有可以站在平台上的脚
            IsOnGround = true;

            //检查脚下的是不是平台
            if (Collider.gameObject.layer == 1 << LayerMask.NameToLayer("PlatForm"))
            {
                _OnPlat = true;
            }
        }
        else
        {
            IsOnGround = false;
            _OnPlat = false;
        }
        //触地检测
        if (IsJustOnGround && _TimeJustOnGround + 0.2 < Time.time)
        {
            IsJustOnGround = false;
        }
        /*
        
        */
        LogicUpdate();
        //ActorPropty.ModVIT(1);

    }

    #region 向外提供接口
    public Vector2 CurFaceDir
    {
        get
        {
            return Vector2.right * (transform.localScale.x >0?1:-1);
        }
    }
    //朝向 y等0时仅改变左右朝向
    public void FaceToDir(Vector2 dir)
    {
        if( dir.x * transform.localScale.x < 0 )
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        if(dir.y != 0)
        {
            dir = dir.normalized;
            float Rotate = 0;
            Rotate = Mathf.Atan2(dir.y, Mathf.Abs(dir.x)) * 180 / Mathf.PI;
            if (dir.x < 0)
            {
                Rotate = Rotate * -1;
            }
            Vector3 Rotation = Vector3.forward * Rotate;
            transform.eulerAngles = Rotation;
        }
        
    }
    #endregion

    #region
    public virtual void LogicUpdate()
    {

    }
    #endregion
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
    #region 动画事件
    public void DebugInfo()
    {
    }
    public void BeAttacked( BaseActorObj attacker,Vector3 position )
    {
        if(_ActorPropty._ActorInfo.BloodName!=null&& _ActorPropty._ActorInfo.BloodName!="")
        {
            GameObject blood = EffectManager.Manager.GenEffect(_ActorPropty._ActorInfo.BloodName);
            float dir = attacker.transform.position.x - this.transform.position.x;
            Vector3 scale = blood.transform.localScale;
            if(scale.x * dir < 0)
            {
                scale.x *= -1;
                blood.transform.localScale = scale;
            }
            blood.transform.position = position;
        }
        

    }
    public virtual void EnterState()
    { }
    //通知硬直事件
    public void HardTime( float hardTime )
    {
        _ActionCtrler.CurAction.HardTime(hardTime);
    }
    //设置运动速度
    public void SetSpeed(float speed)
    {
        _ActionCtrler.CurAction.SetSpeed(speed);
    }
    //设置瞬时垂直速度
    public void SetImdVSpeed(float speed)
    {
        _PhysicCtrl.SetSpeed(new Vector2(_PhysicCtrl.GetSpeed.x, speed));
    }
    //设置瞬时水平速度
    public void SetImdHSpeed(float speed)
    {
        _PhysicCtrl.SetSpeed(new Vector2(speed,_PhysicCtrl.GetSpeed.y ));
    }
    public void StopMove()
    {
        _ActionCtrler.CurAction.SetSpeed(0);
    }
    public void SetFaceLock( int ifLock)
    {
        _ActionCtrler.CurAction.SetFaceLock(ifLock == 1);
    }
    //瞬移
    public void MoveActor(float distance)
    {
        transform.position += FaceDir3D * distance;
        
    }
    //转换状态
    public virtual void SwitchAction()
    {

    }
    //召唤
    public virtual void CallPuppet( string objName )
    {
        BaseActorObj Obj = ActorManager.Mgr.GenActor(objName);
        PuppetNpc puppet = Obj as PuppetNpc;
        _ActionCtrler.CurAction.CallPuppet(puppet);
        
    }
    #endregion
}
