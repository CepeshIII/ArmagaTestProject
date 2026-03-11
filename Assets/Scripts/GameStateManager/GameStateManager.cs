using System;
using System.Collections.Generic;
using Zenject;


public enum GameState
{
    CardPlacement,
    PreviewPhase,
    BattlePhase,
    GameOver,
    RoundPassed,
    LevelPassed,
    BetweenRound,
    Menu,
}


public struct GameStats
{
    public int roundNumber;
}


public struct RoundStats
{
    public int maxPlacedCardsCount;
    public int placedCardsCount;
}


public struct SwitchToNewState
{
    public GameState NewState { get; private set; }


    public SwitchToNewState(GameState newState)
    {
        NewState = newState;
    }

}


public interface IGameStateManager
{
    public void SwitchState(GameState state);
}


public class GameStateManager: IInitializable, IDisposable, IGameStateManager
{
    private readonly GameStateContext stateContext;
    private readonly SignalBus signalBus;
    private readonly DiContainer container;

    private IGameState currentState;
    private IGameState previoslyState;

    public IGameState CurrentState { get => currentState; }
    public IGameState PreviouslyState { get => previoslyState; }



    public GameStateManager(GameStateContext gameStateContext,
        DiContainer container, SignalBus signalBus)
    {
        this.stateContext = gameStateContext;
        this.signalBus = signalBus;
        this.container = container;
    }


    public void Initialize()
    {
        signalBus.Subscribe<SwitchToNewState>(SwitchState);
    }


    public void Dispose()
    {
        signalBus.Unsubscribe<SwitchToNewState>(SwitchState);
    }


    public void SwitchState(GameState state)
    {
        var newState = container.ResolveId<IGameState>(state);

        previoslyState = currentState;
        currentState = newState;

        if (previoslyState != null)
        {
            previoslyState.Exit();
        }

        if (currentState != null)
        {
            currentState.Enter();
        }
    }


    private void SwitchState(SwitchToNewState switchToNewState)
    {
        SwitchState(switchToNewState.NewState);
    }
}
