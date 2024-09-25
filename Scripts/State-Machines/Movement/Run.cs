using Godot;
using System;

public partial class Run : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float speedModifier = 1.0f;

    // Protected

    // Private
    [Export] private State idleState;
    private Vector2 direction;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public State ProcessInput(InputEvent inputEvent) {
        // Trigger Jump Logic here
        return null;
    }

    override public State ProcessPhysics(float delta) {        
        // Get the current velocity snapshot
        Vector2 velocity = characterDir.Velocity;

        // Set the goal speed for the lateral movement
        float goalSpeed = 0;
        if (IsMoving()) {
            goalSpeed = characterDir.GetMovementData().speed * speedModifier;
        }

        // Get the update speed by moving the current speed towards the goal
        float currentSpeed = velocity.Length();
        float updateSpeed = Mathf.MoveToward(
                currentSpeed, 
                goalSpeed, 
                characterDir.GetMovementData().acceleration * delta);

        // Set the latVelocity to have magntiude of the update speed times speedmodifier
        velocity = GetDirectionVector() * updateSpeed * speedModifier;

        // Update the Character Body's Velocity
        characterDir.Velocity = velocity;

        // Move the character body around
        characterDir.MoveAndSlide();

        // Check if the player is now idle
        if (velocity.Length() <= 0.01) {
            return idleState;
        }
        
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
