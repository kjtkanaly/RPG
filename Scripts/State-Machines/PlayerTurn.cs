using Godot;
using System;

public partial class PlayerTurn : BattleState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private BattleSceneMainAction mainAction;
    [Export] private BattleState fleeBattle;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public override BattleState ProcessPhysics(float delta) 
    {
        // Shuffle through user selection
        if (Input.IsActionJustPressed("Down")) {
            mainAction.IncrementSelectedButton(1);
        }
        else if (Input.IsActionJustReleased("Up")) {
            mainAction.IncrementSelectedButton(-1);
        }

        // Check if the Player has made a selection
        if (Input.IsActionJustPressed("Interact")) {
            BattleSceneMainAction.Choice choice = mainAction.GetActiveChoice();

            return ProcessChoice(choice);
        }

        return null;
    }

    // Protected

    // Private
    private BattleState ProcessChoice(BattleSceneMainAction.Choice choice) 
    {
        switch (choice) {
            case(BattleSceneMainAction.Choice.Flee):
                return fleeBattle;
            default:
                return null;
        }
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}