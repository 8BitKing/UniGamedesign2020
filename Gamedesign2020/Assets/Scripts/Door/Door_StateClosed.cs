using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_StateClosed : IState
{
    private Animator animator;
    private DoorController owner;
    private Collider2D collision;
    public Door_StateClosed(DoorController owner)
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
        this.animator.Play("DoorClosed", -1, 0);
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
    }



}
