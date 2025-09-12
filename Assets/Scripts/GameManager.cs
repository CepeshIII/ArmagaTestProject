using UnityEditor;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    public void Reload()
    {
        var cardDataBase = CardDataBase.Instance;
        cardDataBase.ReloadData();

        var grid = IsometricGrid.Instance;
        grid.BuildGrid();

        var board = GameBoard.Instance;
        board.SetGrid(grid);
    }


    new private void Awake()
    {
        base.Awake();
    }


#if UNITY_EDITOR
    public void OnEnable()
    {
        Reload();
    }
#endif

}
