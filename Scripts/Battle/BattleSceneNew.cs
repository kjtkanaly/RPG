using Godot;
using System;
using Godot.Collections;

public partial class BattleSceneNew : Node2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public Main main;

    // Protected

    // Private
    [Export] private BattleStateMachine stateMachine;
    [Export] private Array<BattleSceneCharacter> enemyNodes;
    [Export] private Array<BattleSceneCharacter> playerNodes;
    [Export] private TargetEnemyUI targetEnemyUI;
    [Export] private BattleSceneUI battleSceneUI;
    [Export] private ActionSelectionNode actionSelectionNode;
    private Array<BattleSceneCharacter> battleOrder;
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
    public int GetCurrentBattleOrderIndex() { return currentBattleOrderIndex; }

    public void SetCurrentBattleOrderIndex(int val) { currentBattleOrderIndex = val; }

    public BattleSceneCharacter GetCurrentCharacter() { return battleOrder[currentBattleOrderIndex]; }

    public Array<BattleSceneCharacter> GetBattleOrder() { return battleOrder; } 

    public Array<BattleSceneCharacter> GetEnemyNodes() { return enemyNodes; }

    public Array<BattleSceneCharacter> GetPlayerNodes() { return playerNodes; }

    public void LeaveBattle(bool playerVictory) 
    {
        Array<CharacterData> playerTeamData = new Array<CharacterData>();
        for (int i = 0; i < playerNodes.Count; i++) {
            if (!playerNodes[i].active) {
                continue;
            }
            playerTeamData.Add(playerNodes[i].GetData());
        }

        Array<CharacterData> enemyTeamData = new Array<CharacterData>();
        for (int i = 0; i < enemyNodes.Count; i++) {
            if (!enemyNodes[i].active) {
                continue;
            }
            enemyTeamData.Add(enemyNodes[i].GetData());
        }

        main.EndBattle(playerTeamData, enemyTeamData, playerVictory);
    }

    public BattleSceneCharacter GetEnemyNodeAtIndex(int index) { return enemyNodes[index]; }

    public BattleSceneCharacter GetPlayerNodeAtIndex(int index) { return playerNodes[index]; }

    public TargetEnemyUI GetTargetEnemyUI() { return targetEnemyUI; }

    public ActionSelectionNode GetActionSelectionNode() { return actionSelectionNode; }

    // Protected

    // Private
    private void Init() 
    {
        // Get the Main Node
        main = GetNode<Main>("/root/Main");

        // Get the Battle Info
        BattleQueue battleQueue = main.GetBattleQueue();

        // Set the Player Team Info
        SetTeamInfo(playerNodes, battleQueue.GetPlayerTeam());

        // Set the Enemy Team Info
        SetTeamInfo(enemyNodes, battleQueue.GetEnemyTeam());

        // Determine the battle order
        SetBattleOrder();

        // Reset the current battle order index
        currentBattleOrderIndex = 0;

        // Init the Battle Scene UI
        battleSceneUI.Init(playerNodes);

        // Init the Target Enemy UI
        targetEnemyUI.Init(this);

        // Init the Action Selection UI
        actionSelectionNode.Init(this);

        // Init the Battle Scene State Machine
        stateMachine.Init(this);
    }

    private void SetTeamInfo(
        Array<BattleSceneCharacter> teamNodes,
        Array<CharacterData> teamInfo) 
    {
        // Copy over the data
        for (int i = 0; i < teamInfo.Count; i++) {
            teamNodes[i].Init(teamInfo[i]);
        }
    }

    private void SetBattleOrder() 
    {
        // Init the battle order Array
        battleOrder = new Array<BattleSceneCharacter>();

        AddTeamToBattleOrder(playerNodes);
        AddTeamToBattleOrder(enemyNodes);

        // Debug
        GD.Print("\n");
        for (int i = 0; i < battleOrder.Count; i++) {
            GD.Print($"{i}: {battleOrder[i].GetData().GetName()}");
        }
    }

    private void AddTeamToBattleOrder(Array<BattleSceneCharacter> teamNodes)
    {
        // Loop over the player team
        for (int i = 0; i < teamNodes.Count; i++) {
            // Check if the battle node is active
            if (!teamNodes[i].active) {
                continue;
            }

            // Set position in the battle order
            AppendBattleOrder(teamNodes[i]);
        }
    }

    private void AppendBattleOrder(BattleSceneCharacter character)
    {
        int battleOrderIndex = battleOrder.Count;

        // Go through the current battle order 
        for (int i = battleOrder.Count - 1; i > -1; i--) {
            // If the initiative value is less than the current position's value
            if (character.GetInitiativeRoll() > battleOrder[i].GetInitiativeRoll()) {
                battleOrderIndex = i;
            }
        }
        
        // Check if current battle pos is equal to the new initiative value
        if (battleOrderIndex != battleOrder.Count) {
            if (character.GetInitiativeRoll() == battleOrder[battleOrderIndex].GetInitiativeRoll()) {
                battleOrderIndex = DetermineWhichCharacterGoesFirst(character, battleOrderIndex);
            }
        }

        GD.Print($"{character.GetData().GetName()} roll: {character.GetInitiativeRoll()}, index: {battleOrderIndex}");

        // Insert the position and log it's initiative value
        character.SetBattleIndex(battleOrderIndex);
        battleOrder.Insert(battleOrderIndex, character);
    }

    // Used in scenario's where two character's roll equal initiative
    private int DetermineWhichCharacterGoesFirst(
        BattleSceneCharacter character, 
        int battleOrderIndex)
    {
        // Check if the character at the current index has higher dex value
        if (character.GetData().GetStatByKey("DEX") 
            < battleOrder[battleOrderIndex].GetData().GetStatByKey("DEX")) {
            battleOrderIndex -= 1;
        }
        // Check both characters have equal data
        else if (character.GetData().GetStatByKey("DEX") 
            == battleOrder[battleOrderIndex].GetData().GetStatByKey("DEX")) {
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
