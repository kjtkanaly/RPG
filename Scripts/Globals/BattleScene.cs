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
    [Export] private BattleStateMachine stateMachine;
    private CharacterData[] playerTeam;
    private CharacterData[] enemyTeam;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Init();
    }

    public override void _Process(double delta)
    {
        stateMachine.ProcessGeneral((float) delta);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init() 
    {
        main = GetNode<Main>("/root/Main");

        // Get the Battle Info
        BattleQueue battleQueue = main.GetBattleQueue();

        // Set the Player Team Info

        // Set the Enemy Team Info

        // Init the Battle Scene State Machine
        stateMachine.Init(this);
    }

    public void LeaveBattle() 
    {
        if (main.GetPreviousScene() == null) {
            GetTree().UnloadCurrentScene();
        }

        GetTree().ChangeSceneToPacked(main.GetPreviousScene());
    }

    public CharacterData GetPlayerTeamDataAtIndex(int index) 
    {
        return playerTeam[index];
    }

    public CharacterData GetEnemyTeamDataAtIndex(int index)
    {
        return enemyTeam[index];
    }

    public BattleSceneUI GetBattleUI() 
    {
        return battleUI;
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
