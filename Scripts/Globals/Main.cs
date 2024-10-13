using Godot;
using System;

public partial class Main : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal] public delegate void InteractionEventHandler();
    [Signal] public delegate void DialogueOverEventHandler();
    public RandomNumberGenerator rng;

    // Protected

    // Private
    [Export] private CharacterData[] teamData;
    [Export] private BoolTextBox itemTextBox;
    [Export] private TextBox dialogueTextBox;
    private Inventory inventory;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();

        InventoryUI inventoryUI = GetNode<InventoryUI>("/root/InventoryUi");
        inventory = inventoryUI.GetInventory();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("Interact")) {
            EmitSignal(SignalName.Interaction);
        }
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

    public async void DisplayCharacterDialogue(CharacterData data) 
    {
        // Pause the other processes
        GetTree().Paused = true;

        // Display the Text Box
        foreach (string dialogue in data.currentDialogue) {
            Tween textTween = dialogueTextBox.ShowTextBox(
                dialogue, 
                data.dialogueIcon);

            // Await Text being completed
            await ToSignal(textTween, "finished");

            await ToSignal(this, SignalName.Interaction);
        }

        // Hide the text
        dialogueTextBox.HideTextBox();

        // Resume the game
        GetTree().Paused = false;

        // Emit Signal that the dialogue is done
        EmitSignal(SignalName.DialogueOver);
    }

    public void BeginBattle(PackedScene battleScene) 
    {
        // Begin the battle scene
        GetTree().ChangeSceneToPacked(battleScene);
    }

    public CharacterData GetIndexCharacterData(int index)
    {
        return teamData[index];
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
