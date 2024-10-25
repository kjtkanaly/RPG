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
        battleScene.ShowUI();
    }

    public override BattleState ProcessPhysics(float delta) 
    {
        // Shuffle through user selection
        if (Input.IsActionJustPressed("Down")) {
            // battleScene.GetBattleUI().GetMainAction().IncrementSelectedButton(1);
        }
        else if (Input.IsActionJustReleased("Up")) {
            // battleScene.GetBattleUI().GetMainAction().IncrementSelectedButton(-1);
        }

        // Check if the Player has made a selection
        if (Input.IsActionJustPressed("Interact")) {
            // BattleScene.Choice choice = 
            //    battleScene.GetBattleUI().GetMainAction().GetActiveChoice();

            // return ProcessChoice(choice);
        }

        return null;
    }

    // Protected

    // Private
    private BattleState ProcessChoice(BattleScene.Choice choice) 
    {
        switch (choice) {
            case(BattleScene.Choice.Flee):
                return fleeBattle;
            case(BattleScene.Choice.Attack):
                return playerAttackSequence;
            default:
                return null;
        }
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}