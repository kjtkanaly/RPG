using Godot;
using System;

public partial class PlayerAttackSequence : AttackSequence
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleState victoryState;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override CharacterData GetAttackerData()
    {
        return battleScene.GetPlayerTeamDataAtIndex(0);
    }

    public override CharacterData GetDefenderData()
    {
        return battleScene.GetEnemyTeamDataAtIndex(0);
    }

    public override Control GetDefenderPos()
    {
        return battleScene.GetBattleUI().GetDamagePos(
            BattleSceneUI.CHARACTER_TYPE.Enemy);
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return victoryState;
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
