using Godot;
using System;

public partial class BattleSceneMainAction : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private Button attackButton;
    [Export] private Button fleeButton;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public BattleScene.Choice GetActiveChoice()
    {
        // If the User Selected "Flee"
        if (buttons[currentButtonIndex] == fleeButton) {
            return BattleScene.Choice.Flee;
        }
        // If the User Selected "Attack"
        else if (buttons[currentButtonIndex] == attackButton) {
            return BattleScene.Choice.Attack;
        }
        
        return BattleScene.Choice.Null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
