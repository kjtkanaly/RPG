using Godot;
using System;
using System.Collections.Generic;

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
    [Export] private BattleSceneCharacter[] enemyTeamSceneNodes;
    private List<CharacterData> playerTeam = new List<CharacterData>();
    private List<CharacterData> enemyTeam = new List<CharacterData>();

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
        SetTeamInfo(playerTeam, battleQueue.GetPlayerTeam());

        // Set the Enemy Team Info
        SetTeamInfo(enemyTeam, battleQueue.GetEnemyTeam());

        // Set the Enemy Team Sprties
        SetTeamSprites(enemyTeam, enemyTeamSceneNodes);

        // Init the Battle Scene State Machine
        stateMachine.Init(this);
    }

    public void LeaveBattle() 
    {
        //TODO: Setup a system to make sure the character data has all been updated
        //      Likely need to first just verify if this done via reference
        main.EndBattle(playerTeam, enemyTeam);
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

    public BattleSceneCharacter GetEnemyNodeAtIndex(int index) 
    {
        return enemyTeamSceneNodes[index];
    }

    // Protected

    // Private
    private void SetTeamInfo(
        List<CharacterData> teamInfo,
        List<CharacterData> inTeamInfo) 
    {
        // Clear the old teamInfo
        teamInfo.Clear();

        // Copy over the data
        foreach (CharacterData data in inTeamInfo) {
            teamInfo.Add(data);
        }
    }

    private void SetTeamSprites(
        List<CharacterData> teamData, 
        BattleSceneCharacter[] teamSprites)
    {
        for (int i = 0; i < teamData.Count; i++) {
            teamSprites[i].GetSprite().Texture = teamData[i].GetBattleSprite();
        }
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
