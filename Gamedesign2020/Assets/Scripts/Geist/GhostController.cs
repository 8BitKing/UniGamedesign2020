using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    public float movementSpeed = 1f;
    public float acceleration = 0.1f;


    public Rigidbody2D rb;
    public Animator animator;

    private double timer = 0;


    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        this.stateMachine.ChangeState(new Ghost_StateIdle(this));
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
       // transform.Translate(new Vector2(0,(float) 0.1)*Mathf.Sin(((float) timer)/100)*Time.deltaTime);


        this.stateMachine.runStateUpdate();
    }

    void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();
    }
}
