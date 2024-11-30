using Godot;
using System;
using Godot.Collections;

public partial class BattleSceneCharacter : Node2D
{
    public enum TYPE
    {
        PLAYER = 0,
        ENEMY = 4
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    public bool active = false;

    // Protected

    // Private
    [Export] private TYPE typeValue;
    [Export] private Sprite2D sprite;
    [Export] private AnimationPlayer animationPlayer;
    private CharacterData data;
    private int battleIndex;
    private int initiativeRoll;
    private Main main;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void Init(CharacterData inData) 
    {
        // Get the Main Node
        main = GetNode<Main>("/root/Main");

        // Log that the Character Node is active
        active = true;

        // Set the Character Data
        data = inData;
        
        // Set the character's sprite
        sprite.Texture = data.GetBattleSprite();

        // Roll Inititave
        initiativeRoll = main.rng.RandiRange(1, 20);
    }

    public Sprite2D GetSprite() { return sprite; }

    public AnimationPlayer GetAnimationPlayer() { return animationPlayer; }

    public CharacterData GetData() { return data; }

    public int GetInitiativeRoll() { return initiativeRoll; }

    public void SetBattleIndex(int inIndex) { battleIndex = inIndex; }

    public int GetBattleIndex() { return battleIndex; }

    public bool IsOnPlayerTeam() { return typeValue == TYPE.PLAYER; }

    public void PlayAttackAnimation() 
    { 
        animationPlayer.Play(data.GetAttackAnimationName());
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
