using Godot;
using System;
using Godot.Collections;

public partial class WaitingForPlayer : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private CharacterBodyState followPlayer;
    private CharacterDirector player;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        player = (CharacterDirector) GetTree().GetNodesInGroup("Player")[0];
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override State ProcessPhysics(float delta)
    {
        if ((player.Position - characterDirector.Position).Length() > 40f) {
            return followPlayer;
        }

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}