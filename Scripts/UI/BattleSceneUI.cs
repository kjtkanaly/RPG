using Godot;
using System;
using Godot.Collections;

public partial class BattleSceneUI : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private CharacterInfoContainer characterInfoContainer;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(Array<BattleSceneCharacter> characters) 
    {
        characterInfoContainer.Init(characters);
    }

    public void UpdateInfoByCharacter(BattleSceneCharacter character) 
    {
        characterInfoContainer.UpdateInfoByCharacter(character);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
