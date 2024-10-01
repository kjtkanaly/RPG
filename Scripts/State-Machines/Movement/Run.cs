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
        // Update the Character's Velcoity
        UpdateVelocity(delta);

        // Move the character body around
        characterDir.MoveAndSlide();

        // Orientate the body
        UpdateCharacterSprite();
        UpdateItemRayCastDirection();

        // Check if the player is now idle
        if (characterDir.Velocity.Length() <= 0.01) {
            return idleState;
        }
        
        return null;
    }

    // Protected

    // Private
    private void UpdateVelocity(float delta) 
    {
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
    }

    private void UpdateCharacterSprite() 
    {
        // Get the X component of the Direction vector
        float direction = GetDirectionVector().X;

        // If the direction is 0 then return
        if (direction == 0) {
            return;
        }

        // If the direction is non-zero set accordingly
        Vector2 scale = characterDir.GetSprite().Scale;
        if (direction > 0 ) {
            scale.X = 1;
        }
        else if (direction < 0) {
            scale.X = -1;
        }
        
        // Update the Sprite Object
        characterDir.GetSprite().Scale = scale;
    }

    private void UpdateItemRayCastDirection() 
    {
        // Get the X component of the Direction vector
        Vector2 direction = GetDirectionVector().Normalized();

        if (direction.Length() == 0.0f) {
            return;
        }

        // Get the Item Ray's Length
        float rayLength = characterDir.itemRayLength;

        // Update the Ray's Target Position
        characterDir.GetItemRay().TargetPosition = direction * rayLength;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
