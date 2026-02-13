using System.Collections.Generic;
using UnityEngine;


public class CardDataBase
{
    private Dictionary<int, CardData> idToCard;
    private Dictionary<string, CardData> nameToCard;

    private readonly string path = "CardsData";

    public int CardCount => idToCard?.Count ?? 0;



    public CardDataBase()
    {
        LoadCardData();
    }

    public void ReloadData()
    {
        LoadCardData();
    }


    public bool TryGetCardDataById(int id, out CardData card)
    {
        if (idToCard.TryGetValue(id, out card))
        {
            if (card != null) return true;

            Debug.LogWarning($"[CardDataBase] ID {id} found, but CardData is null.");
        }
        else
        {
            Debug.LogWarning($"[CardDataBase] ID {id} not found in database.");
        }

        card = null;
        return false;
    }


    public bool TryGetCardDataByName(string name, out CardData card)
    {
        if (nameToCard.TryGetValue(name, out card))
        {
            if (card != null) return true;

            Debug.LogWarning($"[CardDataBase] Name '{name}' found, but CardData is null.");
        }
        else
        {
            Debug.LogWarning($"[CardDataBase] Name '{name}' not found in database.");
        }

        card = null;
        return false;
    }


    private void LoadCardData()
    {
        if (!DataLoader.TryLoadData<CardData>(path, out var cardsData))
        {
            Debug.LogError($"[CardDataBase] Failed to load data from path: {path}");
            return;
        }

        idToCard = new Dictionary<int, CardData>();
        nameToCard = new Dictionary<string, CardData>();

        for (int i = 0; i < cardsData.Length; i++)
        {
            var cardData = cardsData[i];
            if (cardData == null) continue;

            // Handle ID Duplicates
            if (idToCard.ContainsKey(cardData.cardId))
            {
                Debug.LogError($"[CardDataBase] Duplicate cardId: {cardData.cardId} in {cardData.name}");
                continue;
            }
            idToCard.Add(cardData.cardId, cardData);

            // Handle Name Duplicates
            if (nameToCard.ContainsKey(cardData.name))
            {
                Debug.LogError($"[CardDataBase] Duplicate card name: {cardData.name}");
                continue;
            }
            nameToCard.Add(cardData.name, cardData);
        }
    }

}