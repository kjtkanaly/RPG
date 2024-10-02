using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private InventoryUI ui;
    private ItemData[] items;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        ui.Init();

        items = new ItemData[ui.maxInventoryCount];
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public bool AddItem(ItemData data)
    {
        // Check if there is room in the list
        if (CheckIfInventoryFull()) {
            return false;
        }

        // Get the first free index
        int freeIndex = GetFreeIndex();

        // Set the data in the free index
        items[freeIndex] = data;

        // Update the UI
        ui.AddItemToIndex(freeIndex, data);

        return true;
    }

    // Protected

    // Private
    private bool CheckIfInventoryFull() 
    {
        foreach (ItemData item in items) {
            if (item == null) {
                return false;
            }
        }
        return true;
    }

    private int GetFreeIndex() {
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                return i;
            }
        }
        return -1;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
