using UnityEngine;
using Zenject;
using System;

public class RoundManager: IInitializable
{
    private readonly CardDeckBuilder deckFactory;
    private readonly DiContainer container;

    [Inject]
    public RoundManager(CardDeckBuilder deckFactory, DiContainer container)
    {
        this.deckFactory = deckFactory;
        this.container = container;
    }

    public void Initialize()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        // 1. Create a fresh deck
        var deck = deckFactory.CreateDefaultDeck();

        // 2. Bind it for this round
        RoundInstaller.Install(container, deck);
        var controller = container.Resolve<CardDeckController>();
        controller.SetDeck(deck);

        // (Optional) Notify listeners
        Debug.Log("New round started with deck: " + deck);
    }
}
