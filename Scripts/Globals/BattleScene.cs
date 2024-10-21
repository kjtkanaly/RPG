using Godot;
using System;

public partial class BattleScene : Node2D
{
    public enum Choice
    {
        Flee,
        Attack,
        Null
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public Main main;
    public int currentAllyIndex = 0;
    public int currentEnemyIndex = 0;

    // Protected

    // Private
    [Export] private BattleSceneUI battleUI;
    [Export] private CharacterData player;
    [Export] private CharacterData enemy;
    [Export] private BattleStateMachine stateMachine;
    [Export] private Control playerPosition;
    [Export] private Control enemyPosition;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        main = GetNode<Main>("/root/Main");

        // Get the character data
        player = main.GetCharacterDataAtIndex(0);

        // Set the character info
        battleUI.SetCharacterInfo(player);

        stateMachine.Init(this);
    }

    public override void _Process(double delta)
    {
        stateMachine.ProcessPhysics((float) delta);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void LeaveBattle() 
    {
        if (main.GetPreviousScene() == null) {
            GetTree().UnloadCurrentScene();
        }

        GetTree().ChangeSceneToPacked(main.GetPreviousScene());
    }

    public CharacterData GetPlayerData() 
    {
        return player;
    }

    public CharacterData GetEnemyData()
    {
        return enemy;
    }

    public BattleSceneUI GetUI() 
    {
        return battleUI;
    }

    public Control GetPlayerPosition()
    {
        return playerPosition;
    }

    public Control GetEnemyPositon()
    {
        return enemyPosition;
    }

    public void HideUI() 
    {
        battleUI.HideUI();
    }

    public void ShowUI() 
    {
        battleUI.ShowUI();
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
