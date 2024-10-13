using Godot;
using System;

public partial class BattleScene : Node2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Main main;
    [Export] private CharacterBattleSceneInfo characterInfo;
    [Export] private TextureRect sceneImageRect;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        main = GetNode<Main>("/root/Main");

        // Get the character data
        CharacterData characteData = main.GetIndexCharacterData(0);

        // Set the character info
        characterInfo.SetCharacterInfo(characteData);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
