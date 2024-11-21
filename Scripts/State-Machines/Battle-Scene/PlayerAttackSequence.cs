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
    // Change this to the Enemy Team Turn State

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
        return battleScene.GetEnemyTeamDataAtIndex(defenderIndex);
    }

    public override BattleSceneCharacter GetDefenderNode()
    {
        return battleScene.GetEnemyNodeAtIndex(defenderIndex);
    }

    public override bool AllDefendersAreDead() 
    {
        for (int i = 0; i < battleScene.GetEnemyTeamData().Count; i++) {
            if (battleScene.GetEnemyTeamData()[i].GetHealthByKey("Current") > 0) {
                return false;
            }
        }
        return true;
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return victoryState;
    }

    protected override void DisplayDamage(int damage) 
    {
        // Instantiate the damage label
        DamageLabel damageLabelInst = (DamageLabel) damageLabel.Instantiate();

        // Add the label to the scene
        GetDefenderNode().AddChild(damageLabelInst);
        damageLabelInst.Position = new Vector2(75, 75);;

        SceneTreeTimer timer = damageLabelInst.Init(damage, battleScene.main.rng);

        timer.Timeout += SetAnimationDone;

        // Play the hit sound effect
        audioPlayer.Stream = soundEffect;
        audioPlayer.Play();
    }   

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
