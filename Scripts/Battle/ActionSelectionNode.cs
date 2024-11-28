using Godot;
using System;
using Godot.Collections;

public partial class ActionSelectionNode : Node2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private ActionSelection ui;
    private BattleSceneNew battleScene;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(BattleSceneNew inScene)
    {
        battleScene = inScene;

        // Set the Target Enemy UI to the first enemy's Positon
        Vector2 pos = battleScene.GetPlayerNodeAtIndex(0).Position;
        Position = pos;

        // Set the UI invisible till it is needed
        Visible = false;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
