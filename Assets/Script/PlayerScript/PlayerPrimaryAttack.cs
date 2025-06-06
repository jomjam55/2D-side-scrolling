using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;
        
        if(comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attackDir = player.facingDir;

        if(xInput != 0)
        {
            attackDir = xInput;
        }

        player.SetVelocity(player.attackMovement[comboCounter] * player.facingDir, rb.velocity.y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.speed = 1;
        player.StartCoroutine("BusyFor", .15f);
         
        comboCounter++;
        lastTimeAttacked = Time.time;
       
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0 )
        {
            player.SetZeroVelocity();
        }

        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }


}
