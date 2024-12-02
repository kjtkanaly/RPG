using Godot;
using System;
using Godot.Collections;

public partial class SceneTransition : ColorRect
{
    public enum FadeDirection 
    {
        IN,
        OUT,
        Null
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    // [Export] public ColorRect colorRect;
    [Export] public float fadeTime = 0.5f;
    private Tween fadeTween;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public Tween SceneFade(FadeDirection inDirection) 
    {
        Color finish = new Color(0.0f, 0.0f, 0.0f);

        switch (inDirection) {
            case (FadeDirection.IN):
                this.Color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
                finish.A = 0.0f;
                break;
            case (FadeDirection.OUT):
                this.Color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                finish.A = 1.0f;
                break;
        }

        fadeTween = GetTree().CreateTween();
        fadeTween.TweenProperty(
            this, 
            "color", 
            finish, 
            fadeTime);
        fadeTween.SetPauseMode(Godot.Tween.TweenPauseMode.Process);

        return fadeTween;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TODO: Do some shit
