using Godot;
using System;

public partial class Agro : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State searchState;
    private CharacterDirector player;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {   
        // Get ref to the player node
        Godot.Collections.Array<Godot.Node> playerNodes = 
            GetTree().GetNodesInGroup("Player");

        player = (CharacterDirector) playerNodes[0];
    }

    public override State ProcessPhysics(float delta)
    {
        if (IsPlayerInFightRange()) {
            // Begin fight
            EnemyBeginsBattle();

            return null;
        }

        UpdateVelocityTowardsPlayer();
        characterDirector.MoveAndSlide();

        return null;
    }

    // Protected

    // Private
    private bool IsPlayerInFightRange() 
    {
        Godot.Collections.Array<Godot.Node2D> bodies = 
            characterDirector.GetFightArea().GetOverlappingBodies();

        foreach (Node2D node in bodies) {
            if (node.IsInGroup("Player")) {
                return true;
            }
        }

        return false;
    }

    private async void EnemyBeginsBattle() 
    {
        // Create the Text Box Data Object
        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.item,
            characterDirector.GetCharacterData().currentDialogue,
            characterDirector.GetCharacterData().portrait);

        characterDirector.GetMain().GetMainUI().DispalyTextBox(data);
        
        // Wait for the Dialogue to be done
        await ToSignal(characterDirector.GetMain().GetMainUI(), MainUI.SignalName.DialogueOver);

        // Switch to battle scene
        main.BeginBattle(
            new CharacterData[] {player.GetCharacterData()}, 
            new CharacterData[] {characterDirector.GetCharacterData()},
            characterDirector);
    }

    private void UpdateVelocityTowardsPlayer()
    {
        Vector2 direction = (player.Position - characterDirector.Position).Normalized();

        characterDirector.Velocity = 
            direction 
            * characterDirector.GetMovementData().speed;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}