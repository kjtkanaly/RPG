using Godot;
using System;
using Godot.Collections;

public partial class FollowPlayer : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float speedModifier = 1.0f;

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
        if ((player.Position - characterDirector.Position).Length() <= 40f
            && player.Velocity.Length() <= 0) {
            return waitingForPlayer;
        }

        UpdateVelocity(delta);
        
        characterDirector.MoveAndSlide();

        return null;
    }

    // Protected

    // Private
    private void UpdateVelocity(float delta) 
    {
        // Get the current velocity snapshot
        Vector2 velocity = characterDirector.Velocity;

        float goalSpeed = characterDirector.GetMovementData().speed * speedModifier;

        // Get the update speed by moving the current speed towards the goal
        float currentSpeed = velocity.Length();
        float updateSpeed = Mathf.MoveToward(
                currentSpeed, 
                goalSpeed, 
                characterDirector.GetMovementData().acceleration * delta);

        // Get the unit vector towards the player
        Vector2 direction = (player.Position - characterDirector.Position).Normalized();

        // Set the latVelocity to have magntiude of the update speed times speedmodifier
        velocity = direction * updateSpeed * speedModifier;

        // Update the Character Body's Velocity
        characterDirector.Velocity = velocity;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
