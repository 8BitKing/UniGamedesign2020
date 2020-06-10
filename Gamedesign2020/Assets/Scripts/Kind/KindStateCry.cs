using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KindStateCry : IState
{
    private KindControllerRaycast owner;
    private Animator animator;
    private Vector2 movement;
    private Vector2 direction;
    private GridDebug gridObject;
    private Vector3 goal;
    public KindStateCry(KindControllerRaycast owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.movement = owner.movement;
        this.gridObject = owner.gridObject;


    }
    public void stateExit()
    {

    }

    public void stateFixedUpdtate()
    {
        owner.movement = this.movement;
    }

    public void stateInit()
    {
        this.animator.Play("CryState", -1, 0);
        

    }

    public void stateOnTriggerEnter(Collider2D collision)
    {

    }

    public void stateOnTriggerExit(Collider2D collision)
    {

    }

    public void stateUpdate()
    {

        movement = new Vector2(0, 0);

        Vector3 centerBoundingBox = owner.gameObject.GetComponent<BoxCollider2D>().bounds.center;
        goal = gridObject.GetGoal(centerBoundingBox, 5);


        direction = new Vector2(goal.x, goal.y) - new Vector2(centerBoundingBox.x, centerBoundingBox.y);

        if (direction.magnitude > 0.3)
        {

            owner.stateMachine.ChangeState(new Kind_StateWalkingRaycast(this.owner));
        }
        if(goal.z>1) owner.stateMachine.ChangeState(new KindStateIdle(this.owner));

    }



}
