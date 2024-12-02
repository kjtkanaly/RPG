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

        // Set the UI invisible till it is needed
        Visible = false;
    }

    public void NewTurn(BattleSceneCharacter character)
    {
        Position = character.Position;

        battleScene.GetActionSelectionNode().Visible = true;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
