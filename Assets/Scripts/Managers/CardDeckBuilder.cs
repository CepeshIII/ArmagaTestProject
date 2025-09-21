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
        return new CardDeck(new List<CardData>
        {
            db.GetCardDataById(0),
            db.GetCardDataById(1),
            db.GetCardDataById(2),
            db.GetCardDataById(3),
            db.GetCardDataById(4),
        });
    }


    public CardDeck CreateRandomDeck(int size)
    {
        var cards = new List<CardData>();
        for (int i = 0; i < size; i++)
        {
            var randomId = Random.Range(0, db.CardCount); // example range
            cards.Add(db.GetCardDataById(randomId));
        }
        return new CardDeck(cards);
    }
}
