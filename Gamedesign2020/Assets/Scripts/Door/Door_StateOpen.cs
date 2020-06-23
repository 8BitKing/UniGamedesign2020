using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_StateOpen : IState
{
    private Animator animator;
    private DoorController owner;
    private Collider2D collision;

    public Door_StateOpen(DoorController owner)
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
        this.animator.Play("DoorOpen", -1, 0);
        this.collision.isTrigger = true;
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }

    public void stateUpdate()
    {
    }

}
