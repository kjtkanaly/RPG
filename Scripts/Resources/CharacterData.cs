using Godot;
using System;
using Godot.Collections;

public partial class CharacterData : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private string name = "";
    [Export] private int level = 1;
    [Export] private Texture2D portrait;
    [Export] private Texture2D battleSprite;
    [Export] private string[] currentDialogue;
    [Export] private int inventorySize = 1;
    [Export] private Vector2I damageRange = new Vector2I(1, 2);
    [Export] private int maxHealth = 1;
    [Export] private Dictionary<string, int> stats = new Dictionary<string, int>
    {
        {"DEX", 10},
        {"STR", 10},
        {"CON", 10},
        {"WIS", 10},
        {"INT", 10},
        {"CHA", 10}
    };
    private int currentHealth;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        currentHealth = maxHealth;
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public string GetName() {return name;}

    public int GetLevel() {return level;}

    public Texture2D GetPortrait() {return portrait;}

    public string[] GetCurrentDialogue() {return currentDialogue;}

    public int GetInventorySize() {return inventorySize;}

    public Vector2I GetDamageRange() {return damageRange;}

    public int GetMaxHealth() {return maxHealth;}

    public int GetCurrentHealth() {return currentHealth;}

    public void IterateCurrentHealth(int step) {currentHealth += step;}

    public Dictionary<string, int> GetStats() {return stats;}

    public Texture2D GetBattleSprite() {return battleSprite;}

    public void UpdateCharacterData(CharacterData inData) 
    {
        name = inData.GetName();
        level = inData.GetLevel();
        portrait = inData.GetPortrait();
        battleSprite = inData.GetBattleSprite();
        currentDialogue = inData.GetCurrentDialogue();
        inventorySize = inData.GetInventorySize();
        damageRange = inData.GetDamageRange();
        maxHealth = inData.GetMaxHealth();
        currentHealth = inData.GetCurrentHealth();
        stats = inData.GetStats();
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}
