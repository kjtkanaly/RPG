using Godot;
using System;

public partial class BattleSceneMainAction : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Main main;
    [Export] private Button fleeButton;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        main = GetNode<Main>("/root/Main");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Down")) {
            IncrementSelectedButton(1);
        }
        else if (Input.IsActionJustReleased("Up")) {
            IncrementSelectedButton(-1);
        }

        // Check if the player made a selection
        if (Input.IsActionJustReleased("Interact")) {
            CheckUserSelection();
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public

    // Protected

    // Private
    private void CheckUserSelection()
    {
        // If the User Selected "Flee"
        if (buttons[currentButtonIndex] == fleeButton) {
            main.ExitBattle();
        }
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
