using Godot;
using System;

public partial class TextBox : CanvasLayer
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public float charactersPerSecond = 0.05f;
    [Export] public string testText;
    [Export] public Texture2D testImage;

    // Protected

    // Private
    [Export] private Label label;
    [Export] private TextureRect textRect;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        HideTextBox();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Toggle Text Box")) {
            if (!Visible) {
                ShowTextBox(testText, testImage);
            }
            else {
                HideTextBox();
            }
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void HideTextBox() 
    {
        Visible = false;
        label.Text = "";
        textRect.Texture = null;
    }

    private void ShowTextBox(string text, Texture2D icon)
    {
        Visible = true;
        label.Text = text;
        textRect.Texture = icon;

        // Animate the Text
        label.VisibleRatio = 0;
        Tween textTween = GetTree().CreateTween();
        textTween.TweenProperty(
            label, 
            "visible_ratio", 
            1, 
            label.Text.Length * charactersPerSecond);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}