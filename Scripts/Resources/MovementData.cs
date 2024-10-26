using Godot;
using System;

[GlobalClass]
public partial class MovementData : Resource
{
	[Export] public float speed = 0.0f;
	[Export] public float walkSpeed = 0.0f;
	[Export] public float acceleration = 0.0f;
	[Export] public float friction = 0.0f;
	[Export] public float mass = 0.0f;
	[Export] public float jumpVelocity = 0.0f;  
}