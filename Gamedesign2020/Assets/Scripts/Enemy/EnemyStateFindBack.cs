using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateFindBack : IState
{
    private EnemyController owner;
    private Animator animator;
    private Vector2 movement;
    private KindControllerRaycast target;
    private int visionRange;
    public EnemyStateFindBack(EnemyController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.movement = owner.movement;
        this.target = owner.target;
        this.visionRange = owner.visionRange;
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
        //if (((Vector2)target.gameObject.transform.position - (Vector2)owner.gameObject.transform.position).magnitude < visionRange) { 
        //    owner.stateMachine.ChangeState(new EnemyStateFollow(owner)); 
        //}
        movement = owner.traceback.Last() - (Vector2)owner.gameObject.transform.position;
        if (Mathf.Abs(movement.x) < 0.05 && Mathf.Abs(movement.y) < 0.05) { owner.stateMachine.ChangeState(new EnemyStateWalking(owner)); }
        movement.Normalize();
        animator.SetFloat("hdir", movement.x);
        animator.SetFloat("vdir", movement.y);
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
