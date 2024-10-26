using Godot;
using System;

public partial class BattleSceneUI : CanvasLayer
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private MainAction mainAction;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void ShowMainAction() 
    {
        mainAction.Visible = true;
    }

    public void HideMainAction()
    {
        mainAction.Visible = false;
    }

    public MainAction GetMainAction()
    {
        return mainAction;
    }

    public void HideUI() {
        
    }

    public void ShowUI() {

    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
