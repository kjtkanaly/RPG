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
    [Export] private bool debugMode;
    [Export] private Label label;
    [Export] private TextureRect textRect;
    [Export] private Button[] buttonGroup;
    private int currentButtonIndex = 0;

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

        // ButtonInteractionStuff
        if (Input.IsActionJustPressed("Left")) {
            CycleSelectedButton(-1);
        }
        else if (Input.IsActionJustPressed("Right")) {
            CycleSelectedButton(1);
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void ShowTextBox(string text, Texture2D icon)
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
    }

    // Protected

    // Private
    private void HideTextBox() 
    {
        Visible = false;
        label.Text = "";
        SetIcon(null);
    }

    private void SetIcon(Texture2D icon) {
        if (textRect == null) {
            return;
        }

        textRect.Texture = icon;
    }

    private void CycleSelectedButton(int step)
    {
        int newButtonIndex = currentButtonIndex + step;

        // Bounds Check
        if ((newButtonIndex < 0) || (newButtonIndex >= buttonGroup.Length)) 
        {
            return;
        }

        currentButtonIndex = newButtonIndex;
        buttonGroup[currentButtonIndex].ButtonPressed = true;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
