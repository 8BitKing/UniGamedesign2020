using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateIdle : IState
{
    private EnemyController owner;
    private Animator animator;
    private Rigidbody2D rb;
    private GridDebug gridObject;
    private int visionRange;
    private float time;
    private float hdir=0;
    private float vdir=-1;
    private int direction = 0;
    private KindControllerRaycast target;

    public EnemyStateIdle(EnemyController owner)
    {

        this.owner = owner;
        
        this.animator = owner.animator;
        this.rb = owner.rb;
        

        
        this.gridObject = owner.gridObject;
        this.visionRange = owner.visionRange;

        this.target = owner.target;
    }
    public void stateInit()
    {
        MonoBehaviour.print("reachedIdle");
        owner.movement = new Vector2(0, 0);
        this.animator.Play("Idle", -1, 0);
        this.animator.SetFloat("hdir", hdir);
        this.animator.SetFloat("vdir", vdir);
        this.owner.rb.bodyType = RigidbodyType2D.Kinematic;
        time = Time.time;
    }
    
    public void stateExit()
    {
       
    }

    public void stateUpdate()
    {
        owner.movement = new Vector2(0, 0);
        if((target.gameObject.transform.position - owner.gameObject.transform.position).magnitude < visionRange * 0.32f&& (target.gameObject.transform.position - owner.gameObject.transform.position).magnitude>0.32f)
        {
            owner.stateMachine.ChangeState(new EnemyStateFollow(owner));
        }
        if (Time.time-time > 3)
        {
            time = Time.time;
            direction++;
            
            switch (direction%8)
            {
                
                case 0:
                    hdir = 0;
                    vdir = -1;
                    break;
                case 1:
                    hdir = -0.71f;
                    vdir = -0.71f;
                    break;
                case 2:
                    hdir = -1;
                    vdir = 0;
                    break;
                case 3:
                    hdir = -0.71f;
                    vdir = 0.71f;
                    break;
                case 4:
                    hdir = 0;
                    vdir = 1;
                    break;
                case 5:
                    hdir = 0.71f;
                    vdir = 0.71f;
                    break;
                case 6:
                    hdir = 1;
                    vdir = 0;
                    break;
                case 7:
                    hdir = 0.71f;
                    vdir = -0.71f;
                    break;

            }

            this.animator.SetFloat("hdir", hdir);
            this.animator.SetFloat("vdir", vdir);
        }
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
