public class GridService : IGridService
{
    private readonly GridBounds gridBounds;
    private readonly IsometricGrid isometricGrid;


    public GridService(GridBounds gridBounds, IsometricGrid isometricGrid)
    {
        this.gridBounds = gridBounds;
        this.isometricGrid = isometricGrid;
    }


    public void BuildGrid()
    {
        isometricGrid.BuildFromBounds(gridBounds);
    }
}
