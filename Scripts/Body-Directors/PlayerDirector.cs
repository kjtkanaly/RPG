using Godot;
using System;
using Godot.Collections;

public partial class PlayerDirector : CharacterDirector
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Array<CharacterDirector> team = new Array<CharacterDirector>();

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        team.Add(this);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override Array<CharacterDirector> GetTeam() { return team; }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
