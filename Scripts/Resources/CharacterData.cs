using Godot;
using System;
using Godot.Collections;
using System.IO;

public partial class CharacterData : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private string dataPath = "";
    private Dictionary data;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        // Load in Character Data File
        LoadData();

        // Priint the Name
        GD.Print(data["Name"]);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public string GetName() {return (string) data["Name"];}

    public int GetLevel() {return (int) data["Level"];}

    public Texture2D GetPortrait() {return (Texture2D) data["Portrait"];}

    public string[] GetCurrentDialogue() {return (string[]) data["Current Dialogue"];}

    public int GetInventorySize() {return (int) data["Inventory-Size"];}

    public Vector2I GetDamageRange() {return (Vector2I) data["Damage-Range"];}

    public int GetMaxHealth() {return (int) data["Max Health"];}

    public int GetCurrentHealth() {return (int) data["Current Health"];}

    public void IterateCurrentHealth(int step) {/*data["currentHealth"] += step;*/}

    public Dictionary<string, int> GetStats() {return (Dictionary<string, int>) data["Stats"];}

    public Texture2D GetBattleSprite() {return (Texture2D) data["Battle Sprite"];}

    public void UpdateCharacterData(CharacterData inData) 
    {
        data["name"] = inData.GetName();
        data["level"] = inData.GetLevel();
        data["portrait"] = inData.GetPortrait();
        data["battleSprite"] = inData.GetBattleSprite();
        data["currentDialogue"] = inData.GetCurrentDialogue();
        data["inventorySize"] = inData.GetInventorySize();
        data["damageRange"] = inData.GetDamageRange();
        data["maxHealth"] = inData.GetMaxHealth();
        data["currentHealth"] = inData.GetCurrentHealth();
        data["stats"] = inData.GetStats();
    }

    // Protected

    // Private
    private void LoadData() 
    {
        // Error Check - If the path doesn't exist
        if (!Godot.FileAccess.FileExists(dataPath)) {
            string ErrorMsg = "Unknown Character Data Path" + dataPath;
            GD.PushError(ErrorMsg);
        }

        // Init the raw data string
        string rawData;

        // Read in the raw data string
        rawData = Godot.FileAccess.GetFileAsString(dataPath);

        // Parse the data
        Json jsonLoader = new Json();
        Error parseError = jsonLoader.Parse(rawData);

        // Check that the JSON data string was parsed successfully
        if (parseError != Error.Ok) {
            GD.PushError(parseError);
        }

        // Convert the JSON data to a Godot Dictionary Object
        data = (Dictionary) jsonLoader.Data;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
