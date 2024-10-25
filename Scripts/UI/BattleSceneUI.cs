using Godot;
using System;

public partial class BattleSceneUI : CanvasLayer
{
    public enum CHARACTER_TYPE
    {
        Player,
        Enemy
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private CharacterBattleSceneInfo characterInfo;
    [Export] private BattleSceneMainAction mainAction;
    [Export] private Control damagePosPlayer;
    [Export] private Control damagePosEnemy;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void SetCharacterInfo(CharacterData data) 
    {
        characterInfo.SetCharacterInfo(data);
    }

    public BattleSceneMainAction GetMainAction()
    {
        return mainAction;
    }

    public CharacterBattleSceneInfo GetCharacterInfo()
    {
        return characterInfo;
    }

    public void HideUI() {
        mainAction.Visible = false;
        characterInfo.Visible = false;
    }

    public void ShowUI() {
        mainAction.Visible = true;
        characterInfo.Visible = true;
    }

    public Control GetDamagePos(CHARACTER_TYPE type)
    {
        if (type == CHARACTER_TYPE.Player) {
            return damagePosPlayer;
        } 
        else if (type == CHARACTER_TYPE.Enemy) {
            return damagePosEnemy;
        }

        return null;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
