using UnityEngine;

public class StateMachine
{
    public EntityState prevState { get; private set; }
    public EntityState currentState { get; private set; }
    public EntityState nextState { get; private set; }
    public bool changed { get; private set; } = false;



    public void Initialize(EntityState startState)
    {
        currentState = startState;
        currentState.Enter();
        //ChangeState(startState);
    }

    public void ChangeState(EntityState newState)
    {
        //currentState.Exit();
        //currentState = newState;
        //currentState.Enter();
        changed = true;
        nextState = newState;
    }

    public void UpdateActiveState()
    {
        // Process state change before updating the current state
        // To avoid one more Update called in the previous state after changing.
        HandleStateChange();
        currentState.Update();


        //if (updated)
        //{
        //    updated = false;

        //}
        //if (nextState != null && nextState != currentState)
        //{
        //    currentState.Exit();
        //    currentState = nextState;
        //    currentState.Enter();
        //    nextState = null;
        //}
    }
    private void HandleStateChange()
    {
        if (!changed) return;

        changed = false;

        prevState = currentState;
        prevState.Exit();
        nextState.Enter();
        currentState = nextState;
    }

}
