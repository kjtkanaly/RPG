using Godot;
using System;

[GlobalClass]
public partial class InteractionData : Resource
{
    [Export] public Texture2D dialogueIcon;
    [Export] public string[] currentDialogue;
    [Export] public PackedScene battleScene;
    [Export] public int maxHealth;
}
