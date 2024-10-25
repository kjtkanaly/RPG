using Godot;
using System;

public partial class ButtonGroupUI : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    [Export] protected ButtonGroup group;
    protected Godot.Collections.Array<Godot.BaseButton> buttons;
    protected int currentButtonIndex = 0;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        // Get the buttons in the group
        buttons = group.GetButtons();

        // Get the current index
        currentButtonIndex = GetPressedButtonIndex();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public int GetPressedButtonIndex() 
    {   
        BaseButton pressedButton = group.GetPressedButton();

        for(int i = 0; i < buttons.Count; i++) {
            if (buttons[i] == pressedButton) {
                return i;
            }
        }

        return -1;
    }

    public void IncrementSelectedButton(int step) 
    {
        // Get the new button index
        int newButtonIndex = currentButtonIndex + step;

        // Check if the button index can increment
        if ((newButtonIndex < 0) || (newButtonIndex >= buttons.Count)) 
        {
            return;
        }

        currentButtonIndex = newButtonIndex;
        buttons[currentButtonIndex].ButtonPressed = true;
    }

    public bool GetSelectedBoolButtonValue()
    {
        BaseButton pressedButton = group.GetPressedButton();

        return (bool) pressedButton.GetMeta("value");
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
