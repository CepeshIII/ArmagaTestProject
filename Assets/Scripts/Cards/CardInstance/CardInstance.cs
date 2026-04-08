using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class CardInstance
{
    public CardData Data { get; }
    public Vector2Int IndexCoords { get; private set; }
    public Team Team { get; set; }


    public CardInstance(CardData data, Vector2Int indexCoords, Team team)
    {
        Data = data;
        IndexCoords = indexCoords;
        Team = team;
    }


    public void Move(Vector2Int newCoords)
    {
        IndexCoords = newCoords;
    }
    
    
    public void SetTeam(Team team)
    {
        Team = team;
    }


    public abstract void ResetParam();
    public abstract IEnumerable<string> GetDescription();
}
