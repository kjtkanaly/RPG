using Godot;
using System;

public partial class TextBox : CanvasLayer
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public float charactersPerSecond = 0.05f;
    public string testText = "The quick brown fox jumps over the lazy dog";
    [Export] public Texture2D testImage;

    // Protected
    [Export] protected bool debugMode;
    [Export] protected Label label;
    [Export] protected TextureRect textRect;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        HideTextBox();
    }

    public override void _Process(double delta)
    {   
        // Debug Stuff
        if (Input.IsActionJustPressed("Toggle Text Box") && debugMode) {
            if (!Visible) {
                ShowTextBox(testText, testImage);
            }
            else {
                HideTextBox();
            }
        }

        // If hiden then don't check for input
        if (!Visible) {
            return;
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public Tween ShowTextBox(string text, Texture2D icon)
    {
        Visible = true;
        label.Text = text;
        SetIcon(icon);

        // Animate the Text
        label.VisibleRatio = 0;
        Tween textTween = GetTree().CreateTween();
        textTween.TweenProperty(
            label, 
            "visible_ratio", 
            1, 
            label.Text.Length * charactersPerSecond);
        textTween.SetPauseMode(Godot.Tween.TweenPauseMode.Process);

        return textTween;
    }

    public void HideTextBox() 
    {
        Visible = false;
        label.Text = "";
        SetIcon(null);
    }

    // Protected
    protected void SetIcon(Texture2D icon) {
        if (textRect == null) {
            return;
        }

        textRect.Texture = icon;
    }

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
