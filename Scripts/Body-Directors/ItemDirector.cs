using Godot;
using System;

public partial class ItemDirector : Area2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private ItemData data;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public ItemData GetData() 
    {
        return data;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
