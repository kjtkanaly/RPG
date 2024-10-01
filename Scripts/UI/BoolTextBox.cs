using Godot;
using System;

public partial class BoolTextBox : TextBox
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal] public delegate void SelectionMadeEventHandler();

    // Protected
    [Export] protected Button[] buttonGroup;
    protected int currentButtonIndex = 0;

    // Private

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
            CycleSelectedButton(-1);
        }
        else if (Input.IsActionJustPressed("Right")) {
            CycleSelectedButton(1);
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
        return currentButtonIndex == 1;
    }

    // Protected
    protected void CycleSelectedButton(int step)
    {
        int newButtonIndex = currentButtonIndex + step;

        // Bounds Check
        if ((newButtonIndex < 0) || (newButtonIndex >= buttonGroup.Length)) 
        {
            return;
        }

        currentButtonIndex = newButtonIndex;
        buttonGroup[currentButtonIndex].ButtonPressed = true;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}