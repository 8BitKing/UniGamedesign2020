using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState currentState;
    private IState previousState;

    public void ChangeState(IState newState)
    {
        if (currentState != null) {
            this.currentState.stateExit();
            this.previousState = currentState;
        }

        this.currentState = newState;
        this.currentState.stateInit();

    }

    public void ChangeToPreviousState() {
        ChangeState(this.previousState);
    }

    public void runStateUpdate()
    {
        if (this.currentState != null) 
        {
            this.currentState.stateUpdate();
        }
    }

    public void runStateFixedUpdate() 
    {
        if (this.currentState != null)
        {
            this.currentState.stateFixedUpdtate();
        }
    }

    public void stateTriggerEnter(Collider2D collision) {
        if (this.currentState != null)
        {
            this.currentState.stateOnTriggerEnter(collision);
        }
    }

    public void stateTriggerExit(Collider2D collision)
    {
        if (this.currentState != null)
        {
            this.currentState.stateOnTriggerExit(collision);
        }
    }
}
