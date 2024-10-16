using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

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
    [Export] private BoolTextBox boolTextBox;
    [Export] private TextBox textBox;
    [Export] private InventoryUI inventoryUI;
    private List<CharacterDirector> teamDirectors;
    private PackedScene previousScene;
    private int currentCharacterIndex = 0;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        
        // Get the team character directors
        teamDirectors = new List<CharacterDirector>();
        foreach (CharacterDirector character in GetTree().GetNodesInGroup("Team")) {
            teamDirectors.Add(character);
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("Interact")) {
            EmitSignal(SignalName.Interaction);
        }

        if (Input.IsActionJustReleased("Inventory")) {
            DisplayInventory();
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void DisplayInventory()
    {
        Inventory inventory = GetCharacterInventory();

        if (inventory == null) {
            return;
        }

        inventoryUI.Toggle(inventory);
    }

    public Inventory GetCharacterInventory()
    {
        if (teamDirectors.Count == 0) {
            return null;
        }

        return teamDirectors[currentCharacterIndex].GetInventory();
    }

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
        // Log current scene
        previousScene = new PackedScene();
        previousScene.Pack(GetTree().CurrentScene);

        // Begin the battle scene
        GetTree().ChangeSceneToPacked(battleScene);
    }

    public CharacterData GetIndexCharacterData(int index)
    {
        return teamDirectors[index].GetCharacterData();
    }

    public PackedScene GetPreviousScene()
    {
        return previousScene;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
