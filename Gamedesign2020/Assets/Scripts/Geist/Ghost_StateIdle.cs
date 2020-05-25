using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Ghost_StateIdle : IState
{
    private GhostController owner;
    private Animator animator;
    private Vector2 movement;
    private Vector2 direction;
   

    public Ghost_StateIdle(GhostController owner){
        this.owner = owner;
        this.animator = owner.animator;
        this.movement = owner.movement;
    }

    public void stateExit()
    {
    }

    public void stateFixedUpdtate()
    {
    }

    public void stateInit()
    {
        if (owner.good_form)
        {
            this.animator.Play("IdleState", -1, 0);
        }
        else
        {
            this.animator.Play("IdleStateEvil", -1, 0);
        }
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }

    public void stateUpdate()
    {
        this.direction.x = Input.GetAxisRaw("Horizontal");
        this.direction.y = Input.GetAxisRaw("Vertical");
        this.direction.Normalize();

        //Abbremsen
        this.movement.x *= .97f;
        this.movement.y *= .97f;

        //Übergebe MOvement Vector
        owner.movement = this.movement * owner.movementSpeed;

        if (this.direction.magnitude > 0) {
            owner.stateMachine.ChangeState(new Ghost_StateWalking(this.owner));
        }

        //Switch Form
        if (owner.good_form != owner.SwitchForm())
        { 
            if (owner.good_form)
            {
                this.animator.Play("IdleState", -1, 0);
            }
            else
            {
                this.animator.Play("IdleStateEvil", -1, 0);
            }
        }

    }
}
