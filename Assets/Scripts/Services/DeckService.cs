using Zenject;

public class DeckService : IDeckService
{
    private readonly CardDeckBuilder deckBuilder;
    private readonly CardDeckController deckController;
    private readonly DiContainer container;



    public DeckService(CardDeckBuilder deckBuilder, CardDeckController deckController, DiContainer container) 
    { 
        this.deckBuilder = deckBuilder;
        this.deckController = deckController;
        this.container = container;
    }


    public void CreateAndAssignDeck()
    {
        //Create a fresh deck
        var deck = deckBuilder.CreateRandomDeck(5);

        //Bind it for this round
        RoundInstaller.Install(container, deck);
        deckController.SetDeck(deck);

    }
}