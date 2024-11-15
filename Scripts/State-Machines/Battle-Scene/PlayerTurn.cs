using Godot;
using System;

public partial class PlayerTurn : BattleState
{
    public enum PHASE
    {
        MAIN_ACTION,
        ENEMY_SELECT
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleState fleeBattle;
    [Export] private BattleState playerAttackSequence;
    private PHASE phase = PHASE.MAIN_ACTION;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Set the character stats
        battleScene.GetBattleUI().GetCharacterStats().SetData(
            battleScene.GetPlayerTeamDataAtIndex(0));

        battleScene.GetBattleUI().ShowMainAction();
        battleScene.GetBattleUI().ShowCharacterStats();

        phase = PHASE.MAIN_ACTION;
    }

    public override void Exit()
    {
        // Toggle the UI
        battleScene.GetBattleUI().HideMainAction();
        battleScene.GetBattleUI().HideCharacterStats();
        battleScene.GetBattleUI().HideSelectEnemyBox();
    }

    public override BattleState ProcessGeneral(float delta) 
    {
        // Init the return state as null;
        BattleState state = null;

        // Check if the Player has made a selection
        if (Input.IsActionJustPressed("Interact")) {
            if (phase == PHASE.MAIN_ACTION) {
                int mainChoice = battleScene.GetBattleUI().GetMainAction().GetSelectedButtonIntValue();
                state = ProcessMainChoice(mainChoice);
            }
            else if(phase == PHASE.ENEMY_SELECT) {
                int selectedEnemyIndex = battleScene.GetBattleUI().GetSelectEnemyBox().GetPressedButtonIndex();
                state = ProcessEnemySelect(selectedEnemyIndex);
            }
        }
        
        return state;
    }

    // Protected

    // Private
    private BattleState ProcessMainChoice(int choice) 
    {
        switch (choice) {
            // The Player chose to attack
            case(0):
                battleScene.GetBattleUI().HideMainAction();
                battleScene.GetBattleUI().ShowSelectEnemyBox();
                phase = PHASE.ENEMY_SELECT;
                return null;
            case(1):
                return null;
            case(2):
                return null;
            // The player chose to flee
            case(3):
                return fleeBattle;
            default:
                return null;
        }
    }

    private BattleState ProcessEnemySelect(int index) {
        ((AttackSequence) playerAttackSequence).SetDefenderIndex(index);
        return playerAttackSequence;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}