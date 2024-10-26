using Godot;
using System;

public partial class PlayerTurn : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleState fleeBattle;
    [Export] private BattleState playerAttackSequence;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        battleScene.GetBattleUI().ShowMainAction();
    }

    public override void Exit()
    {
        // Toggle the UI
        battleScene.GetBattleUI().HideMainAction();
    }

    public override BattleState ProcessGeneral(float delta) 
    {
        // Check if the Player has made a selection
        if (Input.IsActionJustPressed("Interact")) {
            return ProcessChoice(
                battleScene.GetBattleUI().GetMainAction().GetSelectedButtonIntValue());
        }

        return null;
    }

    // Protected

    // Private
    private BattleState ProcessChoice(int choice) 
    {
        switch (choice) {
            case(0):
                return playerAttackSequence;
            case(1):
                return null;
            case(2):
                return null;
            case(3):
                return fleeBattle;
            default:
                return null;
        }
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}