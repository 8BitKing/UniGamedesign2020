using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using CodeMonkey.Utils;
using System;

public class KindControllerRaycast : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();
    public float speed = 2;

    public GameObject Sprite;
    public GridDebug gridObject;


    public Rigidbody2D rb;
    public Animator animator;
   // [NonSerialized]
    public Vector2 movement;
    [NonSerialized]
    public Vector3 goal;            




    void Start()
    {
        
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new Kind_StateWalkingRaycast(this));
    }

    // Update is called once per frame
    void Update()
    {
        this.stateMachine.runStateUpdate();
       
    }
    private void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();

        //Move Child
        this.rb.MovePosition(this.rb.position + this.movement *speed * Time.fixedDeltaTime);
    }
}
