using Godot;
using System;

public partial class Main : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public RandomNumberGenerator rng;

    // Protected

    // Private
    [Export] private BoolTextBox itemTextBox;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public async void DisplayItemTextBox(ItemDirector item) 
    {
        // Pause the other processes
        GetTree().Paused = true;

        // Display the Text Box
        Tween textTween = itemTextBox.ShowTextBox(item.GetData().textBoxString, null);

        // Await Text being completed
        await ToSignal(textTween, "finished");

        // Await User Choice
        await ToSignal(itemTextBox, "SelectionMade");

        // Get the User Choice
        bool choice = itemTextBox.GetUserChoice();
        
        // Set the item to cool down to avoid user response delay
        item.coolDown = true;

        // Do something based upon the choice
        if (choice) {
            // Keep the Data

            // Destory the Item
            item.QueueFree();
        } else {
            item.StartResponseDelayTime();
        }

        // Hide the text
        itemTextBox.HideTextBox();

        // Resume the game
        GetTree().Paused = false;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
