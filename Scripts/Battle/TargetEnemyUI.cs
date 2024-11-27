using Godot;
using System;
using Godot.Collections;

public partial class TargetEnemyUI : Node2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private BattleSceneNew battleScene;
    private int currentIndex;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Left")) {
            IncrementIndex(-1);
        }
        else if (Input.IsActionJustPressed("Right")) {
            IncrementIndex(1);
        }

        // Update the Target UI Positon
        Vector2 pos = battleScene.GetEnemyNodeAtIndex(currentIndex).Position;
        Position = pos;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(BattleSceneNew scene) 
    {
        // Set the Battle Scene Node
        battleScene = scene;

        // Init the current Index
        currentIndex = 0;

        // Set the Target Enemy UI to the first enemy's Positon
        Vector2 pos = battleScene.GetEnemyNodeAtIndex(currentIndex).Position;
        Position = pos;

        // Set the UI invisible till it is needed
        Visible = false;
    }

    public int GetCurrentIndex() { return currentIndex; }

    // Protected

    // Private
    private void IncrementIndex(int step) 
    {
        int newIndex = currentIndex + step;

        while (true) {
            if (newIndex < 0) {
                newIndex = battleScene.GetEnemyNodes().Count - 1;
            }
            else if (newIndex >= battleScene.GetEnemyNodes().Count) {
                newIndex = 0;
            }

            if (!battleScene.GetEnemyNodeAtIndex(newIndex).active) {
                newIndex += step;
                continue;
            }

            break;
        }

        currentIndex = newIndex;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}