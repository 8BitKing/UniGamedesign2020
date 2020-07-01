using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

using System;

public class EnemyController : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();
    public float speed = .5f;

    public PathCreator[] pathCreator;
    public bool[] patrol;
    public GameObject Sprite;
    public GridDebug gridObject;
    public int visionRange = 3;

    public Rigidbody2D rb;
    public Animator animator;
    // [NonSerialized]
    
    [NonSerialized]
    public Vector3 goal;




    void Start()
    {

        this.animator = Sprite.GetComponent<Animator>();
        this.stateMachine.ChangeState(new EnemyStateWalking(this));
    }

    // Update is called once per frame
    void Update()
    {
        //print(this.stateMachine.getCurrentState());
        this.stateMachine.runStateUpdate();

    }
    private void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();

        
        
    }
}
