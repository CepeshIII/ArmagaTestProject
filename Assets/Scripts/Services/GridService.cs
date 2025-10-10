using UnityEngine;

public class GridService : IGridService
{
    private readonly GridBoundsBehaviour gridBounds;
    private readonly ILinearGrid grid;


    public GridService(GridBoundsBehaviour gridBounds, ILinearGrid grid)
    {
        this.gridBounds = gridBounds;
        this.grid = grid;
    }


    public void BuildGrid()
    {
        grid.BuildGrid(gridBounds.bounds);
    }
}
