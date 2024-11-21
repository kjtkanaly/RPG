using Godot;
using System;
using Godot.Collections;

public partial class SelectEnemyBox : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private Label[] indicatorLabels = new Label[3];
    [Export] private Label[] enemyNameLabels = new Label[3];
    int previousIndex = 0;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        previousIndex = currentButtonIndex;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Down")) {
            IncrementSelectedButton(1);
        }
        else if (Input.IsActionJustPressed("Up")) {
            IncrementSelectedButton(-1);
        }
        UpdateLabels();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void SetEnemySelectAtIndexAsEnabled(int index) 
    {
        buttons[index].Disabled = false;
    }

    public void SetEnemySelectAtIndexAsDisabled(int index)
    {
        buttons[index].Disabled = true;
    }

    public void SetEnemyTextAtIndex(int index, string text, TEXT_TYPE textType) 
    {
        enemyNameLabels[index].Text = text;

        if (textType == TEXT_TYPE.DISABLED) {
            Color c = enemyNameLabels[index].Modulate;
            c.V = 0.5f;
            enemyNameLabels[index].Modulate = c;
        }
    }

    // Protected

    // Private
    private void UpdateLabels() 
    {
        int currentIndex = GetPressedButtonIndex();

        if (currentIndex == previousIndex) {
            return;
        }

        indicatorLabels[currentIndex].Text = ">";
        indicatorLabels[previousIndex].Text = " ";

        previousIndex = currentIndex;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
