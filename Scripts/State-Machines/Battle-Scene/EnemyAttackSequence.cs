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

    protected override BattleState GetNextTeamTurn() 
    {
        return playerTurnState;
    }

    // Protected
    protected override BattleState DefenderDead() 
    {
        return defeatState;
    }

    protected override void DisplayDamage(int damage) 
    {
        // Show the text box showing 1) the enemy team member's choice
        // and 2) the aount of damage done to the player

        // Construct the Text Msg
        string[] message = {
            $"{GetAttackerData().GetName()} just dealt {damage.ToString()} damage to {GetDefenderData().GetName()}"};
        TextBox.TextBoxData textBoxData = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.dialogue,
            message,
            null);

        // Display the message in the text box
        battleScene.main.GetMainUI().DispalyTextBox(textBoxData);

        // Set the enemy attack sequence to end once the dialog has finished
        battleScene.main.GetMainUI().DialogueOver += SetAnimationDone;

        // TODO: Also do some animation for the damage

    }   

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}