using Godot;
using System;

public partial class BattleSceneUI : CanvasLayer
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private CharacterBattleSceneInfo characterInfo;
    [Export] private BattleSceneMainAction mainAction;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void SetCharacterInfo(CharacterData data) 
    {
        characterInfo.SetCharacterInfo(data);
    }

    public BattleSceneMainAction GetMainAction()
    {
        return mainAction;
    }

    public CharacterBattleSceneInfo GetCharacterInfo()
    {
        return characterInfo;
    }

    public void HideUI() {
        mainAction.Visible = false;
        characterInfo.Visible = false;
    }

    public void ShowUI() {
        mainAction.Visible = true;
        characterInfo.Visible = true;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}