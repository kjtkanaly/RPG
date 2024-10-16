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

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}