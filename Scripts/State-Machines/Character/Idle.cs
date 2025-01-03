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
    [Export] private State battleQueueState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        base.Enter();

        characterDirector.Velocity = Vector2.Zero;
    }

    override public State ProcessGeneral(float delta) {
        // Check if a basic enemy has entered the combat zone
        if (IsGroupMemeberInComabatRange("Basic-Enemy")) {
            return battleQueueState;
        }

        // Check if the run state is triggered
        if (IsMoving()) {
            return runState;
        }

        // Check if the character is trying to interact
        if (IsInteracting() && characterDirector.CanInteract()) {
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
