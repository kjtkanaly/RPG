using Godot;
using System;

public partial class MainAction : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Down")) {
            IncrementSelectedButton(1);
        }
        else if (Input.IsActionJustPressed("Up")) {
            IncrementSelectedButton(-1);
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
