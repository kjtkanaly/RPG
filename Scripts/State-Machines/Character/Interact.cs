using Godot;
using System;
using Godot.Collections;

public partial class Interact : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private State idleState;
    private bool isInteracting;

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        isInteracting = true;
        ChooseInteractionPath();
    }

    public override State ProcessPhysics(float delta)
    {
        if (!isInteracting) {
            return idleState;
        }

        return null;
    }

    // Protected

    // Private
    private void ChooseInteractionPath() 
    {
        // Get the potential item's Node object
        Node collider = (Node) characterDirector.GetInteractRay().GetCollider();

        // Check if we actually collided with something
        if (collider == null) {
            return;
        }

        // If the interaction is an item
        if (collider.IsInGroup("Item")) {
            PickupItem(collider);
        }

        // If the interaction is an Ally
        else if (collider.IsInGroup("Ally")) {
            InteractWithAlly(collider);
        }

        // If the interaction is an enemy
        else if (collider.IsInGroup("Enemy")) {
            InteractWithNPC(collider);
        }
    }

    private async void PickupItem(Node collider)
    {
        // Get the Item Director
        ItemDirector item = (ItemDirector) collider;

        // Check if the item was just interacted with
        if (item.coolDown) {
            return;
        }

        // Create the Text Box Data Object
        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.item,
            item.GetData().textBoxDescription,
            null);

        // Dispaly the item's text box
        main.GetMainUI().DispalyTextBox(data);

        // Wait for the item description to be done
        await ToSignal(main.GetMainUI(), MainUI.SignalName.DialogueOver);

        // Display the Bool Selection Box
        string booleanString = $"Pickup {item.GetData().name}?";
        main.GetMainUI().DisplayBooleanBox(booleanString);

        // Await User Choice
        await ToSignal(main, Main.SignalName.SelectionMade);

        // Pickup the item
        main.PickupItemSequence(item, characterDirector.GetInventory());

        // Log that the interaction is done
        isInteracting = false;

        // Start the interaction timer
        characterDirector.StartInteractionDelayTimer();

        // TODO: If not enough space then display text saying no room
    }

    private async void InteractWithAlly(Node collider) 
    {
        // Get the Ally's Character Body
        CharacterDirector npc = (CharacterDirector) collider;

        // Create the Text Box Data Object
        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.item,
            npc.GetCharacterData().GetDialogByKey("Talk"),
            npc.GetCharacterData().GetPortrait());

        main.GetMainUI().DispalyTextBox(data);
        
        // Wait for the Dialogue to be done
        await ToSignal(main.GetMainUI(), MainUI.SignalName.DialogueOver);

        // Log that interaction is done
        isInteracting = false;

        // Start the character's interaction delay timer
        characterDirector.StartInteractionDelayTimer();

        // Set the Ally to follow the player and add them to the team
        npc.GetStateMachine().SwitchCurrentStateToFollowThePlayer();
        characterDirector.GetTeam().Add(npc);
    }

    private async void InteractWithNPC(Node collider)
    {
        // Get the Ally's Character Body
        CharacterDirector npc = (CharacterDirector) collider;

        // Create the Text Box Data Object
        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.item,
            npc.GetCharacterData().GetDialogByKey("Talk"),
            npc.GetCharacterData().GetPortrait());

        main.GetMainUI().DispalyTextBox(data);
        
        // Wait for the Dialogue to be done
        await ToSignal(main.GetMainUI(), MainUI.SignalName.DialogueOver);

        // Log that interaction is done
        isInteracting = false;

        // Start the character's interaction delay timer
        characterDirector.StartInteractionDelayTimer();

        // // If Enemy then start battle sequence
        // if (npc.IsInGroup("Enemy")) {
        //     // Switch to battle scene
        //     main.BeginBattle(
        //         new CharacterData[] {characterDirector.GetCharacterData()}, 
        //         new CharacterData[] {npc.GetCharacterData()},
        //         new Array<CharacterDirector> {npc});
        // }
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
