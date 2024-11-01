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
        if (IsPlayerInFightRange()) {
            // Switch the Enemy's Current State to Cool Down
            characterDirector.SwitchCurrentStateToCoolDown();

            // 
            GD.Print($"Enemy State @Agro: {characterDirector.GetCurrentState().Name}");

            // Begin fight
            EnemyBeginsBattle();
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

    private void UpdateVelocityTowardsPlayer()
    {
        Vector2 direction = (player.Position - characterDirector.Position).Normalized();

        characterDirector.Velocity = 
            direction 
            * characterDirector.GetMovementData().speed;
    }

    private async void EnemyBeginsBattle() 
    {
        // Create the Text Box Data Object
        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.item,
            characterDirector.GetCharacterData().GetCurrentDialogue(),
            characterDirector.GetCharacterData().GetPortrait());

        characterDirector.GetMain().GetMainUI().DispalyTextBox(data);
        
        // Wait for the Dialogue to be done
        await ToSignal(characterDirector.GetMain().GetMainUI(), MainUI.SignalName.DialogueOver);

        GD.Print("Begin battle");

        // Switch to battle scene
        main.BeginBattle(
            new CharacterData[] {player.GetCharacterData()}, 
            new CharacterData[] {characterDirector.GetCharacterData()},
            characterDirector);
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}