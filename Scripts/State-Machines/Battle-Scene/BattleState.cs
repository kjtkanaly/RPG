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
    protected BattleSceneNew battleScene;

    // Private

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public virtual void Init (BattleSceneNew scene)
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
        int nextBattleIndex = battleScene.GetCurrentBattleOrderIndex() + 1;
        if (nextBattleIndex >= battleScene.GetBattleOrder().Count) {
            nextBattleIndex -= battleScene.GetBattleOrder().Count;
        }

        // Increment the current battle order index
        battleScene.SetCurrentBattleOrderIndex(nextBattleIndex);

        if (battleScene.GetBattleOrder()[nextBattleIndex].IsOnPlayerTeam()) {
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