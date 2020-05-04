using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_StateIdle : IState
{
    private GhostController owner;
    private Animator animator;
    private Vector2 movement;

    public Ghost_StateIdle(GhostController owner, Animator animator){
        this.owner = owner;
        this.animator = animator;
    }

    public void stateInit()
    {
        this.animator.Play("IdleState", -1, 0);
    }

    public void stateExit()
    {
    }

    public void stateFixedUpdtate()
    {
    }

    public void stateUpdate()
    {
        this.movement.x = Input.GetAxisRaw("Horizontal");
        this.movement.y = Input.GetAxisRaw("Vertical");
        this.movement.Normalize();

        if (this.movement.magnitude > 0) {
            owner.stateMachine.ChangeState(new Ghost_StateWalking(this.owner, this.owner.animator, this.owner.rb, this.owner.movementSpeed));
        }
    }
}
