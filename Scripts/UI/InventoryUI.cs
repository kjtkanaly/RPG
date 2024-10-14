using Godot;
using System;

public partial class InventoryUI : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public int inventorySize;

    // Protected

    // Private
    [Export] private Inventory inventory;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("Inventory")) {
            Toggle();
        }

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
    public void Init(int inventorySizeValue)
    {
        // Log the max 
        inventorySize = inventorySizeValue;

        // Attach the UI's refresh method to the inveotry's Added Item Signal
        inventory.AddedItem += Refresh;
    }

    public void Refresh()
    {
        for (int i = 0; i < inventorySize; i++) {
            buttons[i].Text = inventory.GetItem(i).name;
        }
    }

    // Protected

    // Private
    private void Toggle() 
    {
        Visible = !Visible;
        GetTree().Paused = !GetTree().Paused;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
