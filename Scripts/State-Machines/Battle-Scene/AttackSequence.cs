using Godot;
using System;

public partial class AttackSequence : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    [Export] protected PackedScene damageLabel;
    [Export] protected AudioStreamPlayer2D audioPlayer;
    [Export] protected AudioStream soundEffect;
    protected bool attackSequencnDone = false;
    protected int attackerIndex = -1;
    protected int defenderIndex = -1;

    // Private
    private CharacterData attackerData;
    private CharacterData defenderData;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Log that the attack sequence is beggning
        attackSequencnDone = false;

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
        DisplayDamage(damage);

        // Set the attack sequence to exit once the damage label is done
        
    }

    public override BattleState ProcessGeneral(float delta)
    {
        // Check if the attack is done
        if (!attackSequencnDone) {
            return null;
        }

        // If the defender is dead
        if (GetDefenderData().GetHealthByKey("Current") <= 0) {
            IndicateDefenderDead();
        }

        // If all defenders is dead
        if (AllDefendersAreDead()) {
            return DefenderDead();
        }

        // If attack is done but the defender isn't dead
        BattleState nextState = GetNextTeamTurn();
        return nextState;
    }

    public virtual CharacterData GetAttackerData() 
    {return null;}   

    public virtual CharacterData GetDefenderData() 
    {return null;} 

    public virtual BattleSceneCharacter GetDefenderNode()
    {return null;}

    public void SetAttackerIndex(int inIndex) { attackerIndex = inIndex; }
    public void SetDefenderIndex(int inIndex) { defenderIndex = inIndex; }

    // Protected
    protected virtual BattleState DefenderDead() {return null;}

    protected virtual bool AllDefendersAreDead() { return false; }

    protected virtual void IndicateDefenderDead() { }

    protected virtual void DisplayDamage(int damage) {}

    protected void SetAnimationDone()
    {
        attackSequencnDone = true;
    }

    // Private
    private int GetDamage() 
    {
        // Use the Main RNG seed to generate a random int value
        int damage = battleScene.main.rng.RandiRange(
            attackerData.GetDamageRange().X, 
            attackerData.GetDamageRange().Y);

        // Return said value
        return damage;
    }

    private void DealDamage(int damage)
    {
        defenderData.IterateCurrentHealth(-1 * damage);

        GD.Print($"Dealt Damage: {damage} | Remaining Damage: {defenderData.GetHealthByKey("Current")}");
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
