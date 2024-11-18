using Godot;
using System;
using Godot.Collections;

public partial class BattleQueueState : CharacterBodyState
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    [Export] private PackedScene battleScene;
    int currentQueueCount = 0;
    int maxQueueCount = 4;
    Array<CharacterDirector> queuedEnemies = new Array<CharacterDirector>();

    //-------------------------------------------------------------------------
    // Game Events

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public override void Enter()
    {
        base.Enter();

        // Scan the Battle queue area (Agro Area) for enemies to queue for battle
        GetQueuedEnemies();

        // Log the number of queued enemeis
        currentQueueCount = queuedEnemies.Count;
        GD.Print(currentQueueCount);

        // Set the queued enemeies to agro that aren't already in battle-waiting
        AgroQueuedEnemies();
    }

    public override State ProcessGeneral(float delta)
    {
        // Check if all the queued enemies are present
        if (!AreAllQueuedEnemiesPresent()) {
            return null;
        }

        BeginBattle();

        return null;
    }

    // Protected

    // Private
    private void GetQueuedEnemies() 
    {
        Array<Node2D> bodies = 
            characterDirector.GetAgroArea().GetOverlappingBodies();

        for (int i = 0; i < bodies.Count; i++) {
            if (bodies[i].IsInGroup("Basic-Enemy") 
                && queuedEnemies.Count < maxQueueCount) {
                queuedEnemies.Add((CharacterDirector) bodies[i]);
            }
        }
    }

    private void AgroQueuedEnemies()
    {
        for (int i = 0; i < queuedEnemies.Count; i++) {
            // Check if the enemy is waiting for battle
            if (queuedEnemies[i].IsWaitingForBattle()) {
                continue;
            }

            queuedEnemies[i].SwitchCurrentStateToAgro();
        }
    }

    private bool AreAllQueuedEnemiesPresent() 
    {
        for (int i = 0; i < queuedEnemies.Count; i++) {
            if (!queuedEnemies[i].IsWaitingForBattle()) {
                return false;
            }
        }

        return true;
    }

    private async void BeginBattle() 
    {
        // Do some animatio

        // Get an array of the queued enemy data
        CharacterData[] newQueuedEnemyData = new CharacterData[queuedEnemies.Count];
        for (int i = 0; i < queuedEnemies.Count; i++) {
            newQueuedEnemyData[i] = queuedEnemies[i].GetCharacterData();
        }

        // Pass in Arrays with the character data path's for player and team
        // Pass in the node path's for the enemy team
        Array<String> playerTeamDataPaths = new Array<string>();
        Array<String> enemyTeamDataPaths = new Array<string>();

        // Get the Player Team info
        playerTeamDataPaths.Add(characterDirector.GetCharacterData().GetDataPath());

        // Get the Enemy Team info
        for (int i = 0; i < queuedEnemies.Count; i++) {
            enemyTeamDataPaths.Add(queuedEnemies[i].GetCharacterData().GetDataPath());
        }

        // Switch to battle scene
        main.BeginBattle(
            playerTeamDataPaths,
            enemyTeamDataPaths,
            queuedEnemies);
    }

    //-------------------------------------------------------------------------
    // Debug Methods
}

// If you want to denote an area for future devlopement mark with it
// with a to do comment. Example,
// TODO: Do some shit