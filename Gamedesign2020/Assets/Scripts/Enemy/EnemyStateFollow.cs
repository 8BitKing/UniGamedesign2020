using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStateFollow : IState
{
    private KindControllerRaycast target;
    private EnemyController owner;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private float speed;
    private GridDebug gridObject;
    private int visionRange;
    private Vector2 goal2D;
    private Vector2 direction;
    private Vector2 movement;
    private Vector2 lastGoal;
    private bool lastGoalActive = false;
    private bool lastGoalInUse = false;
    private bool findBack;

    public EnemyStateFollow(EnemyController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.speed = owner.speed;
        this.gridObject = owner.gridObject;
        this.visionRange = owner.visionRange;
        this.target = owner.target;
        this.movement = owner.movement;
        this.findBack = owner.findBack;
    }
    public void stateInit()
    {
        
        this.animator.Play("WalkAnimations", -1, 0);
        this.owner.rb.bodyType = RigidbodyType2D.Dynamic;
        

    }
    public void stateExit()
    {
        
    }

    public void stateUpdate()
    {
        //MonoBehaviour.print(lastGoalInUse);
        if (!lastGoalInUse)
        {
            goal2D = gridObject.getFollowTarget(visionRange, target.gameObject.transform.position, owner.gameObject.transform.position, owner.gameObject);
        }
        if ((goal2D - (Vector2)owner.gameObject.transform.position).magnitude < 0.32f&&lastGoalInUse==false) owner.stateMachine.ChangeState(new EnemyStateIdle(owner));
        if (goal2D !=new Vector2(-999,-999))
        {
           
            lastGoalActive = true;
            lastGoal = goal2D;
        }
        if (goal2D==new Vector2(-999,-999)&&lastGoalActive==false)
        {
            
            if (owner.findBack)owner.stateMachine.ChangeState(new EnemyStateFindBack(owner));
            else owner.stateMachine.ChangeState(new EnemyStateIdle(owner));

        }
        if (((goal2D==new Vector2(-999,-999)) && lastGoalActive == true)||lastGoalInUse)
        {
            
            if ((lastGoal - (Vector2)owner.gameObject.transform.position).magnitude > 0.07f)
            {

                
                goal2D = lastGoal;
                lastGoalInUse = true;
                
                
            }
            else
            {
                lastGoalActive = false;
                lastGoalInUse = false;
            }
        }


        direction = goal2D - (Vector2)owner.gameObject.transform.position;
        direction.Normalize();

        this.movement = this.direction * Time.deltaTime * 10;

        float minspeed = 0.8f;
        float rundeDuHure = 0.001f;
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




        if (this.movement.magnitude > 1)
        {
            this.movement.Normalize();
        }


        animator.SetFloat("hdir", direction.x);
        animator.SetFloat("vdir", direction.y);
        owner.movement = movement;



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
