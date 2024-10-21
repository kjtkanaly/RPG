using Godot;
using System;

public partial class CharacterDirector : CharacterBody2D
{
    public enum CharacterType {
        Player = 0,
        Ally = 1,
        Enemy = 2
    }

    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Export] public CharacterType charactertype;
    public float itemRayLength = 30.0f;
    public int currentHealth;
    public bool canInteract = true;

    // Protected
    [Export] protected MovementData movementData;
    [Export] protected CharacterData characterData;
    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected AudioStreamPlayer audioPlayer;
    [Export] protected StateMachine movementSM;
    [Export] protected Sprite2D sprite;
    [Export] protected RayCast2D interactRay;
    [Export] protected Timer interactionTimer;
    [Export] protected Inventory inventory;
    protected PlayerStats playerStats;
    protected Main main;

    // Private

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        Init();
    }

    public void Init() {
        // Get the global player stats object
        playerStats = GetNode<PlayerStats>("/root/PlayerStats");
        main = GetNode<Main>("/root/Main");

        // Initialize the movement State Machine
        movementSM.Init(this);    

        // Attach the interaction flag method to the timer
        if (interactionTimer != null) {
            interactionTimer.Timeout += SetInteractionTrue; 
        }

        // Init the inventory
        inventory.Init(characterData.inventorySize);
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        movementSM.ProcessInput(inputEvent);
    }

    public override void _PhysicsProcess(double delta)
    {   
        // Call the Movement State Machine's Physics Process
        movementSM.PhysicsProcess((float) delta);
    }

    public override void _Process(double delta)
    {
        switch (charactertype) 
        {
            case (CharacterType.Player):
                ProcessPlayer(delta);
                break;
            case (CharacterType.Ally):
                ProcessNPC(delta);
                break;
            default:
                break;
        }
        

    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public MovementData GetMovementData() 
    {
        return movementData;
    }

    public CharacterData GetCharacterData()
    {
        return characterData;
    }

    public AnimationPlayer GetAnimationPlayer() 
    {
        return animationPlayer;
    }

    public AudioStreamPlayer GetAudioPlayer()
    {
        return audioPlayer;
    }

    public Sprite2D GetSprite() 
    {
        return sprite;
    }

    public RayCast2D GetInteractRay()
    {
        return interactRay;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    // Protected

    // Private
    private void ProcessPlayer(double delta)  
    {
        if (Input.IsActionJustReleased("Interact") && canInteract) {
            // Decide Interaction Path
            ChooseInteractionPath();
        }
    }

    private void ProcessNPC(double delta)
    {
        
    }

    private void ChooseInteractionPath() 
    {
        // Get the potential item's Node object
        Node collider = (Node) interactRay.GetCollider();

        // Check if we actually collided with something
        if (collider == null) {
            return;
        }

        // If the interaction is an item
        if (collider.IsInGroup("Item")) {
            PickupItem(collider);
        }

        // If an ally interaction
        else if (collider.IsInGroup("Ally") || collider.IsInGroup("Enemy")) {
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
        main.DispalyTextBox(data);

        // Wait for the item description to be done
        await ToSignal(main, Main.SignalName.DialogueOver);

        // Display the Bool Selection Box
        string booleanString = $"Pickup {item.GetData().name}?";
        main.DisplayBooleanBox(booleanString);

        // Await User Choice
        await ToSignal(main, Main.SignalName.SelectionMade);

        // Pickup the item
        main.PickupItemSequence(item, inventory);

        // TODO: If not enough space then display text saying no room
    }

    private async void InteractWithNPC(Node collider)
    {
        // Get the Ally's Character Body
        CharacterDirector npc = (CharacterDirector) collider;

        // Create the Text Box Data Object
        TextBox.TextBoxData data = new TextBox.TextBoxData(
            TextBox.TEXT_BOX_TYPE.item,
            npc.GetCharacterData().currentDialogue,
            npc.GetCharacterData().dialogueIcon);

        main.DispalyTextBox(data);
        
        // Wait for the Dialogue to be done
        await ToSignal(main, Main.SignalName.DialogueOver);

        // Set the player to unable to interact to account for user delay
        canInteract = false;

        // Start the interaction delay timer
        interactionTimer.Start();

        // If Enemy then start battle sequence
        if (npc.IsInGroup("Enemy")) {
            // Switch to battle scene
            main.BeginBattle(npc.GetCharacterData().battleSceneFilePath);
            
        }
    }

    private void SetInteractionTrue()
    {
        canInteract = true;
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}
