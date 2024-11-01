using Godot;
using System;

public partial class CharacterDirector : CharacterBody2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

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
    [Export] protected Area2D agroArea;
    [Export] protected Area2D fightArea;
    [Export] protected CharacterBodyState coolDownState;
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
        inventory.Init(characterData.GetInventorySize());
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

    public override void _Process(double delta)
    {
        movementSM.ProcessGeneral((float) delta);
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

    public Main GetMain()
    {
        return main;
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

    public Area2D GetAgroArea() 
    {
        return agroArea;    
    }

    public Area2D GetFightArea() 
    {
        return fightArea;
    }

    public void StartInteractionDelayTimer()
    {
        interactionDelayTimer.Start();
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

    public void SwitchCurrentStateToCoolDown() 
    {
        movementSM.SetCurrentState(coolDownState);
    }

    public State GetCurrentState() 
    {
        return movementSM.GetCurrentState();
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
