using Godot;
using System;

public partial class CharacterBattleSceneInfo : Control
{
    public enum UI_Type
    {
        health,
        dex,
        mana
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private Label nameLabel;
    [Export] private Label levelLabel;
    [Export] private Label healthLabel;
    [Export] private Label dexLabel;
    [Export] private Label manaLabel;
    [Export] private TextureProgressBar healthBar;
    [Export] private TextureProgressBar dexBar;
    [Export] private TextureProgressBar manaBar;
    [Export] private TextureRect imageTexture;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void SetCharacterInfo(CharacterData data)
    {
        // Defautl
        if (data == null) {
            return;
        }

        UpdateName(data.name);
        UpdateLevel(data.level);
        UpdateValueAndProgressBar(
            UI_Type.health, 
            data.currentHealth, 
            data.maxHealth);
        UpdateValueAndProgressBar(
            UI_Type.dex, 
            data.currentDex, 
            data.maxDex);
        UpdateValueAndProgressBar(
            UI_Type.mana, 
            data.currentMana, 
            data.maxMana);
        UpdateImage(data.battlePortrait);
    }

    // Protected

    // Private
    public Label GetUILabel(UI_Type uiType)
    {
        Label uiLabel = null;
        switch (uiType) {
            case (UI_Type.health):
                uiLabel = healthLabel;
                break;
            case (UI_Type.dex):
                uiLabel = dexLabel;
                break;
            case (UI_Type.mana):
                uiLabel = manaLabel;
                break;
        }
        return uiLabel;
    }

    public TextureProgressBar GetProgressBar(UI_Type uiType)
    {
        TextureProgressBar bar = null;
        switch (uiType) {
            case (UI_Type.health):
                bar = healthBar;
                break;
            case (UI_Type.dex):
                bar = dexBar;
                break;
            case (UI_Type.mana):
                bar = manaBar;
                break;
        }
        return bar;
    }

    public void UpdateName(string nameValue)
    {
        nameLabel.Text = nameValue;
    }

    public void UpdateLevel(int value)
    {
        levelLabel.Text = value.ToString();
    }

    public void UpdateValueAndProgressBar(UI_Type uiType, int value, int maxValue)
    {
        // Get the Label and Progress Bar
        TextureProgressBar bar = GetProgressBar(uiType);
        Label valueLabel = GetUILabel(uiType);
        
        // Update the UI elements
        UpdateProgressBar(bar, value, maxValue);
        UpdateValueLabel(valueLabel, value);
    }

    public void UpdateImage(Texture2D texture)
    {
        imageTexture.Texture = texture;
    }

    private void UpdateProgressBar(TextureProgressBar bar, int value, int maxValue)
    {
        bar.Value = value;
        bar.MaxValue = maxValue;
    }

    private void UpdateValueLabel(Label label, int value)
    {
        label.Text = value.ToString();
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
