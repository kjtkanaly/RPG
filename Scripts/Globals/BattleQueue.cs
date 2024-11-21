using Godot;
using System;
using Godot.Collections;

public partial class BattleQueue : Node
{
    //-------------------------------------------------------------------------
    // Game Componenets
    // Public

    // Protected

    // Private
    private Array<CharacterData> playerTeamDataPaths = new Array<CharacterData>();
    private Array<CharacterData> enemyTeamDataPaths = new Array<CharacterData>();
    private Array<String> enemyInstanceNodePath = new Array<string>();

    //-------------------------------------------------------------------------
    // Game Events
    public override void _Ready()
    {
    }

    //-------------------------------------------------------------------------
    // Methods
    // Public
    public void QueueBattle(
        Array<CharacterData> inPlayerTeam,
        Array<CharacterData> inEnemyTeam,
        Array<CharacterDirector> inEnemyDirectors) 
    {
        // Clear out the old teams worth of data
        playerTeamDataPaths.Clear();
        enemyTeamDataPaths.Clear();
        enemyInstanceNodePath.Clear();

        for (int i = 0; i < inPlayerTeam.Count; i++) {
            playerTeamDataPaths.Add(inPlayerTeam[i]);
        }

        for (int i = 0; i < inEnemyTeam.Count; i++) {
            enemyTeamDataPaths.Add(inEnemyTeam[i]);
        }

        for (int i = 0; i < inEnemyDirectors.Count; i++) {
            enemyInstanceNodePath.Add(inEnemyDirectors[i].GetPath());
        }
    }

    public Array<CharacterData> GetPlayerTeam()
    {
        return playerTeamDataPaths;
    }

    public Array<CharacterData> GetEnemyTeam()
    {
        return enemyTeamDataPaths;
    }

    public Array<String> GetEnemyNodePaths()
    {
        return enemyInstanceNodePath;
    }

    // Protected

    // Private

    //-------------------------------------------------------------------------
	// Debug Methods
}
