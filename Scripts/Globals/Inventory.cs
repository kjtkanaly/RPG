using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal] public delegate void AddedItemEventHandler();

    // Protected

    // Private
    private ItemData[] items;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(int inventorySize)
    {
        items = new ItemData[inventorySize];
    }

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

        // Emit the "Added Item" signal
        EmitSignal(SignalName.AddedItem);

        return true;
    }

    public ItemData GetItem(int index) 
    {
        return items[index];
    }

    public int GetInventorySize()
    {
        return items.Length;
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
