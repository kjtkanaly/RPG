using Godot;
using System;

public partial class MainUI : CanvasLayer
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public
    [Signal] public delegate void DialogueOverEventHandler();

    // Protected

    // Private
    [Export] private BoolTextBox boolTextBox;
    [Export] private TextBox textBox;
    [Export] private InventoryUI inventoryUI;
    [Export] private SceneTransition sceneTransition;
    private Main main;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("Inventory")) {
            DisplayInventory();
        }
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void Init(Main mainRef)
    {
        main = mainRef;
    }

    public InventoryUI GetInventoryUI()
    {
        return inventoryUI;
    }

    public BoolTextBox GetBoolTextBox()
    {
        return boolTextBox;
    }

    public TextBox GetTextBox() 
    {
        return textBox;
    }

    public async void DispalyTextBox(TextBox.TextBoxData data)
    {
        // Pause the other processes
        GetTree().Paused = true;

        // Display the Text Box
        foreach (string text in data.text) {
            Tween textTween = textBox.ShowTextBox(
                text, 
                data.icon);

            // Await Text being completed
            await ToSignal(textTween, "finished");

            await ToSignal(main, Main.SignalName.Interaction);
        }

        // Hide the text
        textBox.HideTextBox();

        // Resume the game
        GetTree().Paused = false;

        // Emit Signal that the dialogue is done
        EmitSignal(SignalName.DialogueOver);
    }

    public async void DisplayBooleanBox(string text) 
    {
        // Pause the other processes
        GetTree().Paused = true;

        Tween textTween = boolTextBox.ShowTextBox(text, null);

        // Await Text being completed
        await ToSignal(textTween, "finished");

        // Await the user selection
        await ToSignal(boolTextBox, "SelectionMade");

        // Emit signal that signifes the selection is made
        main.EmitSignal(Main.SignalName.SelectionMade);
    }

    public SceneTransition GetSceneTransition() { return sceneTransition; }

    // Protected

    // Private
    private void DisplayInventory()
    {
        // TODO: comeback and fix this shit
        Inventory inventory = null;

        if (inventory == null) {
            return;
        }

        inventoryUI.Toggle(inventory);
    }

    //-------------------------------------------------------------------------
	// Debug Methods
}
