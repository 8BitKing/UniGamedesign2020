using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Ghost_StatePosses : IState
{

    private GhostController owner;
    private GameObject possesed;
    private Transform goalTransform;
    private Rigidbody2D goalRb;

    private Vector2 direction;
    private Vector3 goalPos;
    private bool spotOn;


    public Ghost_StatePosses(GhostController owner, GameObject possesed) {
        this.possesed = possesed;
        this.owner = owner;
        this.goalTransform = possesed.GetComponent<Transform>();
        this.goalRb = possesed.GetComponent<Rigidbody2D>();
    }

    public void stateInit()
    {
        this.owner.movement = new Vector2(0, 0);
        this.owner.hitbox.enabled = false;
        this.goalPos = goalTransform.position;
        this.goalRb.bodyType = RigidbodyType2D.Dynamic;
        this.spotOn = false;

    }
    public void stateUpdate()
    {
        this.direction.x = Input.GetAxisRaw("Horizontal");
        this.direction.y = Input.GetAxisRaw("Vertical");
        this.direction.Normalize();

        float dist = Vector3.Distance(this.owner.transform.position, this.goalPos);
        Vector2 vec1 = new Vector2(goalPos.x - owner.transform.position.x, goalPos.y - owner.transform.position.y);
        Vector2 vec2 = vec1;

        vec2 *= 6;
        vec2 += this.direction;

        owner.movement = vec2;

        this.goalPos = this.goalTransform.position;

        if (Input.GetButtonDown("Dash")) 
        {
            owner.BreakoutIdle();
        }
    }

    public void stateFixedUpdtate()
    {
        this.goalRb.MovePosition(this.goalRb.position + this.direction * .5f * Time.fixedDeltaTime);
    }

    public void stateExit()
    {
        this.owner.hitbox.enabled = true;
        this.goalRb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }
}
