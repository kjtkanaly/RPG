using Godot;
using System;

[GlobalClass]
public partial class CharacterData : Resource
{
    [Export] public string name;
    [Export] public int level;
    [Export] public Texture2D portrait;
    [Export] public string[] currentDialogue;
    [Export] public int inventorySize;
    [Export] public Vector2I damageRange;
    [Export] public int currentHealth;
    [Export] public int maxHealth;
    [Export] public int dexterity;
    [Export] public int strength;
    [Export] public int constitution;
    [Export] public int inteligence;
    [Export] public int wisdom;
    [Export] public int charisma;
}
