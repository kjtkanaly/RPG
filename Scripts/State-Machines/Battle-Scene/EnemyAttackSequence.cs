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

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    // public override void Init(BattleSceneNew scene)
    // {
    //     base.Init(scene);

    //     battleScene.GetCurrentCharacter().GetAnimationPlayer().AnimationFinished += SetAnimationDone;
    // }

    public override CharacterData GetDefenderData()
    {
        return battleScene.GetPlayerNodeAtIndex(defenderIndex).GetData();
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return defeatState;
    }

    protected override bool AllDefendersAreDead() 
    {
        for (int i = 0; i < battleScene.GetPlayerNodes().Count; i++) {
            if (!battleScene.GetPlayerNodes()[i].active) {
                continue;
            }

            if (battleScene.GetPlayerNodes()[i].GetData().GetHealthByKey("Current") > 0) {
                return false;
            }
        }
        return true;
    }

    protected override void DisplayDamage(int damage) 
    {
        // Update the player node's UI
        battleScene.GetBattleSceneUI().UpdateInfoByCharacter(
            battleScene.GetPlayerNodeAtIndex(defenderIndex));

        // Do an animation
        battleScene.GetCurrentCharacter().PlayAttackAnimation();
        
        // Instantiate the damage label
        DamageLabel damageLabelInst = (DamageLabel) damageLabel.Instantiate();

        // Add the label to the scene
        battleScene.GetPlayerNodeAtIndex(defenderIndex).AddChild(damageLabelInst);

        // Init the damage label
        damageLabelInst.Init(damage, battleScene.main.rng);
    }   

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}