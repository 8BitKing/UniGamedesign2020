using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_StateCloseing : IState
{
    private Animator animator;
    private DoorController owner;
    private Collider2D collision;
    public Door_StateCloseing(DoorController owner)
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
        this.animator.Play("DoorClosing", -1, 0);
        this.collision.isTrigger = false;
        this.collision.enabled = true;
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }

    public void stateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f){
            owner.stateMachine.ChangeState(new Door_StateClosed(owner));
        }
    }



}
