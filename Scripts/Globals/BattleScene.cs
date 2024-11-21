using Godot;
using System;
using Godot.Collections;

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
    public int currentPlayerTeamIndex = 0;
    public int currentEnemyTeamIndex = 0;

    // Protected

    // Private
    [Export] private BattleSceneUI battleUI;
    [Export] private BattleStateMachine stateMachine;
    [Export] private BattleSceneCharacter[] enemyTeamSceneNodes = new BattleSceneCharacter[4];
    private Array<CharacterData> playerTeam = new Array<CharacterData>();
    private Array<CharacterData> enemyTeam = new Array<CharacterData>();
    private Array<CharacterData> battleOrder = new Array<CharacterData>();
    private int currentBattleOrderIndex = 0;

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

        // Reset the current battle order index
        currentBattleOrderIndex = 0;

        // Log the Enemy options
        UpdateEnemySelectOptions();

        // Init the Battle Scene State Machine
        stateMachine.Init(this);
    }

    public void LeaveBattle(bool playerVictory) 
    {
        //TODO: Setup a system to make sure the character data has all been updated
        //      Likely need to first just verify if this done via reference
        main.EndBattle(playerTeam, enemyTeam, playerVictory);
    }

    public CharacterData GetPlayerTeamDataAtIndex(int index) 
    {
        return playerTeam[index];
    }

    public Array<CharacterData> GetPlayerTeamData() { return playerTeam; }

    public CharacterData GetEnemyTeamDataAtIndex(int index)
    {
        return enemyTeam[index];
    }

    public Array<CharacterData> GetEnemyTeamData() { return enemyTeam; }

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

    public bool IsInPlayerTeam(CharacterData data) 
    {
        for (int i = 0; i < playerTeam.Count; i++) {
            if (data.GetName() == playerTeam[i].GetName()) {
                return true;
            }
        }
        return false;
    }

    public void StepCurrentBattleOrderIndex() 
    {
        currentBattleOrderIndex += 1;
        if (currentBattleOrderIndex >= battleOrder.Count) {
            currentBattleOrderIndex -= battleOrder.Count;
        }
    }

    public CharacterData GetCurrentCharacterInBattleOrder() 
    {
        return battleOrder[currentBattleOrderIndex];
    }

    public CharacterData GetNextCharacterInBattleOrder()
    {   
        int nextIndex = currentBattleOrderIndex + 1;
        if (nextIndex >= battleOrder.Count) {
            nextIndex -= battleOrder.Count;
        }
        return battleOrder[nextIndex];
    }

    public void UpdateEnemySelectOptions() {
        // Update the individual Enemy Text Lines
        for (int i = 0; i < enemyTeam.Count; i++) {
            // If the enemy is still alive
            if (enemyTeam[i].GetHealthByKey("Current") > 0) {
                GetBattleUI().GetSelectEnemyBox().SetEnemyTextAtIndex(
                    i, 
                    enemyTeam[i].GetName(), 
                    ButtonGroupUI.TEXT_TYPE.ENABLED);
                GetBattleUI().GetSelectEnemyBox().SetEnemySelectAtIndexAsEnabled(i);
            }
            else {
                // If the enemy is dead
                GetBattleUI().GetSelectEnemyBox().SetEnemyTextAtIndex(
                    i, 
                    enemyTeam[i].GetName(), 
                    ButtonGroupUI.TEXT_TYPE.DISABLED);
                GetBattleUI().GetSelectEnemyBox().SetEnemySelectAtIndexAsEnabled(i);
            }
        }
    }

    // Protected

    // Private
    private void SetTeamInfo(
        Array<CharacterData> teamInfo,
        Array<CharacterData> inTeamInfo) 
    {
        // Clear the old teamInfo
        teamInfo.Clear();

        // Copy over the data
        for (int i = 0; i < inTeamInfo.Count; i++) {
            teamInfo.Add(inTeamInfo[i]);
            teamInfo[i].SetTeamIndex(i);
        }
    }

    private void SetTeamSprites(
        Array<CharacterData> teamData, 
        BattleSceneCharacter[] teamSprites)
    {
        for (int i = 0; i < teamData.Count; i++) {
            GD.Print($"Enemy Team Sprite: {i} | {teamData[i].GetBattleSprite()} | {teamSprites[i].Position}");
            teamSprites[i].Texture = teamData[i].GetBattleSprite();
        }
    }

    private void SetBattleOrder() 
    {
        // Init the battle order Array
        battleOrder.Clear();

        AddTeamToBattleOrder(playerTeam);
        AddTeamToBattleOrder(enemyTeam);

        // Debug
        GD.Print("\n");
        for (int i = 0; i < battleOrder.Count; i++) {
            GD.Print($"{i}: {battleOrder[i].GetName()}");
        }
    }

    private void AddTeamToBattleOrder(Array<CharacterData> teamData)
    {
        // Loop over the player team
        for (int i = 0; i < teamData.Count; i++) {
            // Roll initiative
            int initiativeVal = GetInitiative(teamData[i].GetStatByKey("DEX"));

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
        int battleOrderIndex = battleOrder.Count;

        // Go through the current battle order 
        for (int i = battleOrder.Count - 1; i > -1; i--) {
            // If the initiative value is less than the current position's value
            if (initiativeVal > battleOrder[i].GetInitiativeValue()) {
                battleOrderIndex = i;
            }
        }
        
        // Check if current battle pos is equal to the new initiative value
        if (battleOrderIndex != battleOrder.Count) {
            if (initiativeVal == battleOrder[battleOrderIndex].GetInitiativeValue()) {
                battleOrderIndex = DetermineWhichCharacterGoesFirst(data, battleOrderIndex);
            }
        }

        GD.Print($"{data.GetName()} roll: {initiativeVal}, index: {battleOrderIndex}");

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
                battleOrderIndex += 1;
            }
        }

        return battleOrderIndex;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
