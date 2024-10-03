using Godot;
using System;

public partial class ItemDirector : StaticBody2D
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public bool coolDown = false;

    // Protected

    // Private
    [Export] private ItemData data;
    [Export] private Timer responseDelay;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        responseDelay.Timeout += StopCoolDown;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public ItemData GetData() 
    {
        return data;
    }

    public void StartResponseDelayTime() 
    {
        responseDelay.Start();
    }

    // Protected

    // Private
    private void StopCoolDown() 
    {
        coolDown = false;
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
