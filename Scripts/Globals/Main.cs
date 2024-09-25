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

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
    }

    //-------------------------------------------------------------------------
	// Methods
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
