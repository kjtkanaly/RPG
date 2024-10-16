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
    [Export] private BattleSceneUI battleUI;
    [Export] private CharacterData[] enemies;
    [Export] private BattleStateMachine stateMachine;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        main = GetNode<Main>("/root/Main");

        // Get the character data
        CharacterData characteData = main.GetIndexCharacterData(0);

        // Set the character info
        battleUI.SetCharacterInfo(characteData);

        stateMachine.Init(this);
    }

    public override void _Process(double delta)
    {
        stateMachine.ProcessPhysics((float) delta);
    }

    public void FleeBattle() 
    {
        GetTree().ChangeSceneToPacked(main.GetPreviousScene());
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
