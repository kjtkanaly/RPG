using Godot;
using System;

public partial class BattleSceneMainAction : ButtonGroupUI
{
    public enum Choice
    {
        Flee,
        Null
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private Button fleeButton;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public Choice GetActiveChoice()
    {
        // If the User Selected "Flee"
        if (buttons[currentButtonIndex] == fleeButton) {
            return Choice.Flee;
        }
        return Choice.Null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
