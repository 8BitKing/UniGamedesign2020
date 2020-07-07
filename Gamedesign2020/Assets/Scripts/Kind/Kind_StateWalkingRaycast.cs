using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using System;



public class Kind_StateWalkingRaycast : IState
{

    private KindControllerRaycast owner;
    private Animator animator;
    private Rigidbody2D rigidbody;

    private float speed;
    private Vector2 movement;
    private Vector3 goal;
    private Vector2 direction;
    private Vector2 goal2D;
    private Transform transform;
    private GridDebug gridObject;
    private int visionRange;
    

    public Kind_StateWalkingRaycast(KindControllerRaycast owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.speed = owner.speed;
        this.movement = owner.movement;
        this.transform = owner.transform;
        this.gridObject = owner.gridObject;
        this.visionRange = owner.visionRange;


    }
    public void stateInit()
    {
        this.animator.Play("Walk Animations", -1, 0);
        this.owner.rb.bodyType = RigidbodyType2D.Dynamic;

    }


    public void stateExit()
    {
        
    }


    public void stateUpdate()
    {
        //MonoBehaviour.print("reached");
        //MonoBehaviour.print("left");
        goal2D = new Vector2(goal.x, goal.y);
        float minspeed = 0.8f;
        float rundeDuHure = 0.001f;
        Vector3 centerBoundingBox = owner.gameObject.GetComponent<BoxCollider2D>().bounds.center;
        direction = new Vector2(goal.x, goal.y) - new Vector2(centerBoundingBox.x, centerBoundingBox.y);
        direction.Normalize();
        
        this.movement = this.direction * Time.deltaTime * 10;

        if (Mathf.Abs(movement.x) < rundeDuHure)
        {

            movement.x = 0;
        }
        if (Mathf.Abs(movement.y) < rundeDuHure)
        {

            movement.y = 0;
        }
        if (Mathf.Abs(movement.x) < minspeed)
        {

            movement.x = minspeed * Math.Sign(movement.x);
        }
        if (Mathf.Abs(movement.y) < minspeed)
        {

            movement.y = minspeed * Math.Sign(movement.y);
        }
        if ((goal2D - new Vector2(centerBoundingBox.x, centerBoundingBox.y)).magnitude < 0.13)
        {

            
            //movement = goal2D - new Vector2(centerBoundingBox.x, centerBoundingBox.y);
            //owner.movement = this.movement;


            if (goal.z > 1) {
                owner.stateMachine.ChangeState(new KindStateIdle(this.owner));
            }
            else
            {
                owner.stateMachine.ChangeState(new KindStateCry(this.owner));
            }
            
        }


        if (this.movement.magnitude > 1)
        {
            this.movement.Normalize();
        }
        
        animator.SetFloat("hSpeed", -direction.x);
        animator.SetFloat("vSpeed", direction.y);
        animator.SetFloat("speed", direction.magnitude);

        owner.movement = this.movement;
        goal = gridObject.GetGoal(centerBoundingBox, this.visionRange,owner.gameObject) ;
    }


    public void stateFixedUpdtate()
    {
        
    }

    public void stateOnTriggerEnter(Collider2D collision)
    {
    }

    public void stateOnTriggerExit(Collider2D collision)
    {
    }
}
