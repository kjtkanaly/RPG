using Godot;
using System;
using Godot.Collections;

public partial class Search : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State agroState;
    private Vector2 timerRange = new Vector2(0.5f, 1f);
    private SceneTreeTimer movingTimer;
    private SceneTreeTimer standingTimer;
    private int previousDirection = -1;

    //-------------------------------------------------------------------------
    // Game Events

    // Roaming feature will walk in a given direction for x seconds
    // then stop for x seconds. Check the Area2D each process to see if we
    // are in range

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Kick off the roaming feature
        StartStanding();
    }

    public override State ProcessPhysics(float delta)
    {
        if (IsPlayerInAgroRange()) {
            return agroState;
        }

        characterDirector.MoveAndSlide();

        return null;
    }

    // Protected

    // Private
    private void StartMoving()
    {
        float timerTime = characterDirector.GetMain().rng.RandfRange(
            timerRange.X, 
            timerRange.Y);

        Vector2 direction = GetRandomDirection();
        characterDirector.Velocity = direction * characterDirector.GetMovementData().walkSpeed;

        movingTimer = GetTree().CreateTimer(timerTime);
        movingTimer.Timeout += StartStanding;
    }

    private void StartStanding()
    {
        float timerTime = characterDirector.GetMain().rng.RandfRange(
            timerRange.X, 
            timerRange.Y);

        characterDirector.Velocity = Vector2.Zero;

        standingTimer = GetTree().CreateTimer(timerTime);
        standingTimer.Timeout += StartMoving;
    }

    private Vector2 GetRandomDirection() {
        Vector2 output = new Vector2();

        int roll = previousDirection;
        while (roll == previousDirection) {
            roll = characterDirector.GetMain().rng.RandiRange(0, 3);
        }

        switch (roll) {
            case (0):
                output = Vector2.Down;
                break;
            case (1):
                output = Vector2.Right;
                break;
            case (2):
                output = Vector2.Up;
                break;
            case (3):
                output = Vector2.Left;
                break;
        }

        previousDirection = roll;

        return output;
    }

    private bool IsPlayerInAgroRange() 
    {
        // Get the objects in the area 2D
        Array<Godot.Node2D> bodies = 
            characterDirector.GetAgroArea().GetOverlappingBodies();

        foreach(Node2D node in bodies) {
            if (node.IsInGroup("Player")) {
                return true;
            }
        }

        return false;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
