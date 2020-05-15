using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_StateMoveToPoint : IState
{
    private GhostController owner;
    private Vector3 point;
    private bool inWall = true;

    public Ghost_StateMoveToPoint(GhostController owner, Vector3 point)
    {
        this.owner = owner;
        this.point = point;
    }

    public void stateInit()
    { 
        this.owner.hitbox.enabled = false;
    }

    public void stateUpdate()
    {
        Vector2 vec = new Vector2(point.x - owner.transform.position.x, point.y - owner.transform.position.y)*2;
        owner.movement = vec;
        if (vec.magnitude < .1 || this.inWall == false)
        {
            owner.BreakoutIdle();
        }
    }

    public void stateFixedUpdtate()
    {
        
    }

    public void stateExit()
    {
        this.owner.hitbox.enabled = true;
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
        this.inWall = false;
    }


}
