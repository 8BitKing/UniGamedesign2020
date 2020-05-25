using System;
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

    private Vector2 oldPos;
    private Vector2 movement;

    private bool inWall = false;

    public Ghost_StateDash(GhostController owner) 
    {
        this.movement = owner.movement;
        this.animator = owner.animator;
        this.owner = owner;
    }

    public void stateInit()
    {
        this.oldPos = owner.transform.position;
        this.startTime = Time.time;
        this.animator.Play("WalkState", -1, 0);
        this.owner.hitbox.enabled = false;
    }


    public void stateUpdate()
    {
        this.movement.Normalize();
        this.owner.movement = this.movement * this.owner.dashSpeed;

        //--Breakout

        //To Idle State
        
        if (Time.time - startTime >= owner.dashTime)
        {
            if (inWall == false) {
                owner.BreakoutIdle();
            }
            else
            {
                owner.stateMachine.ChangeState(new Ghost_StateMoveToPoint(owner, oldPos));
            }
            
        }
    }

    public void stateExit()
    {
        this.owner.lastDash = Time.time;
        this.movement.Normalize();
        this.owner.movement = this.movement;
        this.owner.hitbox.enabled = true;
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.tag == "wallCollision") { 
            this.inWall = true;
            MonoBehaviour.print("in Wall");
        }
        if (collision.gameObject.tag == "MOVEABLE")
        {
            owner.stateMachine.ChangeState(new Ghost_StatePosses(owner, collision.gameObject));
        }
    }


    public void stateOnTriggerExit(Collider2D collision)
    {
        if (collision.gameObject.tag == "wallCollision")
        {
            this.inWall = false;
            MonoBehaviour.print("false");
        }
    }

    public void stateFixedUpdtate()
    {
    }

}
