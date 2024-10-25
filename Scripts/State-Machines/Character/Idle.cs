using Godot;
using System;

public partial class Idle : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State runState;
    [Export] private State interactState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        base.Enter();

        characterDir.Velocity = Vector2.Zero;
    }

    override public State ProcessInput(InputEvent inputEvent) {
        // Check if the run state is triggered
        if (IsMoving()) {
            return runState;
        }

        if (CanInteract()) {
            // Decide Interaction Path
            return interactState;
        }

        return null;
    }

    public override State ProcessPhysics(float delta)
    {
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
