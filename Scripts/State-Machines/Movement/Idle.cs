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
