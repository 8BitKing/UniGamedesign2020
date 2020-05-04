using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    public float movementSpeed = 1f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        this.stateMachine.ChangeState(new Ghost_StateIdle(this, animator));
    }

    // Update is called once per frame
    void Update()
    {
        this.stateMachine.runStateUpdate();
    }

    void FixedUpdate()
    {
        this.stateMachine.runStateFixedUpdate();
    }
}
