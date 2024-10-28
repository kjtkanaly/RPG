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
    [Export] private CharacterStats characterStats;

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

    public void ShowCharacterStats() 
    {
        characterStats.Visible = true;
    }

    public void HideCharacterStats() 
    {
        characterStats.Visible = false;
    }

    public CharacterStats GetCharacterStats() 
    {
        return characterStats;
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
