using Godot;
using System;
using Godot.Collections;

public partial class CharacterBodyState : State
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public float moveSpeed;

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    override public void Init(CharacterDirector bodyDirectorRef) 
    {
        base.Init(bodyDirectorRef);
    }

    // Protected
    protected bool IsMoving() 
    {
        if (Input.IsActionPressed("Left") 
            || Input.IsActionPressed("Right")
            || Input.IsActionPressed("Down")
            || Input.IsActionPressed("Up")) {
            return true;
        }
        return false;        
    }

    protected Vector2 GetDirectionVector() 
    {
        Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
        return direction;
    }

    protected bool IsInteracting()
    {
        if (!Input.IsActionJustPressed("Interact")) {
            return false;
        }
        
        // Get the potential item's Node object
        Node collider = (Node) characterDirector.GetInteractRay().GetCollider();

        // Check if we actually collided with something
        if (collider == null) {
            return false;
        }

        return true;
    }

    protected bool IsGroupMemeberInComabatRange(string groupName) 
    {
        // Safety check if no combat range
        if (characterDirector.GetCombatArea() == null) {
            return false;
        }
        
        Array<Node2D> bodies = 
            characterDirector.GetCombatArea().GetOverlappingBodies();

        for (int i = 0; i < bodies.Count; i++) {
            if (bodies[i].IsInGroup(groupName)) {
                return true;
            }
        }

        return false;
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}