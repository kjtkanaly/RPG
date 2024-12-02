using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private State currentState;
    [Export] private State startingState;
    [Export] protected CharacterBodyState idleState;
    [Export] protected CharacterBodyState coolDownState;
    [Export] protected CharacterBodyState agroState;
    [Export] protected CharacterBodyState followPlayerState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    // Init the Character Body State Machine
    public void Init(CharacterDirector objectDirRef) {
        // Init all of the child state objects
        foreach (State child in GetChildren()) {
            child.Init(objectDirRef);
        }

        // Initialize to the default state
        ChangeState(startingState);
    }

    public void ChangeState(State newState) {
        // If there is a current state, call any exit logic
        if (currentState != null) {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void ProcessInput(InputEvent inputEvent) {
        State newState = currentState.ProcessInput(inputEvent);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    public void PhysicsProcess(float delta)
    {
        State newState = currentState.ProcessPhysics(delta);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    public void ProcessGeneral(float delta)
    {
        State newState = currentState.ProcessGeneral(delta);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    public State GetCurrentState() 
    {
        return currentState;
    }

    public void SwitchCurrentStateToIdle()
    {
        ChangeState(idleState);
    }

    public void SwitchCurrentStateToCoolDown() 
    {
        ChangeState(coolDownState);
    }

    public void SwitchCurrentStateToAgro()
    {
        ChangeState(agroState);
    }

    public void SwitchCurrentStateToFollowThePlayer()
    {
        ChangeState(followPlayerState);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
