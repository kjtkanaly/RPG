using Godot;
using System;
using Godot.Collections;

public partial class CharacterInfo : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private TextureRect profilePic;
    [Export] private Label healthValue;
    [Export] private Label specialValue;
    [Export] private ProgressBar healthBar;
    [Export] private ProgressBar specialBar;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void SetProfilePic(Texture2D image)
    {
        profilePic.Texture = image;
    }

    public void SetHealthValue(int value) 
    {
        healthValue.Text = value.ToString();
    }

    public void SetSpecialValue(int value) 
    {
        specialValue.Text = value.ToString();
    }

    public void SetHealthBar(int current, int max=-1)
    {
        if (max>-1) {
            healthBar.MaxValue = max;
        }
        
        healthBar.Value = current;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
