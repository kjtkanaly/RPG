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
        // Show the text box showing 1) the enemy team member's choice
        // and 2) the aount of damage done to the player

        // Construct the Text Msg
        string[] message = {
            $"{attackerData.GetName()} just dealt {damage.ToString()} damage to {GetDefenderData().GetName()}"};
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