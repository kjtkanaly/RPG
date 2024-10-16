using Godot;
using System;

public partial class BattleState : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected
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

    public virtual BattleState ProcessPhysics(float delta)
    {
        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}