using Godot;
using System;
using Godot.Collections;

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
    [Export] private BattleSceneNew battleSceneNew;
    [Export] private Level currentOverworld;
    [Export] private AudioStreamPlayer2D soundEffectPlayer;
    [Export] private AudioStream incrementSoundEffect;
    [Export] private AudioStream selectSoundEffect;
    [Export] private PackedScene packedBattleScene;
    [Export] private PackedScene gameOverScreen;
    [Export] private PackedScene mainMenu;
    [Export] private PackedScene checkpointScene;

    //-------------------------------------------------------------------------
	// Game Events
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();

        // Init the Main UI
        mainUI.Init(this);
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

    public async void BeginBattle(
        Array<CharacterDirector> inPlayerTeam,
        Array<CharacterDirector> inEnemyTeam) 
    {
        // Zoom animation
        Tween zoomTween = currentOverworld.GetCamera().ZoomInOnCharacter();
        await ToSignal(zoomTween, Tween.SignalName.Finished);
        
        // Disable the Overworld Camera and make it invisible
        currentOverworld.GetCamera().Enabled = false;
        SetObjectStructVisibleValue(currentOverworld, false);

        // Instantiate the battle scene
        battleSceneNew = (BattleSceneNew) packedBattleScene.Instantiate();
        this.AddChild(battleSceneNew);

        // Enable the Battle Scene and it's view
        battleSceneNew.GetCamera().Enabled = true;

        GetTree().Paused = true;

        battleSceneNew.Begin(inPlayerTeam, inEnemyTeam);
    }

    public async void EndBattle (
        Array<CharacterDirector> playerTeam, 
        Array<CharacterDirector> enemyTeam,
        BattleSceneNew.END_STATE endState)
    {
        GetTree().Paused = false;

        // Enable the Battle Scene and it's view
        battleSceneNew.GetCamera().Enabled = false;
        battleSceneNew.QueueFree();
        await ToSignal(battleSceneNew, Node2D.SignalName.TreeExited);

        // Enable the Overworld Scene and it's View
        currentOverworld.GetCamera().Enabled = true;
        SetObjectStructVisibleValue(currentOverworld, true);

        // Reset the Overworld Camera
        Tween zoomTween = currentOverworld.GetCamera().ZoomOutFromCharacter();
        await ToSignal(zoomTween, Tween.SignalName.Finished);

        switch (endState) {
            case (BattleSceneNew.END_STATE.PLAYER_VICTORY):
                VictorySequence(playerTeam, enemyTeam);
                break;
            case (BattleSceneNew.END_STATE.ENEMY_VICTORY):
                GameOverSequence();
                break;
            case (BattleSceneNew.END_STATE.FLEE):
                // FleeBattleSequence(inPlayerTeam, inEnemyTeam);
                break;
            default:
                break;
        }
    }

    public MainUI GetMainUI()
    {
        return mainUI;
    }

    public void RestartFromCheckpoint() 
    {
        // Reload to the previous checkpoint
        // TODO: Setup checkpoints lol
        GetTree().ChangeSceneToPacked(checkpointScene);
    }

    public void QuitToMainMenu() 
    {
        GetTree().ChangeSceneToPacked(mainMenu);
    }

    public AudioStreamPlayer2D GetSoundEffectPlayer() { return soundEffectPlayer; }

    public void PlayIncrementSoundEffect() 
    { 
        soundEffectPlayer.Stream = incrementSoundEffect; 
        soundEffectPlayer.Play();
    }

    public void PlaySelectSoundEffect()
    {
        soundEffectPlayer.Stream = incrementSoundEffect; 
        soundEffectPlayer.Play();
    }

    public async void SwitchToPackedScene(PackedScene scene)
    {
        Tween transitoinTween = mainUI.GetSceneTransition().SceneFade(
            SceneTransition.FadeDirection.OUT);

        await ToSignal(transitoinTween, Tween.SignalName.Finished);

        GetTree().ChangeSceneToPacked(scene);
    }

    public async void FadeIn()
    {
        Tween transitoinTween = mainUI.GetSceneTransition().SceneFade(
            SceneTransition.FadeDirection.IN);

        await ToSignal(transitoinTween, Tween.SignalName.Finished);
        return;
    }

    public void SetCurrentOverworld(Level inLevel) { currentOverworld = inLevel; }

    // Protected

    // Private
    private async void VictorySequence(
        Array<CharacterDirector> playerTeam,
        Array<CharacterDirector> enemyTeam
    ) 
    {
        for (int i = 0; i < enemyTeam.Count; i++) {
            enemyTeam[i].QueueFree();
            await ToSignal(enemyTeam[i], Node2D.SignalName.TreeExited);
        }

        for (int i = 0; i < playerTeam.Count; i++) {
            if (playerTeam[i].IsInGroup("Player")) {
                playerTeam[i].GetStateMachine().SwitchCurrentStateToIdle();
            }
            else {
                if (playerTeam[i].GetCharacterData().GetHealthByKey("Current") <= 0) {
                    playerTeam[i].QueueFree();
                    await ToSignal(playerTeam[i], Node2D.SignalName.TreeExited);
                }
            }
            
        }
    }

    private void FleeBattleSequence(Array<CharacterDirector> enemyTeam)
    {
        for (int i = 0; i < enemyTeam.Count; i++) {
            // Switch the enemy to a cool down state
            enemyTeam[i].GetStateMachine().SwitchCurrentStateToCoolDown();

            Color colour = enemyTeam[i].Modulate;
                colour.A = 1;
                enemyTeam[i].Modulate = colour;
        }
    }

    private void GameOverSequence() 
    {
        // Swap back to scene prior to the battle
        GetTree().ChangeSceneToPacked(gameOverScreen);
    }

    private void SetObjectStructVisibleValue(Node2D obj, bool value) 
    {
        Godot.Collections.Array args = new Godot.Collections.Array();
        args.Add(value);
        obj.PropagateCall("set_visible", args);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
