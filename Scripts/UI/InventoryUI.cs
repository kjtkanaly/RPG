using Godot;
using System;

public partial class InventoryUI : CanvasLayer
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public int maxInventoryCount;
    public int currentButtonIndex = 0;

    // Protected

    // Private
    [Export] private Button[] itemButtons;
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
            CycleSelectedButton(1);
        }
        if (Input.IsActionJustPressed("Up")) {
            CycleSelectedButton(-1);
        }
        if (Input.IsActionJustPressed("Left")) {
            CycleSelectedButton(-5);
        }
        if (Input.IsActionJustPressed("Right")) {
            CycleSelectedButton(5);
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init()
    {
        maxInventoryCount = itemButtons.Length;
    }

    public void AddItemToIndex(int index, ItemData data) 
    {
        itemButtons[index].Text = data.itemName;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    // Protected

    // Private
    private void Toggle() 
    {
        Visible = !Visible;
        GetTree().Paused = !GetTree().Paused;
    }

    protected void CycleSelectedButton(int step)
    {
        int newButtonIndex = currentButtonIndex + step;

        // Bounds Check
        if ((newButtonIndex < 0) || (newButtonIndex >= itemButtons.Length)) 
        {
            return;
        }

        currentButtonIndex = newButtonIndex;
        itemButtons[currentButtonIndex].ButtonPressed = true;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
