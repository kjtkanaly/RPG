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
    public float itemRayLength = 30.0f;

    // Protected
    [Export] protected MovementData movementData;
    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected AudioStreamPlayer audioPlayer;
    [Export] protected StateMachine movementSM;
    [Export] protected Sprite2D sprite;
    [Export] protected RayCast2D itemRay;
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
        if (Input.IsActionJustPressed("Interact")) {
            PickupItem();
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public MovementData GetMovementData() 
    {
        return movementData;
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

    public RayCast2D GetItemRay()
    {
        return itemRay;
    }

    // Protected

    // Private
    private void PickupItem()
    {
        // Get the potential item's Node object
        Node collider = (Node) itemRay.GetCollider();

        // Check if the colliding object is an item
        if (!collider.IsInGroup("Item")) {
            return;
        }

        //
        ItemDirector item = (ItemDirector) collider;

        // Pickup the item
        main.DisplayItemTextBox(item.GetData());
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
