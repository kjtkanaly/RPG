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
    [Export] private BattleState enemyAttackSequence;

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

    public override Node2D GetDefenderPos()
    {
        return battleScene.GetEnemyPosAtIndex(0);
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return victoryState;
    }

    protected override BattleState GetNextTeamTurn() 
    {
        return enemyAttackSequence;
    }

    protected override void DisplayDamage(int value) 
    {
        // Instantiate the damage label
        DamageLabel damageLabelInst = (DamageLabel) damageLabel.Instantiate();

        // Add the label to the scene
        GetDefenderPos().AddChild(damageLabelInst);

        SceneTreeTimer timer = damageLabelInst.Init(value, battleScene.main.rng);

        timer.Timeout += SetAnimationDone;
    }   

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
