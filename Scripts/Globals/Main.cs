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
    [Export] private TextBox itemTextBox;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void DisplayItemTextBox(ItemData data) 
    {
        itemTextBox.ShowTextBox(data.textBoxString, null);
        GetTree().Paused = true;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
