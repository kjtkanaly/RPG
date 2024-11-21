using Godot;
using System;

public partial class EnemyTeamTurn : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleState enemyAttackSequence;
    private bool choiceMade = false;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Decide if the enemy is gonna attack or dome something else
        choiceMade = true;
    }

    public override BattleState ProcessGeneral(float delta)
    {
        if (!choiceMade) {
            return null;
        }

        // If the enemy team member is dead
        GD.Print($"Checking Enemy Memeber's Health: {battleScene.GetCurrentCharacterInBattleOrder().GetHealthByKey("Current")}");
        if (battleScene.GetCurrentCharacterInBattleOrder().GetHealthByKey("Current") <= 0) {
            BattleState nextState = GetNextTeamTurn();
            return nextState;
        }
        
        // Assume for now that the enemy will only attack
        // TODO: setup system and logic for enemy deciding to attack or other actions
        return enemyAttackSequence;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}