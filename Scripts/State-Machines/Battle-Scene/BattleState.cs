using Godot;
using System;

public partial class BattleState : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
    [Export] protected BattleState playerTeamTurn;
    [Export] protected BattleState enemyTeamTurn;
    protected BattleScene battleScene;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public virtual void Init (BattleScene scene)
    {
        battleScene = scene;
    }

    public virtual void Enter() 
    {

    }

    public virtual void Exit () 
    {
        
    }

    public virtual BattleState ProcessInput(InputEvent inputEvent) 
    {
        return null;
    }

    public virtual BattleState ProcessGeneral(float delta)
    {
        return null;
    }

    public virtual BattleState ProcessPhysics(float delta)
    {
        return null;
    }

    // Protected
    protected BattleState GetNextTeamTurn() 
    {
        bool nextCharacterIsOnPlayerTeam = battleScene.IsInPlayerTeam(
            battleScene.GetNextCharacterInBattleOrder());

        battleScene.StepCurrentBattleOrderIndex();

        if (nextCharacterIsOnPlayerTeam) {
            return playerTeamTurn;
        }
        else {
            return enemyTeamTurn;
        }
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}