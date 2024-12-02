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

        if (battleScene.GetCurrentCharacter().GetData().GetHealthByKey("Current") <= 0) {
            return;
        }

        // Set the UI elements visible
        battleScene.GetActionSelectionNode().NewTurn(
            battleScene.GetCurrentCharacter());

        // Init the Enemy Target
        battleScene.GetTargetEnemyUI().NewTurn();
    }

    public override void Exit()
    {
        // Set the UI elements visible
        battleScene.GetActionSelectionNode().Visible = false;
        battleScene.GetTargetEnemyUI().Visible = false;
    }

    public override BattleState ProcessGeneral(float delta) 
    {
        if (battleScene.GetCurrentCharacter().GetData().GetHealthByKey("Current") <= 0) {
            return GetNextTeamTurn();
        }

        // Init the return state as null;
        BattleState state = null;

        // Check if the Player has made a selection
        if (Input.IsActionJustReleased("Attack-UI")) {
            if (CanAttackEnemy()) {
                return playerAttackSequence;
            }
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
    private bool CanAttackEnemy() 
    {   
        // Get the defender index
        int defenderIndex = battleScene.GetTargetEnemyUI().GetCurrentIndex(); 

        if (battleScene.GetEnemyNodes()[defenderIndex].GetData().GetHealthByKey("Current") <= 0) {
            return false;
        }

        return true;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}