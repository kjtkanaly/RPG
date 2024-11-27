using Godot;
using System;
using Godot.Collections;

public partial class EnemyTeamBattleNodes : ButtonGroupUI
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Dictionary<BaseButton, BattleSceneEnemy> nodes = new Dictionary<BaseButton, BattleSceneEnemy>();

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        base._Ready();

        for(int i = 0; i < buttons.Count; i++) {
            nodes.Add(buttons[i], buttons[i].GetNode<BattleSceneEnemy>(""));
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public Array<BattleSceneEnemy> GetNodes() 
    { 
        Array<BattleSceneEnemy> output = (Array<BattleSceneEnemy>) nodes.Values;
        return output; 
    }

    public void SetTeamSprites(
        Array<CharacterData> teamData)
    {
        for (int i = 0; i < teamData.Count; i++) {
            GetNodes()[i].GetSprite().Texture = teamData[i].GetBattleSprite();
        }
    }

    public BattleSceneEnemy GetEnemyNodeAtIndex(int index) 
    { 
        return GetNodes()[index]; 
    }

    public void SelectFirstEnemy() 
    {
        for (int i = 0; i < buttons.Count; i++) { 
            if (buttons[i].Disabled) {
                continue;
            }

            buttons[i].ButtonPressed = true;
            return;
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
    // Debug Methods
}