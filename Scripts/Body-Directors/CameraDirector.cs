using Godot;
using System;
using Godot.Collections;

public partial class CameraDirector : Camera2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private int zoomNormal = 3;
    [Export] private int zoomIn = 8;
    [Export] private float zoomDuration = 1.0f;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public Tween ZoomInOnCharacter()
    {
        Tween zoomTween = GetTree().CreateTween();
        zoomTween.TweenProperty(
            this, 
            "zoom", 
            zoomIn * Vector2.One, 
            zoomDuration);
        zoomTween.SetPauseMode(Godot.Tween.TweenPauseMode.Process);

        return zoomTween;
    }

    public Tween ZoomOutFromCharacter()
    {
        Tween zoomTween = GetTree().CreateTween();
        zoomTween.TweenProperty(
            this, 
            "zoom", 
            zoomNormal * Vector2.One, 
            zoomDuration);
        zoomTween.SetPauseMode(Godot.Tween.TweenPauseMode.Process);

        return zoomTween;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}