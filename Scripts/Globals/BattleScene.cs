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
    private List<CharacterData> battleOrder = new List<CharacterData>();

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

        // Determine the battle order
        SetBattleOrder();

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

    private void SetBattleOrder() 
    {
        // Init the battle order list
        battleOrder.Clear();

        AddTeamToBattleOrder(playerTeam);
        AddTeamToBattleOrder(enemyTeam);

        // Debug
        GD.Print("\n");
        for (int i = 0; i < battleOrder.Count; i++) {
            GD.Print($"{i}: {battleOrder[i].GetName()}");
        }
    }

    private void AddTeamToBattleOrder(List<CharacterData> teamData)
    {
        // Loop over the player team
        for (int i = 0; i < teamData.Count; i++) {
            // Roll initiative
            int initiativeVal = GetInitiative(teamData[i].GetStatByKey("DEX"));

            GD.Print($"{teamData[i].GetName()} roll: {initiativeVal}");

            // Set position in the battle order
            AppendBattleOrder(teamData[i], initiativeVal);
        }
    }

    private int GetInitiative(int dexValue) 
    {
        // Roll the d20
        int roll = main.rng.RandiRange(1, 20);

        return roll;
    }

    private void AppendBattleOrder(CharacterData data, int initiativeVal)
    {
        // Init the battle index
        int battleOrderIndex = 0;

        // Go through the current battle order 
        for (int i = battleOrder.Count - 1; i > -1; i--) {
            // If the initiative value is less than the current position's value
            if (initiativeVal > battleOrder[i].GetInitiativeValue()) {
                continue;
            }

            battleOrderIndex = i;

            // Check if current battle pos is equal to the new initiative value
            if (initiativeVal == battleOrder[battleOrderIndex].GetInitiativeValue()) {
                DetermineWhichCharacterGoesFirst(data, battleOrderIndex);
            }

            break;
        }

        // Insert the position and log it's initiative value
        data.SetInitiative(initiativeVal);
        battleOrder.Insert(battleOrderIndex, data);
    }

    // Used in scenario's where two character's roll equal initiative
    private int DetermineWhichCharacterGoesFirst(
        CharacterData data, 
        int battleOrderIndex)
    {
        // Check if the character at the current index has higher dex value
        if (data.GetStatByKey("DEX") < battleOrder[battleOrderIndex].GetStatByKey("DEX")) {
            battleOrderIndex -= 1;
        }
        // Check both characters have equal data
        else if (data.GetStatByKey("DEX") == battleOrder[battleOrderIndex].GetStatByKey("DEX")) {
            // Flip a coin
            if (main.rng.RandiRange(0, 1) == 0) {
                battleOrderIndex -= 1;
            }
        }

        return battleOrderIndex;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
