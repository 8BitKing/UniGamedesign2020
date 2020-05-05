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

    private Vector2 movement;
    private Vector2 direction;

    public Ghost_StateWalking(GhostController owner) {
        this.owner = owner;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.movementSpeed = owner.movementSpeed;
        this.acceleration = owner.acceleration;
        this.movement = owner.movement;
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
        this.direction.Normalize();

        //acceleration logik
        this.movement += this.direction * this.acceleration * 50 * Time.deltaTime;
        if (this.movement.magnitude > 1) {
            this.movement.Normalize();
        }

        //Abbremsen für extra mehr crisp movement
        if (this.direction.x == 0) 
        {
            this.movement.x *= .97f;
        }
        if (this.direction.y == 0)
        {
            this.movement.y *= .97f;
        }

        //Controll Animator
        if (this.movement.magnitude > 0)
        {
            this.animator.SetFloat("hDir", this.movement.x);
            this.animator.SetFloat("vDir", this.movement.y);
        }

        //Übergebe Movement Vector
        owner.movement = this.movement;

        //--Breakout

        //To Idle State
        if (this.direction.magnitude == 0)
        {
            owner.stateMachine.ChangeState(new Ghost_StateIdle(owner));
        }
    }

    public void stateFixedUpdtate()
    {
       
    }

}
