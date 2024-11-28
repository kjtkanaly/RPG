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

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
