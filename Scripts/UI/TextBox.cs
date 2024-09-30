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
        SetIcon(null);
    }

    private void ShowTextBox(string text, Texture2D icon)
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
    }

    private void SetIcon(Texture2D icon) {
        if (textRect == null) {
            return;
        }

        textRect.Texture = icon;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
