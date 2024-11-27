using Godot;
using System;

public partial class BattleIntro : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private bool introPlaying = false;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {

    }

    public override BattleState ProcessGeneral(float delta)
    {
        if (!introPlaying) {
            return GetFirstTurn();
        }

        return null;
    }

    // Protected

    // Private
    private BattleState GetFirstTurn()
    {
        // DEBUG
        return playerTeamTurn;

        GD.Print($"First Character: {battleScene.GetCurrentCharacter().GetData().GetName()}");

        if (battleScene.GetCurrentCharacter().IsOnPlayerTeam()) {
            return playerTeamTurn;
        }
        else {
            return enemyTeamTurn;
        }
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
