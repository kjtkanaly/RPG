using Godot;
using System;

public partial class Main : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public RandomNumberGenerator rng;

    // Protected

    // Private
    [Export] private BoolTextBox itemTextBox;
    private Inventory inventory;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();

        InventoryUI inventoryUI = GetNode<InventoryUI>("/root/InventoryUi");
        inventory = inventoryUI.GetInventory();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public async void DisplayItemTextBox(ItemDirector item) 
    {
        // Pause the other processes
        GetTree().Paused = true;

        // Display the Text Box
        Tween textTween = itemTextBox.ShowTextBox(item.GetData().textBoxString, null);

        // Await Text being completed
        await ToSignal(textTween, "finished");

        // Await User Choice
        await ToSignal(itemTextBox, "SelectionMade");

        // Get the User Choice
        bool choice = itemTextBox.GetUserChoice();
        
        // Set the item to cool down to avoid user response delay
        item.coolDown = true;

        // Do something based upon the choice
        if (choice) {
            // Keep the Data
            bool itemAdded = inventory.AddItem(item.GetData());

            // Destory the Item
            if (itemAdded) {
                item.QueueFree();
            }
        } else {
            item.StartResponseDelayTime();
        }

        // Hide the text
        itemTextBox.HideTextBox();

        // Resume the game
        GetTree().Paused = false;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
