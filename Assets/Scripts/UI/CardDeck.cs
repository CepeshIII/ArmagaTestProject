using System;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck
{
    private List<CardData> cards;

    public event Action OnDeckChanged;

    public List<CardData> Cards { get => cards; }


    public CardDeck()
    {
        cards = new List<CardData>();
    }

    public CardDeck(List<CardData> cards)
    {
        this.cards = cards;
    }

    public void AddCard(CardData card)
    {
        cards.Add(card);
        OnDeckChanged?.Invoke();
    }


    public void RemoveCard(CardData card)
    {
        cards.Remove(card);
        OnDeckChanged?.Invoke();
    }
}
