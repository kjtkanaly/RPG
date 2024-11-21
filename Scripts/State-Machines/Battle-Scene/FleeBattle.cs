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
        battleScene.LeaveBattle(true);
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}