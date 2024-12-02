using Godot;
using System;

public partial class FleeBattle : BattleState
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
    public override void Enter()
    {
        battleScene.LeaveBattle(BattleSceneNew.END_STATE.FLEE);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}