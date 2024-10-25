using Godot;
using System;

public partial class VictoryState : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public async override void Enter()
    {
        string[] message = {"Victory!!!"};

        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.dialogue,
            message,
            null);

        battleScene.main.GetMainUI().DispalyTextBox(data);

        await ToSignal(battleScene.main.GetMainUI(), MainUI.SignalName.DialogueOver);

        battleScene.LeaveBattle();
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}