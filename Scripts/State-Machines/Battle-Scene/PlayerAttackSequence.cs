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
    public override CharacterData GetDefenderData()
    {
        return battleScene.GetEnemyNodeAtIndex(defenderIndex).GetData();
    }

    public override BattleSceneEnemy GetDefenderNode()
    {
        return null; // battleScene.GetEnemyTeamBattleNodes().GetEnemyNodeAtIndex(defenderIndex);
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return victoryState;
    }

    protected override bool AllDefendersAreDead() 
    {
        for (int i = 0; i < battleScene.GetEnemyNodes().Count; i++) {
            if (!battleScene.GetEnemyNodes()[i].active) {
                continue;
            }

            if (battleScene.GetEnemyNodes()[i].GetData().GetHealthByKey("Current") > 0) {
                return false;
            }
        }
        return true;
    }

    protected override void IndicateDefenderDead() 
    {
        Color defenderColor = battleScene.GetEnemyNodeAtIndex(defenderIndex).Modulate;
        defenderColor.V = 0.5f;
        battleScene.GetEnemyNodeAtIndex(defenderIndex).Modulate = defenderColor;
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

    protected override int GetDefenderIndex() 
    { 
        return battleScene.GetTargetEnemyUI().GetCurrentIndex(); 
    }

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
