using UnityEngine;

public class GridService : IGridService
{
    private readonly IGridBoundsBehaviour gridBounds;
    private readonly ILinearGrid grid;


    public GridService(IGridBoundsBehaviour gridBounds, ILinearGrid grid)
    {
        this.gridBounds = gridBounds;
        this.grid = grid;
    }


    public void BuildGrid()
    {
        grid.BuildGrid(gridBounds.GetGridBounds());
    }
}
