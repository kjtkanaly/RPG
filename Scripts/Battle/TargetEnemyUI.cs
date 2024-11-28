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
        SetNewPosition();
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
        SetNewPosition();

        // Set the UI invisible till it is needed
        Visible = false;
    }

    public void NewTurn() 
    {
        BattleSceneCharacter character = battleScene.GetEnemyNodeAtIndex(currentIndex);

        GD.Print($"New Turn, Target Enemy {character.GetData().GetHealthByKey("Current")}");

        if (character.GetData().GetHealthByKey("Current") <= 0) {
            IncrementIndex(1);
            SetNewPosition();
        }

        Visible = true;
    }

    public int GetCurrentIndex() { return currentIndex; }

    public void SetNewPosition() 
    {  
        Vector2 pos = battleScene.GetEnemyNodeAtIndex(currentIndex).Position;
        Position = pos;
    }

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

            BattleSceneCharacter character = battleScene.GetEnemyNodeAtIndex(newIndex);

            if (!character.active
                || character.GetData().GetHealthByKey("Current") <= 0) {
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