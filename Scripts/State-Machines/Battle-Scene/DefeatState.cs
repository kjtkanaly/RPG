using Godot;
using System;
using Godot.Collections;

public partial class DefeatState : BattleState
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
        string[] message = {"Defeat..."};

        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.dialogue,
            message,
            null);

        battleScene.main.GetMainUI().DispalyTextBox(data);

        await ToSignal(battleScene.main.GetMainUI(), MainUI.SignalName.DialogueOver);

        battleScene.LeaveBattle(BattleSceneNew.END_STATE.ENEMY_VICTORY);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TODO: Do some shit