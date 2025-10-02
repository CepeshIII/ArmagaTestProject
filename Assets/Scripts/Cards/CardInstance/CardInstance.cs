using UnityEngine;


public abstract class CardInstance
{
    public CardData Data { get; }
    public Vector2Int IndexCoords { get; private set; }



    public CardInstance(CardData data, Vector2Int indexCoords = default)
    {
        Data = data;
        IndexCoords = indexCoords;
    }


    public void Move(Vector2Int newCoords)
    {
        IndexCoords = newCoords;
    }
    
    
    public abstract void ResetParam();
    public abstract string GetDescription();
}
