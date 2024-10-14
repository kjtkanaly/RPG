using Godot;
using System;
using System.Collections;

public partial class Main : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal] public delegate void InteractionEventHandler();
    [Signal] public delegate void DialogueOverEventHandler();
    [Signal] public delegate void SelectionMadeEventHandler();
    public RandomNumberGenerator rng;

    // Protected

    // Private
    [Export] private CharacterData[] teamData;
    [Export] private BoolTextBox boolTextBox;
    [Export] private TextBox textBox;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
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
    public async void DispalyTextBox(TextBox.TextBoxData data)
    {
        // Pause the other processes
        GetTree().Paused = true;

        // Display the Text Box
        foreach (string text in data.text) {
            Tween textTween = textBox.ShowTextBox(
                text, 
                data.icon);

            // Await Text being completed
            await ToSignal(textTween, "finished");

            await ToSignal(this, SignalName.Interaction);
        }

        // Hide the text
        textBox.HideTextBox();

        // Resume the game
        GetTree().Paused = false;

        // Emit Signal that the dialogue is done
        EmitSignal(SignalName.DialogueOver);
    }

    public async void DisplayBooleanBox(string text) 
    {
        // Pause the other processes
        GetTree().Paused = true;

        Tween textTween = boolTextBox.ShowTextBox(text, null);

        // Await Text being completed
        await ToSignal(textTween, "finished");

        // Await the user selection
        await ToSignal(boolTextBox, "SelectionMade");

        // Emit signal that signifes the selection is made
        EmitSignal(SignalName.SelectionMade);
    }

    public void PickupItemSequence(ItemDirector item, Inventory inventory) 
    {  
        // Set the item to cool down to avoid user response delay
        item.coolDown = true;

        // Get the User Choice
        bool choice = boolTextBox.GetUserChoice();

        // Do something based upon the choice
        if (choice) {
            // Keep the Data
            bool itemAdded = inventory.AddItem(item.GetData());

            GD.Print($"Choice: {choice} | Item Added: {itemAdded}");

            // Destory the Item
            if (itemAdded) {
                item.QueueFree();
            }
        } 
        else {
            item.StartResponseDelayTime();
        }

        // Hide the text
        boolTextBox.HideTextBox();

        // Resume the game
        GetTree().Paused = false;

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
