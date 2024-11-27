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
    private BattleScenePlayerTeam playerTeamNode;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        GD.Print("Player Turn");

        // Set the UI elements visible
        battleScene.GetActionSelectionNode().Visible = true;
        battleScene.GetTargetEnemyUI().Visible = true;

        // // Set the character stats
        // battleScene.GetBattleUI().GetCharacterStats().SetData(
        //     battleScene.GetPlayerTeamDataAtIndex(0));

        // // Show the Player Stats
        // battleScene.GetBattleUI().ShowCharacterStats();
    }

    public override void Exit()
    {
        // Set the UI elements visible
        battleScene.GetActionSelectionNode().Visible = false;
        battleScene.GetTargetEnemyUI().Visible = false;
    }

    public override BattleState ProcessGeneral(float delta) 
    {
        // Init the return state as null;
        BattleState state = null;

        // Check if the Player has made a selection
        if (Input.IsActionJustReleased("Attack-UI")) {
            return playerAttackSequence;
        }
        else if (Input.IsActionJustReleased("Act-UI")) {
            return null;
        }
        else if (Input.IsActionJustReleased("Item-UI")) {
            return null;
        }
        else if (Input.IsActionJustReleased("Special-UI")) {
            return null;
        }
        
        return state;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}