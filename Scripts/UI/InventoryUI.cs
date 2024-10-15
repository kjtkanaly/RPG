using Godot;
using System;

public partial class InventoryUI : ButtonGroupUI
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
        if (!Visible) {
            return;
        }

        // 
        if (Input.IsActionJustPressed("Down")) {
            IncrementSelectedButton(1);
        }
        if (Input.IsActionJustPressed("Up")) {
            IncrementSelectedButton(-1);
        }
        if (Input.IsActionJustPressed("Left")) {
            IncrementSelectedButton(-5);
        }
        if (Input.IsActionJustPressed("Right")) {
            IncrementSelectedButton(5);
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Toggle(Inventory inventory) 
    {
        if (!Visible) {
            RefreshUI(inventory);
        }

        Visible = !Visible;
        GetTree().Paused = !GetTree().Paused;

        GD.Print($"UI is visible: {Visible}");
    }

    public void RefreshUI(Inventory inventory) {
        for (int i = 0; i < inventory.GetInventorySize(); i++) {
            ItemData data = inventory.GetItem(i);

            if (data == null) {
                continue;
            }

            buttons[i].Text = inventory.GetItem(i).name;
        }
    }

    // Protected

    // Private
    

    //-------------------------------------------------------------------------
    // Debug Methods
}
