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
    private String enemyInstanceNodePath;

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
        playerTeam = new List<CharacterData>();
        enemyTeam = new List<CharacterData>();
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void QueueBattle(
        CharacterData[] inPlayerTeam,
        CharacterData[] inEnemyTeam,
        String inEnemyInstanceNodePath) 
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

        enemyInstanceNodePath = inEnemyInstanceNodePath;
    }

    public List<CharacterData> GetPlayerTeam()
    {
        return playerTeam;
    }

    public List<CharacterData> GetEnemyTeam()
    {
        return enemyTeam;
    }

    public String GetEnemyInstanceNodePath()
    {
        return enemyInstanceNodePath;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}