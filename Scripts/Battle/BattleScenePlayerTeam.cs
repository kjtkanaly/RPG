using Godot;
using System;
using Godot.Collections;

public partial class BattleScenePlayerTeam : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private ActionSelection actionSelection;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void ShowActionSelection() { actionSelection.Visible = true; }

    public void HideActionSelection() { actionSelection.Visible = false; }

    public ActionSelection GetActionSelection() { return actionSelection; }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
