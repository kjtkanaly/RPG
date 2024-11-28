using Godot;
using System;
using Godot.Collections;

public partial class CharacterInfoContainer : Control
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PackedScene packedCharacterInfo;
    [Export] private HBoxContainer infoContainer;

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

            // Set the Character Info
            info.SetProfilePic(characters[i].GetData().GetPortrait());
            info.SetHealthValue(characters[i].GetData().GetHealthByKey("Current"));
            info.SetSpecialValue(100);
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
