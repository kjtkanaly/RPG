using Godot;
using System;
using Godot.Collections;

public partial class ButtonGroupUI : Control
{
    public enum TEXT_TYPE 
    {
        ENABLED,
        DISABLED
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    [Export] protected ButtonGroup group;
    protected Main main;
    protected Array<Godot.BaseButton> buttons;
    protected int currentButtonIndex = 0;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        // Get the global player stats object
        main = GetNode<Main>("/root/Main");

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

        // Check if the new button index is disabled
        if (buttons[newButtonIndex].Disabled == true) {
            return;
        }

        currentButtonIndex = newButtonIndex;
        buttons[currentButtonIndex].ButtonPressed = true;

        // Play Sound Effect
        main.PlayIncrementSoundEffect();
    }

    public bool GetSelectedButtonBoolValue()
    {
        SelectButton();

        BaseButton pressedButton = group.GetPressedButton();

        bool value = (bool) pressedButton.GetMeta("value");
        return value;
    }

    public int GetSelectedButtonIntValue()
    {
        SelectButton();

        BaseButton pressedButton = group.GetPressedButton();

        int value = (int) pressedButton.GetMeta("value");
        return value;
    }

    // Protected
    public void SetButtonPressedByName(string inName)
    {
       BaseButton pressedButton = group.GetPressedButton();

        for(int i = 0; i < buttons.Count; i++) {
            if ((string) buttons[i].GetMeta("Name") == inName) {
                buttons[i].ButtonPressed = true;
            }
        }
    }

    // Private
    private void SelectButton() 
    {
        main.PlaySelectSoundEffect();
        // await ToSignal(
        //     main.GetSoundEffectPlayer(), 
        //     AudioStreamPlayer2D.SignalName.Finished);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
