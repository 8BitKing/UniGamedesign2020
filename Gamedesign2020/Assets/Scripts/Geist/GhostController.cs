using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    public GameObject Sprite;
    public float movementSpeed = 1f;
    public float acceleration = 1f;
    public float dashSpeed = 4f;
    public float dashTime = 0.15f;
    public float lastDash = -1;
    public float dashCooldown = 0.5f;
    public BoxCollider2D hitbox;

    public Vector2 movement;

    public Rigidbody2D rb;
    [NonSerialized]
    public Animator animator;


    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {   
        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new Ghost_StateIdle(this));
    }

    // Update is called once per frame
    void Update()
    {
        this.stateMachine.runStateUpdate();
    }

    void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();

        //Move Ghost
        this.rb.MovePosition(this.rb.position + this.movement  * Time.fixedDeltaTime);
    }

    //--Breakout Functions




    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.stateMachine.stateTriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.stateMachine.stateTriggerExit(collision);
    }

    public Boolean BreakoutDash()
    {
        if (Time.time-lastDash >= dashCooldown)
        {
            if (Input.GetButtonDown("Dash"))
            {
                this.stateMachine.ChangeState(new Ghost_StateDash(this));
                return true;
            }
        }
        return false;
    }

    public Boolean BreakoutIdle()
    {
        stateMachine.ChangeState(new Ghost_StateIdle(this));
        return true;
    }
}
