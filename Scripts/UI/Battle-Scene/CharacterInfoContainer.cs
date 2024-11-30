using Godot;
using System;
using Godot.Collections;
using SC = System.Collections.Generic;

public partial class CharacterInfoContainer : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PackedScene packedCharacterInfo;
    [Export] private HBoxContainer infoContainer;
    private SC.Dictionary<BattleSceneCharacter, CharacterInfo> infoList = 
        new SC.Dictionary<BattleSceneCharacter, CharacterInfo>();

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(Array<BattleSceneCharacter> characters) 
    {
        // Loop over the characters
        for (int i = 0; i < characters.Count; i++) {
            if (!characters[i].active) {
                continue;
            }

            // Create and the characterInfo to the element
            CharacterInfo info = (CharacterInfo) packedCharacterInfo.Instantiate();
            infoContainer.AddChild(info);

            CharacterData data = characters[i].GetData();

            // Set the Character Info
            info.SetProfilePic(data.GetPortrait());
            info.SetHealthValue(data.GetHealthByKey("Current"));
            info.SetSpecialValue(100);
            info.SetHealthBar(
                data.GetHealthByKey("Current"),
                data.GetHealthByKey("Max"));

            // Add the new Info to the UI
            infoList.Add(characters[i], info);
        }
    }

    public void UpdateInfoByCharacter(BattleSceneCharacter character)
    {
        UpdateHealthInfo(character);
    }

    // Protected

    // Private
    private void UpdateHealthInfo(
        BattleSceneCharacter character) 
    {
        CharacterInfo info = infoList[character];

        info.SetHealthValue(character.GetData().GetHealthByKey("Current"));
        info.SetHealthBar(character.GetData().GetHealthByKey("Current"));
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
