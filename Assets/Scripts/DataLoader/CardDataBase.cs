using System.Collections.Generic;
using UnityEngine;

public class CardDataBase
{
    private Dictionary<int, CardData> idToCard;
    private Dictionary<string, CardData> nameToCard;

    private readonly string path = "CardsData";


    public CardData GetCardDataById(int id) =>
        idToCard.TryGetValue(id, out var card) ? card : null;

    public CardData GetCardDataByName(string name) =>
        nameToCard.TryGetValue(name, out var card) ? card : null;


    public CardDataBase()
    {
        LoadCardData();
    }


    public void ReloadData()
    {
        LoadCardData();
    }


    private void LoadCardData()
    {
        DataLoader.TryLoadData<CardData>(path, out var cardsData);

        idToCard = new ();
        nameToCard = new ();

        for(int i = 0; i < cardsData.Length; i++) 
        {
            var cardData = cardsData[i];

            if (idToCard.ContainsKey(cardData.cardId))
            {
                Debug.LogError($"Duplicate cardId found: {cardData.cardId} in {cardData.name}");
                continue;
            }
            idToCard.Add(cardData.cardId, cardData);

            if (nameToCard.ContainsKey(cardData.name))
            {
                Debug.LogError($"Duplicate card name found: {cardData.name}");
                continue;
            }
            nameToCard.Add(cardData.name, cardData);
        }
    }
}
