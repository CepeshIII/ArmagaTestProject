using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class CardDeckBuilder
{
    private readonly CardDataBase db;


    [Inject]
    public CardDeckBuilder(CardDataBase db)
    {
        this.db = db;
    }


    public CardDeck CreateDefaultDeck()
    {
        var defaultCardIds = new List<int>()
        {
            1, 2, 3, 4,
        };

        var cards = new List<CardData>();
        foreach (var defaultCardId in defaultCardIds)
        {
            if (db.TryGetCardDataById(defaultCardId, out var card))
            {
                cards.Add(card);
            }
        }

        return new CardDeck(cards);
    }


    public CardDeck CreateRandomDeck(int size)
    {
        var cards = new List<CardData>();
        var deck = new CardDeck();

        for (int i = 0; i < size; i++)
        {
            AddRandomCardToDeck(deck);
        }

        return deck;
    }


    public CardDeck CreateDeckWithAllCards()
    {
        var cards = new List<CardData>();
        for (int i = 0; i < db.CardCount; i++)
        {
            if (db.TryGetCardDataById(i, out var card))
            {
                cards.Add(card);
            }
        }
        return new CardDeck(cards);
    }


    public void AddRandomCardToDeck(CardDeck deck)
    {
        var randomId = Random.Range(0, db.CardCount); // example range
        if (db.TryGetCardDataById(randomId, out var card))
        {
            deck.AddCard(card);
        }
    }


}




