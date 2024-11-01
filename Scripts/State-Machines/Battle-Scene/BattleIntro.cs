using Godot;
using System;

public partial class BattleIntro : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleState playerTurnState;
    private bool introPlaying = false;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        introPlaying = true;
        IntroAnimation();
    }

    public override BattleState ProcessGeneral(float delta)
    {
        GD.Print(introPlaying);
        if (!introPlaying) {
            return playerTurnState;
        }

        return null;
    }

    // Protected

    // Private
    private async void IntroAnimation() 
    {
        string enemyName = battleScene.GetEnemyTeamDataAtIndex(0).GetName();
        string[] message = {
            $"{enemyName} approaches the player!"};

        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.dialogue,
            message,
            null);

        battleScene.main.GetMainUI().DispalyTextBox(data);

        await ToSignal(battleScene.main.GetMainUI(), MainUI.SignalName.DialogueOver);

        introPlaying = false;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}