using Godot;
using System;

[GlobalClass]
public partial class CharacterData : Resource
{
    [Export] public string name;
    [Export] public int level;
    [Export] public int currentHealth;
    [Export] public int currentDex;
    [Export] public int currentMana;
    [Export] public int maxHealth;
    [Export] public int maxDex;
    [Export] public int maxMana;
    [Export] public Texture2D battlePortrait;
    [Export] public Texture2D dialogueIcon;
    [Export] public string[] currentDialogue;
    [Export] public int inventorySize;
    [Export] public Vector2I damageRange;
}
