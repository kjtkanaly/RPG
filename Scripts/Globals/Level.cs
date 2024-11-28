using Godot;
using System;
using Godot.Collections;

public partial class Level : Node2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Main main;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        // Get the global player stats object
        main = GetNode<Main>("/root/Main");

        Tween transitoinTween = main.GetMainUI().GetSceneTransition().SceneFade(
            SceneTransition.FadeDirection.IN);

        // await ToSignal(transitoinTween, Tween.SignalName.Finished);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
