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
    [Export] private CameraDirector camera;
    private Main main;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        // Get the global player stats object
        main = GetNode<Main>("/root/Main");

        main.SetCurrentOverworld(this);

        Tween transitoinTween = main.GetMainUI().GetSceneTransition().SceneFade(
            SceneTransition.FadeDirection.IN);

        // await ToSignal(transitoinTween, Tween.SignalName.Finished);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public CameraDirector GetCamera() { return camera; }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
