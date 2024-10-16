using Godot;
using System;

public partial class BattleStateMachine : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private BattleState currentState;
    [Export] private BattleState startingState;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(BattleScene scene)
    {
        // Init all of the child state objects
        foreach (BattleState child in GetChildren()) {
            child.Init(scene);
        }

        // Initialize to the default state
        ChangeState(startingState);
    }

    public void ChangeState(BattleState newState) 
    {   
        // If there is a current state, call exit method
        if (currentState != null) {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void ProcessInput(InputEvent inputEvent) {
        BattleState newState = currentState.ProcessInput(inputEvent);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    public void ProcessPhysics(float delta)
    {
        BattleState newState = currentState.ProcessPhysics(delta);
        if (newState != null) {
            ChangeState(newState);
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}