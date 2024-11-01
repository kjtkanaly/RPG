using Godot;
using System;
using Godot.Collections;

public partial class CharacterStats : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private Dictionary<string, string> stats = new Dictionary<string, string>
    {
        { "DEX", "" },
        { "STR", "" },
        { "CON", "" },
        { "WIS", "" },
        { "INT", "" },
        { "CHA", "" },  
    };

    [Export] private Label name;
    [Export] private Label currentHP;
    [Export] private Label maxHP;
    [Export] private Label currentLevel;
    [Export] private TextureRect portraitRect;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void SetData(CharacterData inData) 
    {
        SetName(inData.GetName());
        SetStats(inData.GetStats());
        SetHP(inData.GetCurrentHealth(), inData.GetMaxHealth());
        SetLevel(inData.GetLevel());
        SetPortraitPicture(inData.GetPortrait());
    }

    public void SetStat(System.Collections.Generic.KeyValuePair<string, int> stat)
    {
        Label statLabel = (Label) GetNode(stats[stat.Key]);
        string text = statLabel.Text;

        string[] subString = text.Split(": ");

        subString[1] = stat.Value.ToString();
        statLabel.Text = subString[0] + ": " + subString[1];
    }
    
    public void SetStats(Dictionary<string, int> stats)
    {
        foreach(System.Collections.Generic.KeyValuePair<string, int> stat in stats)
        {
            SetStat(stat);
        }
    }

    public void SetName(string inName) 
    {
        name.Text = inName;
    }

    private void SetHP(int currentHPValue, int maxHPValue)
    {
        currentHP.Text = currentHPValue.ToString();
        maxHP.Text = maxHPValue.ToString();
    }

    private void SetLevel(int currentLevelValue) 
    {
        currentLevel.Text = currentLevelValue.ToString();
    }

    private void SetPortraitPicture(Texture2D picture) 
    {
        portraitRect.Texture = picture;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
