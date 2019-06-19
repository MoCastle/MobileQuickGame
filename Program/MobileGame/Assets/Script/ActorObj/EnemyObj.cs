using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using GameScene;

public class EnemyObj : NPCObj
{
    #region 编辑
    [SerializeField]
    [Header("巡逻范围")]
    public Vector2 guardSize;
    [Header("偏移量")]
    public Vector3 guardShiftValue;
    public Color guardDrawColor;

    void OnDrawGizmos()
    {
        Gizmos.color = guardDrawColor;//为随后绘制的gizmos设置颜色。
        Gizmos.DrawCube(transform.position + guardShiftValue, guardSize);
    }
    #endregion
    #region 内部变量
    [SerializeField]
    [Header("守卫区域")]
    BoxCollider2D _GuardBox;

    public BoxCollider2D GuardinArea
    {
        get
        {
            return _GuardBox;
        }

    }
    #endregion
    #region 临时功能
    public Slider _LifeSlider;
    public Slider LifeSlider
    {
        get
        {
            if (_LifeSlider == null)
            {
                _LifeSlider = transform.Find("WorldCanvas").Find("LifeSlide").GetComponent<Slider>();
            }
            return _LifeSlider;
        }
    }
    #endregion
    #region 属性

    //堆对象缓存 提快计算速度
    BoxCollider2D[] _ColliderList;
    public NPCActionControler actionControler
    {
        get
        {
            return m_ActionCtrler as NPCActionControler;
        }
    }
    #endregion

    #region 对外接口
    //寻找敌人
    public Collider2D[] FindEnemy( int Layer )
    {
        if(!GuardinArea.enabled)
        {
            return null;
        }
        if(_ColliderList == null)
        {
            _ColliderList = new BoxCollider2D[5];
        }
        ContactFilter2D _contactFilter = new ContactFilter2D();
        _contactFilter.SetLayerMask(Layer);
        GuardinArea.OverlapCollider(_contactFilter, _ColliderList);
        return _ColliderList;
    }
    #endregion
    protected override void Init()
    {
        m_Animator = transform.Find("Animator").GetComponent<Animator>();
        m_IDLayer = 1 << LayerMask.NameToLayer("Player");

        Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
        m_IDLayer = 1 << LayerMask.NameToLayer("Player");
        m_ActionCtrler = new NPCActionControler(this);
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        LifeSlider.value = m_ActorPropty.percentLife;
    }

    void ClearAnimParams()
    {
        AnimatorControllerParameter[] animParams = ActionCtrl.GetAnimParams;
        foreach(AnimatorControllerParameter param in animParams)
        {
            if (param.type == AnimatorControllerParameterType.Bool && param.name != "IsOnGround" && param.name != "IsJustOnGround")
            {
                ActionCtrl.SetBool(param.name,false);
            }
        }
    }

    #region 事件
    public override void BeBreak()
    {
    }
    #endregion
}
