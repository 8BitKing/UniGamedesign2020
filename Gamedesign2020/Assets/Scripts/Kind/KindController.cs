using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class KindController : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();
    public float speed = 2;
    public PathCreator pathCreator;




    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;
    private Vector2 exitPosition;


    public void setExitPos(Vector2 exitPos)
    {
        exitPosition = exitPos;
    }
    void Start()
    {
        this.stateMachine.ChangeState(new Kind_StateWalking(this));
    }

    // Update is called once per frame
    void Update()
    {
        this.stateMachine.runStateUpdate();
    }
}
