using Godot;
using System;

public partial class BattleSceneEnemy : TextureButton
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    [Export] protected TextureRect sprite;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public

    public TextureRect GetSprite() { return sprite; }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
