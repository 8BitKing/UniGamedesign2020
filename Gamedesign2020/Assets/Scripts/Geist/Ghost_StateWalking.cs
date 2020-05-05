using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost_StateWalking : IState
{
    private GhostController owner;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private float movementSpeed;
    private float acceleration;

    Vector2 movement;
    Vector2 direction;

    public Ghost_StateWalking(GhostController owner) {
        this.owner = owner;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.movementSpeed = owner.movementSpeed;
        this.acceleration = owner.acceleration;
    }

    public void stateInit()
    {
        this.animator.Play("WalkState", -1, 0);
    }


    public void stateExit()
    {

    }

    public void stateUpdate()
    {
        //Get Input Axes
        this.direction.x = Input.GetAxisRaw("Horizontal");
        this.direction.y = Input.GetAxisRaw("Vertical");

        this.movement += this.direction;

        //Controll Animator
        if (this.movement.magnitude > 0)
        {
            this.animator.SetFloat("hDir", this.direction.x);
            this.animator.SetFloat("vDir", this.direction.y);
        }

        //--Breakout

        //To Idle State
        if (this.movement.magnitude == 0)
        {
            owner.stateMachine.ChangeState(new Ghost_StateIdle(owner));
        }
    }

    public void stateFixedUpdtate()
    {
        //Move Ghost
        this.rigidbody.MovePosition(this.rigidbody.position + this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }

}
