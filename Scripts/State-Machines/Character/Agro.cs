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
    [Export] private State battleWaitingState;
    private CharacterDirector player;
    private bool battleBegins = false;

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
        // If the Basic Enemy has reached the player switch battle waiting
        if (IsGroupMemeberInComabatRange("Player")) {
            return battleWaitingState;
        }

        // TODO: If the player has escaped
        // if () {

        // }

        UpdateVelocityTowardsPlayer();
        characterDirector.MoveAndSlide();

        return null;
    }

    // Protected

    // Private
    private void UpdateVelocityTowardsPlayer()
    {
        Vector2 direction = (player.Position - characterDirector.Position).Normalized();

        characterDirector.Velocity = 
            direction 
            * characterDirector.GetMovementData().speed;
    }

    // private async void EnemyBeginsBattle() 
    // {
    //     // Create the Text Box Data Object
    //     TextBox.TextBoxData data = new TextBox.TextBoxData(
    //         TextBox.TEXT_BOX_TYPE.item,
    //         characterDirector.GetCharacterData().GetDialogByKey("Fight"),
    //         characterDirector.GetCharacterData().GetPortrait());

    //     characterDirector.GetMain().GetMainUI().DispalyTextBox(data);
        
    //     // Wait for the Dialogue to be done
    //     await ToSignal(characterDirector.GetMain().GetMainUI(), MainUI.SignalName.DialogueOver);

    //     GD.Print("Begin battle");

    //     // Switch to battle scene
    //     main.BeginBattle(
    //         new CharacterData[] {player.GetCharacterData()}, 
    //         new CharacterData[] {characterDirector.GetCharacterData()},
    //         characterDirector);
    // }

    //-------------------------------------------------------------------------
    // Debug Methods
}