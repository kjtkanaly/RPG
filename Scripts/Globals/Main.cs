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
    [Signal] public delegate void BattleOverEventHandler();
    public RandomNumberGenerator rng;

    // Protected

    // Private
    [Export] private MainUI mainUI;
    [Export] private BattleQueue battleQueue;
    [Export] private PackedScene battleScene;
    private PackedScene previousScene;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();

        // Init the Main UI
        mainUI.Init(this);
        
        // Get the team character directors
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
        CharacterData[] inEnemyTeam,
        CharacterDirector inEnemyDirector) 
    {
        // Set the enemy instance invisible
        Color colour = inEnemyDirector.Modulate;
        colour.A = 0;
        inEnemyDirector.Modulate = colour;

        // Log current scene
        previousScene = new PackedScene();
        previousScene.Pack(GetTree().CurrentScene);

        // Queue the battle info
        battleQueue.QueueBattle(
            inPlayerTeam, 
            inEnemyTeam, 
            inEnemyDirector.GetPath());

        // Begin the battle scene
        GetTree().ChangeSceneToPacked(battleScene);
    }

    public async void EndBattle (
        List<CharacterData> inPlayerTeam, 
        List<CharacterData> inEnemyTeam)
    {
        // If the previous scene is null then crash tbh
        if (previousScene == null) {
            GD.PushError("No previous scene loaded");
        }

        // Swap back to scene prior to the battle
        GetTree().ChangeSceneToPacked(previousScene);

        // Wait for the Scene root to be loaded and then wait for it be ready
        await ToSignal(GetTree(), SceneTree.SignalName.NodeAdded);
        await ToSignal(GetTree().CurrentScene, Node.SignalName.Ready);

        // Update the Enemy Team Objects Post Battle
        PostBattleEnemyEvents(inEnemyTeam);

        // Update the Player Team Objects Post Battle
        PostBattlePlayerTeamEvents(inPlayerTeam);
    }

    public BattleQueue GetBattleQueue()
    {
        return battleQueue;
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
    private void PostBattleEnemyEvents(List<CharacterData> inEnemyTeam)
    {
        // Get the Enemy Node
        CharacterDirector enemyNode = 
            (CharacterDirector) GetNode(battleQueue.GetEnemyInstanceNodePath());

        // Switch the enemy to a cool down state
        enemyNode.SwitchCurrentStateToCoolDown();

        // If the player won, then get rid of the enemy instance
        if (inEnemyTeam[0].GetCurrentHealth() <= 0) {
            enemyNode.QueueFree();
        }
        // Set the enemy instance visible and set to cooldown
        else {
            Color colour = enemyNode.Modulate;
            colour.A = 1;
            enemyNode.Modulate = colour;
        }
    }

    private void PostBattlePlayerTeamEvents(List<CharacterData> inPlayerTeam)
    {
        // Get the Player Team's Nodes
        Godot.Collections.Array<Godot.Node> playerTeamNodes = 
            GetTree().GetNodesInGroup("Player-Team");

        // Cycle through the Player's Team and Update their Data
        for (int i = 0; i < playerTeamNodes.Count; i++) {
            // Conver the player team member node to character director
            CharacterDirector playerTeamMember = 
                (CharacterDirector) playerTeamNodes[i];

            // Cycle through the List of Input Player Team Data and find one that matches the current member
            for (int j = 0; j < inPlayerTeam.Count; j++) {
                if (playerTeamMember.GetCharacterData().GetName() 
                    != inPlayerTeam[j].GetName()) {
                    continue;
                }

                playerTeamMember.UpdateCharacterData(inPlayerTeam[j]);
            }
        }
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
