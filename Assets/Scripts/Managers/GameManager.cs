using System;
using UnityEngine;
using Zenject;



public class GameManager : IInitializable, IDisposable
{
    private readonly IGameStateManager gameStateManager;
    private readonly RoundManager roundManager;
    private readonly SignalBus signalBus;



    [Inject]
    public GameManager(RoundManager roundManager, IGameStateManager gameStateManager, SignalBus signalBus)
    {
        //this.roundManager = roundManager;
        this.gameStateManager = gameStateManager;
        this.roundManager = roundManager;
        this.signalBus = signalBus;
    }


    public void Initialize()
    {
        //roundFactory.CreateRound();
        //roundManager.PlacementPhaseCompleted += PlacementPhaseCompleted;
        //roundManager.PreviewPhaseCompleted += PreviewPhaseCompleted;
        //roundManager.BattlePhaseCompleted += BattlePhaseCompleted;
        //roundManager.GameOver += GameOver;
        //
        //roundManager.SetStats(new RoundStats { maxPlacedCardsCount = 1, placedCardsCount = 0 });
        StartRound();
    }


    public void Dispose()
    {
        //roundManager.PlacementPhaseCompleted -= PlacementPhaseCompleted;
        //roundManager.PreviewPhaseCompleted -= PreviewPhaseCompleted;
        //roundManager.BattlePhaseCompleted -= BattlePhaseCompleted;
        //roundManager.GameOver -= GameOver;
    }


    public void StartRound()
    {
        signalBus.TryFire(new SwitchToNewState(GameState.RoundPassed));
        //signalBus.Fire(new SwitchToNewState(GameState.CardPlacement));
        //gameStateManager.SwitchState(GameState.CardPlacement);
        roundManager.InitRound();
    }


    public void RestartRound()
    {
        gameStateManager.SwitchState(GameState.CardPlacement);
        roundManager.StartNewRound();
    }


    private void PlacementPhaseCompleted()
    {
        Debug.Log("Placement Phase Completed");
        gameStateManager.SwitchState(GameState.PreviewPhase);
    }


    private void PreviewPhaseCompleted()
    {
        Debug.Log("Preview Phase Completed");
        gameStateManager.SwitchState(GameState.BattlePhase);
    }


    private void BattlePhaseCompleted()
    {
        Debug.Log("BattlePhase Phase Completed");
        gameStateManager.SwitchState(GameState.RoundPassed);
        RestartRound();
    }


    private void GameOver()
    {
        Debug.Log("GameOver Phase Completed");
        gameStateManager.SwitchState(GameState.GameOver);
    }


}
