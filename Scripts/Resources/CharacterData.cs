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
    private Texture2D portrait;
    private Texture2D battleSprite;
    private Dictionary data;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Init();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init()
    {
        // Load in Character Data File
        LoadData();

        // Load the texture 2D
        portrait = (Texture2D) GD.Load(GetPortraitPath());
        battleSprite = (Texture2D) GD.Load(GetBattleSpritePath());

        // Priint the Name
        GD.Print(data["Name"]);
    }

    public string GetName() {return (string) data["Name"];}

    public int GetLevel() {return (int) data["Level"];}

    public string[] GetDialogByKey(string key) {return ((Dictionary<string, string[]>) data["Dialog"])[key];}

    public int GetInventorySize() {return (int) data["Inventory-Size"];}

    public Vector2I GetDamageRange() {return (Vector2I) data["Damage-Range"];}

    public int GetHealthByKey(string key) {return ((Dictionary<string, int>) data["Health"])[key];}

    public int GetMaxHealth() {return (int) data["Max Health"];}

    public int GetCurrentHealth() {return (int) data["Current Health"];}

    public void IterateCurrentHealth(int step) {/*data["currentHealth"] += step;*/}

    public Dictionary<string, int> GetStats() {return (Dictionary<string, int>) data["Stats"];}

    public Texture2D GetPortrait() {return portrait;}

    public Texture2D GetBattleSprite() {return battleSprite;}

    public string GetPortraitPath() {return (string) data["Portrait-Path"];}

    public string GetBattleSpritePath() {return (string) data["Battle-Sprite-Path"];}

    public void UpdateCharacterData(CharacterData inData) 
    {
        data["name"] = inData.GetName();
        data["level"] = inData.GetLevel();
        data["portrait"] = inData.GetPortrait();
        data["battleSprite"] = inData.GetBattleSprite();
        // data["currentDialogue"] = inData.GetDialogByKey();
        data["inventorySize"] = inData.GetInventorySize();
        // data["damageRange"] = inData.GetDamageRange();
        // data["maxHealth"] = inData.GetMaxHealth();
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
