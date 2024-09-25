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
    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected AudioStreamPlayer audioPlayer;
    [Export] protected StateMachine movementSM;
    protected PlayerStats playerStats;

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

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
