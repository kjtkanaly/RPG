using Godot;
using System;

public partial class BattleSceneCharacter : Node2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private Sprite2D sprite;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public Sprite2D GetSprite() {return sprite;}

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
