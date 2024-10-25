using Godot;
using System;

public partial class AttackSequence : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PackedScene damageLabel;
    [Export] private BattleState playerTurn;
    private CharacterData attackerData;
    private CharacterData defenderData;
    private bool attackSequencnDone = false;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Log that the attack sequence is beggning
        attackSequencnDone = false;

        // Toggle the UI
        battleScene.HideUI();

        // Get the Source of the Attack's Character Data
        attackerData = GetAttackerData();

        // Get the Target of the Attack's Character Data
        defenderData = GetDefenderData();

        // Calculate the damage to deal
        int damage = GetDamage();

        // Deal damage to the enemy (weather NPC or Player)
        DealDamage(damage);

        // Update the UI (Utilize the tween)

        // Begin Animation that implies damage
        SceneTreeTimer timer = DisplayDamage(damage);

        // Set the attack sequence to exit once the damage label is done
        timer.Timeout += SetAnimationDone;
    }

    public override BattleState ProcessPhysics(float delta)
    {
        // Check if the attack is done
        if (!attackSequencnDone) {
            return null;
            
        }

        // If the defender is dead
        if (defenderData.currentHealth < 0) {
            return DefenderDead();
        }
        // If attack is done but the defender isn't dead
        else {
            return playerTurn;
        }
    }

    public virtual CharacterData GetAttackerData() 
    {return null;}   

    public virtual CharacterData GetDefenderData() 
    {return null;} 

    public virtual Node GetDefenderPos()
    {return null;}

    // Protected
    protected virtual BattleState DefenderDead() {return null;}

    // Private
    private int GetDamage() 
    {
        // Use the Main RNG seed to generate a random int value
        int damage = battleScene.main.rng.RandiRange(
            attackerData.damageRange.X, 
            attackerData.damageRange.Y);

        // Return said value
        return damage;
    }

    private void DealDamage(int damage)
    {
        defenderData.currentHealth -= damage;

        GD.Print($"Dealt Damage: {damage} | Remaining Damage: {defenderData.currentHealth}");
    }

    private SceneTreeTimer DisplayDamage(int value) 
    {
        // Instantiate the damage label
        DamageLabel damageLabelInst = (DamageLabel) damageLabel.Instantiate();

        // Add the label to the scene
        GetDefenderPos().AddChild(damageLabelInst);

        // Set the value
        return damageLabelInst.Init(value, battleScene.main.rng);
    }   

    private void SetAnimationDone()
    {
        attackSequencnDone = true;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
