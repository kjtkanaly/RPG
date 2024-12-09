using Godot;
using System;
using Godot.Collections;

public partial class FollowPlayer : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float speedModifier = 1.0f;
    public float followRadius = 40.0f;

    // Protected

    // Private
    [Export] private CharacterBodyState idle;
    [Export] private CharacterBodyState waitingForPlayer;
    private CharacterDirector player;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        player = (CharacterDirector) GetTree().GetNodesInGroup("Player")[0];
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override State ProcessPhysics(float delta)
    {
        // Position difference to player
        Vector2 posDiff = player.Position - characterDirector.Position;

        if (posDiff.Length() < followRadius) {
            UpdateVelocity(0, (float) delta);
        }
        else {
            UpdateVelocity(
                characterDirector.GetMovementData().speed * speedModifier, 
                (float) delta);
        }
        
        characterDirector.MoveAndSlide();

        return null;
    }

    // Protected

    // Private
    private void UpdateVelocity(float goalSpeed, float delta) 
    {
        // Get the current velocity snapshot
        Vector2 velocity = characterDirector.Velocity;

        // Get the update speed by moving the current speed towards the goal
        float currentSpeed = velocity.Length();
        float updateSpeed = Mathf.MoveToward(
                currentSpeed, 
                goalSpeed, 
                characterDirector.GetMovementData().acceleration * delta);

        // Get the unit vector towards the player
        Vector2 direction = (player.Position - characterDirector.Position).Normalized();

        // Set the latVelocity to have magntiude of the update speed times speedmodifier
        velocity = direction * updateSpeed;

        // Update the Character Body's Velocity
        characterDirector.Velocity = velocity;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
