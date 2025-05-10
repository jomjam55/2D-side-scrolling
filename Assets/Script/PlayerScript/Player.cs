using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack detail")]
    public float[] attackMovement;


    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed ;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed ;
    public float dashDuration;
    public float dashDir { get; private set; }  


    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerWallSildeState wallSlide { get; private set; }
    public PlayerPrimaryAttack primaryAttack { get; private set; } 
    public PlayerDeadState deadState { get; private set; }


    #endregion

    protected override void Awake()
    {
        base.Awake();   

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSildeState(this, stateMachine, "WallSilde");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        deadState = new PlayerDeadState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
       
    }

   

    protected override void Update()
    {
        base.Update(); 

        stateMachine.currentState.Update();         
        CheckForDashInput();
        
    }

    public IEnumerable BusyFor(float _seconds)
    {
        isBusy = true;
        Debug.Log("busy");

        yield return new WaitForSeconds(_seconds);

        Debug.Log("free");
        isBusy= false;

    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if(IsWallDetected())
        {
            return;
        }

        dashUsageTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {
            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }


    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
