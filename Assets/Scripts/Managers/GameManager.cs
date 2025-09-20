using UnityEditor;
using UnityEngine;
using Zenject;


public class GameManager : Singleton<GameManager>, IInitializable
{
    private readonly RoundManager roundManager;



    [Inject]
    public GameManager(RoundManager roundManager)
    {
        this.roundManager = roundManager;
    }


    public void Initialize()
    {
        roundManager.StartNewRound(); // start immediately
    }


    public void RestartRound()
    {
        roundManager.StartNewRound(); // replaces old deck with a new one
    }


    new private void Awake()
    {
        base.Awake();
    }


}
