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
    [Export] private string dataPath = "";
    private Texture2D portrait;
    private Texture2D battleSprite;
    private Dictionary data;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Init(dataPath);
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(String inDataPath)
    {
        // Load in Character Data File
        LoadData(inDataPath);

        // Load the texture 2D
        portrait = (Texture2D) GD.Load(GetPortraitPath());
        battleSprite = (Texture2D) GD.Load(GetBattleSpritePath());
    }

    public string GetDataPath() { return dataPath; }

    public Dictionary GetData() {return data;}

    public string GetName() {return (string) data["Name"];}

    public int GetLevel() {return (int) data["Level"];}

    public string[] GetDialogByKey(string key) 
    {
        return ((Dictionary<string, string[]>) data["Dialog"])[key];
    }

    public int GetInventorySize() {return (int) data["Inventory-Size"];}

    public int GetHealthByKey(string key) 
    {
        return ((Dictionary<string, int>) data["Health"])[key];
    }
    
    public void SetHealthByKey(string key, int value) 
    {
        ((Dictionary<string, int>) data["Health"])[key] = value;
    }

    public void IterateCurrentHealth(int step) 
    {
        SetHealthByKey("Current", GetHealthByKey("Current") + step);
    }

    public Dictionary<string, int> GetStats() 
    {
        return (Dictionary<string, int>) data["Stats"];
    }

    public int GetStatByKey(string key)
    {
        return ((Dictionary<string, int>) data["Stats"])[key];
    }

    public Texture2D GetPortrait() {return portrait;}

    public Texture2D GetBattleSprite() {return battleSprite;}

    public string GetPortraitPath() {return (string) data["Portrait-Path"];}

    public string GetBattleSpritePath() 
    {
        return (string) data["Battle-Sprite-Path"];
    }

    public Vector2I GetDamageRange() 
    {
        int min = ((Dictionary<string, int>) data["Damage-Range"])["Min"];
        int max = ((Dictionary<string, int>) data["Damage-Range"])["Max"];
        return new Vector2I(min, max);
    }

    public void UpdateCharacterData(CharacterData inData) 
    {
        CopyDictionaryValues(data, inData.GetData());
    }

    public int GetInitiativeValue() { return -1; }

    public void SetInitiative(int inVal) { }

    public int GetTeamIndex() { return -1; }

    public void SetTeamIndex(int inVal) { }

    public string GetAttackAnimationName() { return (string) data["Attack-Animation-Name"]; }

    // Protected

    // Private
    private void CopyDictionaryValues(Dictionary data, Dictionary inData)
    {
        foreach (var (key, value) in inData) {
            if (value.VariantType == Variant.Type.Dictionary) {
                CopyDictionaryValues(
                    (Dictionary) data[key], 
                    (Dictionary) inData[key]);
            }
            else {
                data[key] = inData[key];
            }
        }
    }

    private void LoadData(String inDataPath) 
    {
        // Error Check - If the path doesn't exist
        if (!Godot.FileAccess.FileExists(inDataPath)) {
            string ErrorMsg = $"Unknown Character Data Path '{inDataPath}'";
            GD.PushError(ErrorMsg);
        }

        // Init the raw data string
        string rawData;

        // Read in the raw data string
        rawData = Godot.FileAccess.GetFileAsString(inDataPath);

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
    
    public CharacterData CopyData() 
    {
        CharacterData copy = new CharacterData();
        copy.Init(dataPath);
        copy.UpdateCharacterData(this);

        return copy;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
