using Godot;
using System;

public partial class CharacterDirector : CharacterBody2D
{
    public enum CharacterType {
        Player = 0,
        Ally = 1,
        Enemy = 2
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public CharacterType charactertype;

    // Protected
    [Export] protected MovementData movementData;
    [Export] protected CharacterData characterData;
    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected AudioStreamPlayer audioPlayer;
    [Export] protected StateMachine movementSM;
    [Export] protected Sprite2D sprite;
    [Export] protected RayCast2D interactRay;
    [Export] protected Timer interactionDelayTimer;
    [Export] protected Inventory inventory;
    protected PlayerStats playerStats;
    protected Main main;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Init();
    }

    public void Init() {
        // Get the global player stats object
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
        main = GetNode<Main>("/root/Main");

        // Initialize the movement State Machine
        movementSM.Init(this);    

        // Init the inventory
        inventory.Init(characterData.inventorySize);
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        movementSM.ProcessInput(inputEvent);
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        movementSM.PhysicsProcess((float) delta);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public MovementData GetMovementData() 
    {
        return movementData;
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
    }

    public AnimationPlayer GetAnimationPlayer() 
    {
        return animationPlayer;
    }

    public AudioStreamPlayer GetAudioPlayer()
    {
        return audioPlayer;
    }

    public Sprite2D GetSprite() 
    {
        return sprite;
    }

    public RayCast2D GetInteractRay()
    {
        return interactRay;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void StartInteractionDelayTimer()
    {
        interactionDelayTimer.Start();
        GD.Print("Start Timer");
    }

    public bool CanInteract()
    {
        if (interactionDelayTimer.TimeLeft > 0) {
            return false;
        }
        else {
            return true;
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
