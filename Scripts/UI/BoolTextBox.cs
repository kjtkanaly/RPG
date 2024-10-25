using Godot;
using System;

public partial class BoolTextBox : TextBox
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal] public delegate void SelectionMadeEventHandler();

    // Protected

    // Private
    [Export] private ButtonGroupUI boolButtons;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        // If hiden then don't check for input
        if (!Visible) {
            return;
        }

        // ButtonInteractionStuff
        if (Input.IsActionJustPressed("Left")) {
            boolButtons.IncrementSelectedButton(-1);
        }
        else if (Input.IsActionJustPressed("Right")) {
            boolButtons.IncrementSelectedButton(1);
        }

        // Check if a selection has been made
        if (Input.IsActionJustReleased("Interact")) {
            EmitSignal(SignalName.SelectionMade);
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public bool GetUserChoice() 
    {   
        return boolButtons.GetSelectedBoolButtonValue();
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
