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

    Vector2 movement;

    public Ghost_StateWalking(GhostController owner, Animator animator, Rigidbody2D rb, float mSpeed) {
        this.owner = owner;
        this.animator = animator;
        this.rigidbody = rb;
        this.movementSpeed = mSpeed;
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
        this.movement.x = Input.GetAxisRaw("Horizontal");
        this.movement.y = Input.GetAxisRaw("Vertical");
        this.movement.Normalize();

        this.animator.SetFloat("hSpeed", this.movement.x);
        this.animator.SetFloat("vSpeed", this.movement.y);
        //this.animator.SetFloat("speed", this.movement.magnitude);

        if (this.movement.magnitude > 0)
        {
            this.animator.SetFloat("hDir", this.movement.x);
            this.animator.SetFloat("vDir", this.movement.y);
        }
        else {
            owner.stateMachine.ChangeState(new Ghost_StateIdle(owner, owner.animator));
        }
    }

    public void stateFixedUpdtate()
    {
        this.rigidbody.MovePosition(this.rigidbody.position + this.movement * this.movementSpeed * Time.fixedDeltaTime);
    }

}
