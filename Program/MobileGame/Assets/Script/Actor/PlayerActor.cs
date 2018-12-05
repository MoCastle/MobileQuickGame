using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerActEnum
{
    Hand_Drag,
    Hand_Click,
    Hand_Slip,
    Hand_Holding,
    Dir_Left,
    Dir_Right,
    Dir_Up,
    InputPercent
}

public class PlayerActor : BaseActor {
    
    Rigidbody2D _RigidBody2D;
    public Vector2 TheSpeed
    {
        get
        {
            if(_RigidBody2D == null)
            {
                _RigidBody2D = GetComponent<Rigidbody2D>();
            }
            return _RigidBody2D.velocity;
        }
    }
    public SkillEnum PreState;
    public Vector2 PreInput;

    public override bool IsOnGround
    {
        get
        {
            return base.IsOnGround;
        }

        set
        {
            if( IsOnGround != value )
            {
                AnimCtrl.SetInteger( "Dashed" ,0);
            }
            base.IsOnGround = value;
        }
    }

    public float MoveVector;
    public float ChargeAddSpeed;
    int _Dashed;
    public int Dashed
    {
        get
        {
            return _Dashed;
        }
        set
        {
            if( _Dashed != value )
            {
                _Dashed = value;
                AnimCtrl.SetInteger("Dashed", _Dashed);
            }
        }
    }
    NormInput _CurInput;
    Dictionary<HandGesture, NormInput> LegalnputDict = new Dictionary<HandGesture, NormInput>( );
    NormInput TempInput;
    public NormInput GetTempInput( HandGesture Key )
    {
        NormInput Result = new NormInput();
        if( LegalnputDict.TryGetValue( Key, out Result ))
        {
            LegalnputDict[Key] = new NormInput();
        }
        return Result;
    }
    public void RemoveInput( HandGesture Key )
    {
        LegalnputDict.Remove(Key);
    }
    public NormInput CurInput
    {
        get
        {
            return _CurInput;
        }set
        {
            _CurInput = value;
            if( _CurInput.IsLegal )
            {
                if( LegalnputDict.ContainsKey( _CurInput.Gesture ) )
                {
                    LegalnputDict[_CurInput.Gesture] = _CurInput;
                }
                else
                {
                    LegalnputDict.Add(_CurInput.Gesture, _CurInput);
                }
            }
        }
    }
    // Use this for initialization
    public override void LogicAwake(){
        PlayerCtrl.AddFingerOff (OnFingerOut);
        
    }
	
    public void FireOff()
    {
        if( ActorState.SkillType == SkillEnum.RocketCut )
        {
            RocketCutState State = (RocketCutState)ActorState;
            State.FireOff();
        }
    }
    public void MoveForward()
    {
        Vector3 OldPst = TransCtrl.position;
        OldPst.x = OldPst.x + MoveVector;
        TransCtrl.position = OldPst;
    }
    public override void SwitchState()
    {
        base.SwitchState();
        ClearAnimParam();
    }
    public void ClearAnimParam()
    {
        this.AnimCtrl.SetBool("Hand_Drag", false);
        this.AnimCtrl.SetBool("Hand_Click", false);
        this.AnimCtrl.SetBool("Hand_Slip", false);
        this.AnimCtrl.SetBool("Hand_Holding", false);
        this.AnimCtrl.SetBool("Dir_Left", false);
        this.AnimCtrl.SetBool("Dir_Right", false);
        this.AnimCtrl.SetBool("Dir_Down", false);
        this.AnimCtrl.SetBool("Dir_Up", false);
        this.AnimCtrl.SetFloat("InputPercent", 0f);
        this.AnimCtrl.SetFloat("XInputPercent", 0f);
        this.AnimCtrl.SetFloat("YInputPercent", 0f);
    }
    public void SetAnimParam(NormInput HandInput)
    {
        ClearAnimParam();
        this.AnimCtrl.SetFloat("XInputPercent", HandInput.InputInfo.XPercent);
        this.AnimCtrl.SetFloat("XInputPercent", HandInput.InputInfo.YPercent);
        if (HandInput.Dir != InputDir.Middle)
        {
            int InputNum = (int)HandInput.Dir;
            //将逻辑枚举拆分成
            switch (HandInput.GetHoriEnum())
            {
                case InputDir.Left:
                    this.AnimCtrl.SetBool("Dir_Left", true);
                    break;
                case InputDir.Right:
                    this.AnimCtrl.SetBool("Dir_Right", true);
                    break;
                default:
                    this.AnimCtrl.SetBool("Dir_Right", false);
                    this.AnimCtrl.SetBool("Dir_Left", false);
                    break;
            }
            switch (HandInput.GetVertEnum())
            {
                case InputDir.Up:
                    this.AnimCtrl.SetBool("Dir_Up", true);
                    break;
                case InputDir.Down:
                    this.AnimCtrl.SetBool("Dir_Down", true);
                    break;
                default:
                    this.AnimCtrl.SetBool("Dir_Up", false);
                    this.AnimCtrl.SetBool("Dir_Down", false);
                    break;
            }
        }
        switch (HandInput.Gesture)
        {
            case HandGesture.Click:
                this.AnimCtrl.SetBool("Hand_Click", true);
                break;
            case HandGesture.Drag:
                this.AnimCtrl.SetBool("Hand_Drag", true);
                break;
            case HandGesture.Slip:
                this.AnimCtrl.SetBool("Hand_Slip", true);
                break;
            case HandGesture.Holding:
                this.AnimCtrl.SetBool("Hand_Holding", true);
                break;
            default:
                this.AnimCtrl.SetBool("Hand_Drag", false);
                this.AnimCtrl.SetBool("Hand_Click", false);
                this.AnimCtrl.SetBool("Hand_Slip", false);
                this.AnimCtrl.SetBool("Hand_Holding", false);
                break;
        }
    }
    public virtual void Input(NormInput Input)
    {
        
        if(Input.Gesture == HandGesture.Click && Input.Gesture == HandGesture.Click)
        {
            Vector3 scale = transform.localScale;

            Vector2 faceDir = Vector2.right;
            Vector2 dir = CurInput.InputInfo.EndPs - Input.InputInfo.EndPs;

            if (dir.x * scale .x < 0)
            {
                scale.x *= -1;
                //PhysicCtrl
            }

        }
        CurInput = Input;
        
        SetAnimParam(Input);
        PlayerCtrl.CurOrder = Input;
    }
    
    public override void LogicUpdate()
    {
        if( Alive )
        {
            NormInput HandInput = PlayerCtrl.InputRoundArr.Pop();
            if(HandInput.IsLegal)
            {
                Input(HandInput);
            }
            
        }
    }

    //手指挪开触发事件
    public void OnFingerOut()
    {
        //不再处于跑动状态
        //ActionCtrl.SetBool("IsRunning",false);
    }

    //销毁事件
    public void OnDestroy()
    {
        PlayerCtrl.RemoveFingerOff(OnFingerOut);
    }
}
