using Godot;
using System;
using System.Collections.Generic;

public partial class BattleQueue : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private List<CharacterData> playerTeam;
    private List<CharacterData> enemyTeam;

    //-------------------------------------------------------------------------
	// Game Events

    //-------------------------------------------------------------------------
	// Methods
    // Public
    public void QueueBattle(
        CharacterData[] inPlayerTeam,
        CharacterData[] inEnemyTeam) 
    {
        // Clear out the old teams worth of data
        playerTeam.Clear();
        enemyTeam.Clear();

        foreach (CharacterData data in inPlayerTeam) {
            playerTeam.Add(data);
        }

        foreach (CharacterData data in inEnemyTeam) {
            enemyTeam.Add(data);
        }
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}