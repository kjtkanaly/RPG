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
    protected int defenderIndex = -1;
    protected CharacterData attackerData;
    protected CharacterData defenderData;

    // Private

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        // Log the Defender's Index
        defenderIndex = GetDefenderIndex();
        GD.Print($"Target Index: {defenderIndex}");

        // // Log that the attack sequence is beggning
        attackSequencnDone = false;

        // Get the Attacker and Defender Data
        attackerData = battleScene.GetCurrentCharacter().GetData();
        defenderData = GetDefenderData();

        // Calculate the damage to deal
        int damage = GetDamage();

        // Deal damage to the enemy (weather NPC or Player)
        DealDamage(damage);

        // // Update the UI (Utilize the tween)

        // // Begin Animation that implies damage
        DisplayDamage(damage);

        // // Set the attack sequence to exit once the damage label is done
        
    }

    public override BattleState ProcessGeneral(float delta)
    {
        // Check if the attack is done
        if (!attackSequencnDone) {
            return null;
        }

        // If the defender is dead
        if (defenderData.GetHealthByKey("Current") <= 0) {
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

    public virtual CharacterData GetDefenderData() { return null; } 

    public virtual BattleSceneEnemy GetDefenderNode()
    {return null;}

    public void SetDefenderIndex(int inIndex) { defenderIndex = inIndex; }

    // Protected
    protected virtual BattleState DefenderDead() {return null;}

    protected virtual bool AllDefendersAreDead() { return false; }

    protected virtual void IndicateDefenderDead() { }

    protected virtual void DisplayDamage(int damage) {}

    protected void SetAnimationDone(StringName name)
    {
        attackSequencnDone = true;
    }

    protected virtual int GetDefenderIndex() { return 0; }

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
