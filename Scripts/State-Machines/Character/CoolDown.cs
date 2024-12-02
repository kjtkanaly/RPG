using Godot;
using System;

public partial class CoolDown : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State searchState;
    private SceneTreeTimer timer;
    private float coolDownTime = 2.0f;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Set the Velocity to Zero
        characterDirector.Velocity = Vector2.Zero;

        // Start the cool off timer
        timer = GetTree().CreateTimer(coolDownTime);
    }

    public override State ProcessPhysics(float delta)
    {
        if (timer.TimeLeft <= 0) {
            return searchState;
        }

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
