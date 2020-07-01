using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_StateOpening : IState
{
    private Animator animator;
    private DoorController owner;
    private Collider2D collision;

    public Door_StateOpening(DoorController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.collision = owner.collision;
    }

    public void stateExit()
    {
    }

    public void stateFixedUpdtate()
    {
    }

    public void stateInit()
    {
        this.animator.Play("DoorOpening", -1, 0);
        this.collision.isTrigger = true;
        this.collision.enabled = false;
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }

    public void stateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            owner.stateMachine.ChangeState(new Door_StateOpen(owner));
        }
    }

}
