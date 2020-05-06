using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEditorInternal;
using UnityEngine;

public class Ghost_StateDash : IState
{
    private Animator animator;
    private GhostController owner;
    private float startTime;

    Vector2 movement;

    public Ghost_StateDash(GhostController owner) 
    {
        this.movement = owner.movement;
        this.animator = owner.animator;
        this.owner = owner;
    }

    public void stateInit()
    {
        startTime = Time.time;
        this.animator.Play("WalkState", -1, 0);
    }


    public void stateUpdate()
    {
        this.movement.Normalize();
        this.owner.movement = this.movement * this.owner.dashSpeed;

        //--Breakout

        //To Idle State
        
        if (Time.time - startTime >= owner.dashTime)
        {
            owner.BreakoutIdle();
        }
    }

    public void stateFixedUpdtate()
    {
    }


    public void stateExit()
    {
        this.owner.lastDash = Time.time;
        this.movement.Normalize();
        this.owner.movement = this.movement;
        //MonoBehaviour.print(Time.time - startTime);
    }


}
