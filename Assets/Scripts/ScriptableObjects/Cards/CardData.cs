using UnityEngine;
using UnityEngine.Tilemaps;

public enum CardType
{
    Unit,
    Building,
    Spell
}


public abstract class CardData : ScriptableObject
{
    public int cardId;

    public string cardName;
    public string description;
    public Sprite sprite;

    public Tile tile;

    public abstract CardType CardType { get; }
}

