using Godot;
using System;

public partial class MainMenu : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PackedScene gameStartPacked;
    [Export] private PackedScene optionsUIPacked;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("Interact")) {
            ProcessUserChoice();
        }

        if (Input.IsActionJustPressed("Down")) {
            IncrementSelectedButton(1);
        }

        if (Input.IsActionJustPressed("Up")) {
            IncrementSelectedButton(-1);
        }
    }

    // Protected

    // Private
    private void ProcessUserChoice()
    {
        int selectedValue = GetSelectedButtonIntValue();

        switch (selectedValue){
            case 0:
                GoToGameStart();
                break;
            case 1:
                GoToOptions();
                break;
            case 2:
                QuitGame();
                break;
            default:
                GD.PushError("Unknown Main Menu Selection");
                break;
        }
    }

    private void GoToGameStart()
    {
        main.SwitchToPackedScene(gameStartPacked);
    }

    private void GoToOptions() 
    {
        main.SwitchToPackedScene(optionsUIPacked);

    }

    private void QuitGame() 
    {
        GetTree().Quit();
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
