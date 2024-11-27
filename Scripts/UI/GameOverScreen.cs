using Godot;
using System;
using Godot.Collections;

public partial class GameOverScreen : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Left")) {
            IncrementSelectedButton(-1);
        } 
        else if (Input.IsActionJustPressed("Right")) {
            IncrementSelectedButton(1);
        }

        // Check Choice
        if (Input.IsActionJustReleased("Interact")) {
            ProcessChoice();
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void ProcessChoice() 
    {
        bool choice = GetSelectedButtonBoolValue();

        if (choice) {
            main.RestartFromCheckpoint();
        } else {
            main.QuitToMainMenu();
        }
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TODO: Do some shit
