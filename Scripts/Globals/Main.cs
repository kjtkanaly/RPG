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
    [Signal] public delegate void SelectionMadeEventHandler();
    public RandomNumberGenerator rng;

    // Protected

    // Private
    [Export] private MainUI mainUI;
    [Export] private BattleQueue battleQueue;
    [Export] private PackedScene battleScene;
    private List<CharacterDirector> teamDirectors;
    private PackedScene previousScene;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();

        // Init the Main UI
        mainUI.Init(this);
        
        // Get the team character directors
        teamDirectors = new List<CharacterDirector>();
        UpdatePlayerTeam();
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
    public void UpdatePlayerTeam() 
    {
        foreach (CharacterDirector character in GetTree().GetNodesInGroup("Team")) {
            teamDirectors.Add(character);
        }
    }

    public Inventory GetCharacterInventoryAtIndex(int index)
    {
        if (teamDirectors.Count == 0) {
            return null;
        }

        return teamDirectors[index].GetInventory();
    }

    public void PickupItemSequence(ItemDirector item, Inventory inventory) 
    {  
        // Set the item to cool down to avoid user response delay
        item.coolDown = true;

        // Get the User Choice
        bool choice = mainUI.GetBoolTextBox().GetUserChoice();

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
        mainUI.GetBoolTextBox().HideTextBox();

        // Resume the game
        GetTree().Paused = false;

    }

    public void BeginBattle(
        CharacterData[] inPlayerTeam, 
        CharacterData[] inEnemyTeam) 
    {
        // Log current scene
        previousScene = new PackedScene();
        previousScene.Pack(GetTree().CurrentScene);

        // Queue the battle info
        SetBattleQueue(inPlayerTeam, inEnemyTeam);

        // Begin the battle scene
        GetTree().ChangeSceneToPacked(battleScene);
    }

    public void SetBattleQueue(
        CharacterData[] inPlayerTeam,
        CharacterData[] inEnemyTeam) 
    {
        battleQueue.QueueBattle(inPlayerTeam, inEnemyTeam);
    }

    public BattleQueue GetBattleQueue()
    {
        return battleQueue;
    }

    public CharacterData GetCharacterDataAtIndex(int index)
    {
        // Default
        if (teamDirectors.Count <= index) {
            return null;
        }

        return teamDirectors[index].GetCharacterData();
    }

    public PackedScene GetPreviousScene()
    {
        return previousScene;
    }

    public MainUI GetMainUI()
    {
        return mainUI;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
