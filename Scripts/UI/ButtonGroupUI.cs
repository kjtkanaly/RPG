using Godot;
using System;

public partial class ButtonGroupUI : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    protected int currentButtonIndex;
    [Export] protected Button[] buttons;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void IncrementSelectedButton(int step) 
    {
        // Get the new button index
        int newButtonIndex = currentButtonIndex + step;

        // Check if the button index can increment
        if ((newButtonIndex < 0) || (newButtonIndex >= buttons.Length)) 
        {
            return;
        }

        currentButtonIndex = newButtonIndex;
        buttons[currentButtonIndex].ButtonPressed = true;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}