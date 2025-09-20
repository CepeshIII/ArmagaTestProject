using Zenject;

public class RoundInstaller : Installer<CardDeck, RoundInstaller>
{
    private readonly CardDeck deck;

    public RoundInstaller(CardDeck deck)
    {
        this.deck = deck;
    }

    public override void InstallBindings()
    {
        //Container.Bind<CardDeck>().FromInstance(deck).AsSingle();
    }
}
