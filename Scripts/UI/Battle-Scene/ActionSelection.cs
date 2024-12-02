using Godot;
using System;
using Godot.Collections;

public partial class ActionSelection : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        VisibilityChanged += ResetButtons;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Attack-UI")) {
            SetButtonPressedByName("Attack");
        }
        else if (Input.IsActionJustPressed("Act-UI")) {
            SetButtonPressedByName("Act");
        } 
        else if (Input.IsActionJustPressed("Item-UI")) {
            SetButtonPressedByName("Item");
        } 
        else if (Input.IsActionJustPressed("Special-UI")) {
            SetButtonPressedByName("Special");
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void ResetButtons() 
    {
        for (int i = 0; i < buttons.Count; i++) {
            buttons[i].ButtonPressed = false;
        }
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
