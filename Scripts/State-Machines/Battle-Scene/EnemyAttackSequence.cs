using Godot;
using System;

public partial class EnemyAttackSequence : AttackSequence
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleState defeatState;
    [Export] private BattleState playerTurnState;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override CharacterData GetAttackerData()
    {
        return battleScene.GetEnemyTeamDataAtIndex(0);
    }

    public override CharacterData GetDefenderData()
    {
        return battleScene.GetPlayerTeamDataAtIndex(0);
    }

    public override Node2D GetDefenderPos()
    {
        return battleScene.GetPlayerPosAtIndex(0);
    }

    protected override BattleState GetNextTeamTurn() 
    {
        return playerTurnState;
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return defeatState;
    }

    protected override void DisplayDamage(int value) 
    {
        SetAnimationDone();
    }   

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}