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
    [Export] private SelectEnemyBox selectEnemyBox;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void ShowMainAction() { mainAction.Visible = true; }

    public void HideMainAction() { mainAction.Visible = false; }

    public MainAction GetMainAction() { return mainAction; }

    public void ShowCharacterStats() { characterStats.Visible = true; }

    public void HideCharacterStats() { characterStats.Visible = false; }

    public CharacterStats GetCharacterStats() { return characterStats; }

    public void ShowSelectEnemyBox() { selectEnemyBox.Visible = true; }

    public void HideSelectEnemyBox() { selectEnemyBox.Visible = false; }

    public SelectEnemyBox GetSelectEnemyBox() { return selectEnemyBox; }

    public void HideUI() {
        
    }

    public void ShowUI() {

    }

    public void SetCharacterStats() 
    {
        
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
