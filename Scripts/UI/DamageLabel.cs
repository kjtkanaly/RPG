using Godot;
using System;

public partial class DamageLabel : Label
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private SceneTreeTimer timer;
    private float time;
    private float displayTime = 1.5f;
    private float finalDiff = 150.0f;
    private float posRange = 50.0f;
    private Vector2 initPos;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {   
        // Get the time 
        time += (float) delta / displayTime;
        time = Mathf.Clamp(time, 0, 1.0f);

        // Update the Positon
        Vector2 pos = Position;
        pos.Y = initPos.Y + (finalDiff * EaseOutCirc(time));
        Position = pos;

        // Update the sprite fade
        Color mod = Modulate;
        mod.A = 1 - EaseOutCirc(time);
        Modulate = mod;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public SceneTreeTimer Init(int value, RandomNumberGenerator rng)
    {
        // Set the Text
        Text = value.ToString();

        // Start Tween
        timer = GetTree().CreateTimer(displayTime);

        // Set the label to destroy after the timer ends
        timer.Timeout += Destroy;

        // Calc the init 
        Position = new Vector2(
            posRange * 2 * (rng.Randf() - 0.5f),
            posRange * 2 * (rng.Randf() - 0.5f));

        // Log the init pos
        initPos = Position;

        return timer;
    }

    // Protected

    // Private
    private void Destroy()
    {
        QueueFree();
    }

    private float EaseOutCirc(float t) 
    {
        return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
