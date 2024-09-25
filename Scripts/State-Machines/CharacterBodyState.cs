using Godot;
using System;

public partial class CharacterBodyState : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float moveSpeed;

    // Protected
    protected CharacterDirector characterDir;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public void Init(CharacterDirector bodyDirectorRef) 
    {
        base.Init(bodyDirectorRef);
        characterDir = bodyDirectorRef;
    }

    // Protected
    protected bool IsMoving() {
        if (Input.IsActionPressed("Left") 
            || Input.IsActionPressed("Right")
            || Input.IsActionPressed("Down")
            || Input.IsActionPressed("Up")) {
            return true;
        }
        return false;        
    }

    protected Vector2 GetDirectionVector() {
        Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
        return direction;
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}