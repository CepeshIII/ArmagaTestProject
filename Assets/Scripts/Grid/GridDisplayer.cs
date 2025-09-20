using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(IsometricGrid))]
[ExecuteAlways]
public class GridDisplayer : MonoBehaviour
{
    private struct MouseClickInfo
    {
        public Vector2Int indexCoords;

        public Vector2 rectPosition; // IndexCoords in rectangular space
        public Vector2 isoPosition; // IndexCoords in isometric space
    }

    private IsometricGrid grid;
    private MouseClickInfo mouseClickInfo;


    private void OnEnable()
    {
        grid = GetComponent<IsometricGrid>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0f;

            var mouseIsoPosition = mouseWorldPosition; // We see game in isometric space,
                                                       // so our world IndexCoords is isometric IndexCoords

            // Transform isometric IndexCoords to rectangular IndexCoords
            var mouseRectPosition = IsoMath.ReverseIsoProject(mouseIsoPosition);

            // Find the cell index in rectangular space
            var cellIndex = new Vector2Int(
                Mathf.FloorToInt(mouseRectPosition.x / grid.CellSize.x),
                Mathf.FloorToInt(mouseRectPosition.y / grid.CellSize.y)
            );

            mouseClickInfo.rectPosition = mouseRectPosition;
            mouseClickInfo.isoPosition = mouseIsoPosition;
            mouseClickInfo.indexCoords = grid.GridPositionToIndexCoords(cellIndex); 

            // Debug output
            Debug.Log($"MousePosition: {mouseWorldPosition}, mouseRectPosition: {mouseRectPosition}");
            Debug.Log($"globalCellIndex: {cellIndex}, moved to origin indexCoords: {mouseClickInfo.indexCoords}");
        }
    }


    private void DrawCellRectProjection(Vector2 position, Vector2 cellSize, Color color)
    {
        var halfCellSize = (cellSize) / 2f;

        // Draw Normal Projection
        var leftUpCorner = (position + new Vector2(-halfCellSize.x, halfCellSize.y));
        var rightDownCorner = (position + new Vector2(halfCellSize.x, -halfCellSize.y));
        var leftDownCorner = (position + new Vector2(-halfCellSize.x, -halfCellSize.y));
        var rightUpCorner = (position + new Vector2(halfCellSize.x, halfCellSize.y));

        DrawRect(leftUpCorner, rightUpCorner, rightDownCorner, leftDownCorner, color);
    }


    private static void DrawCellIsometricProjection(Vector2 position, Vector2 cellSize, Color color)
    {
        var halfCellSize = (cellSize) / 2f;

        // Draw Isometric Projection
        var leftUpCorner = IsoMath.IsoProject(position + new Vector2(-halfCellSize.x, halfCellSize.y));
        var rightUpCorner = IsoMath.IsoProject(position + new Vector2(halfCellSize.x, halfCellSize.y));
        var rightDownCorner = IsoMath.IsoProject(position + new Vector2(halfCellSize.x, -halfCellSize.y));
        var leftDownCorner = IsoMath.IsoProject(position + new Vector2(-halfCellSize.x, -halfCellSize.y));

        DrawRect(leftUpCorner, rightUpCorner, rightDownCorner, leftDownCorner, color);
    }


    private static void DrawRect(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, c);
        Gizmos.DrawLine(c, d);
        Gizmos.DrawLine(d, a);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(mouseClickInfo.rectPosition, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(mouseClickInfo.isoPosition, 0.1f);

        for (int y = 0; y < grid.GridSize.y; y++)
        {
            for (int x = 0; x < grid.GridSize.x; x++)
            {
                var cellIndex = new Vector2Int(x, y);
                var centerRectPosition = grid.IndexCoordsToGridPosition(cellIndex) + new Vector2(0.5f, 0.5f);

                DrawCellRectProjection(centerRectPosition, grid.CellSize, Color.black);
                DrawCellIsometricProjection(centerRectPosition, grid.CellSize, Color.blue);
            }
        }
    }
}
