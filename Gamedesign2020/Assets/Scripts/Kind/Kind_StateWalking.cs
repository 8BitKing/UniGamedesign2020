using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Kind_StateWalking : IState
{

    private KindController owner;
    private Animator animator;
    private Rigidbody2D rigidbody;


    private PathCreator pathCreator;

    private float distanceTravelled;

    private float speed;
    private Vector2 movement;
    private Vector2 exitPos;
    private Transform transform;

    public Kind_StateWalking(KindController owner)
    {
        this.owner = owner;
        this.animator = owner.animator;
        this.rigidbody = owner.rb;
        this.speed = owner.speed;
        this.movement = owner.movement;
        this.transform = owner.transform;
        this.pathCreator = owner.pathCreator;
    }
    public void stateInit()
    {
        this.animator.Play("Walk Animations", -1, 0);
    }


    public void stateExit()
    {
        owner.setExitPos(exitPos);
    }


    public void stateUpdate()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);


        movement.x = -(pathCreator.path.GetNormalAtDistance(distanceTravelled).x);
        movement.y = (pathCreator.path.GetNormalAtDistance(distanceTravelled).y);
        movement.Normalize();
        movement = Quaternion.Euler(0, 0, 90) * movement;

        animator.SetFloat("hSpeed", movement.x);
        animator.SetFloat("vSpeed", movement.y);
        animator.SetFloat("speed", movement.magnitude);

        owner.movement = this.movement;
    }


    public void stateFixedUpdtate()
    {
        
    }
}
