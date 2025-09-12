using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridBounds))]
public class GridBoundsInspector : Editor
{
    GridBounds cb;
    private Rect boundArea;

    private const float wpSize = 0.1f;

    private UnityEngine.Grid grid;


    private void OnEnable()
    {
        cb = (GridBounds)target;
        grid = cb.GetComponent<UnityEngine.Grid>();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        DrawBorderHandles();
    }

    // Draw and edit the rectangle defined by gridBounds points (A, B, C, D).
    // Points are stored in rectangular space but displayed in isometric space.
    // This allows accurate visual editing while maintaining correct rectangle ratios.
    public void DrawBorderHandles()
    {
        // Reorder points to make sure A is top-left, B is top-right, C is bottom-right, D is bottom-left of the rectangle
        var maxY = Mathf.Max(cb.pointA.y, cb.pointB.y, cb.pointC.y, cb.pointD.y);
        var maxX = Mathf.Max(cb.pointA.x, cb.pointB.x, cb.pointC.x, cb.pointD.x);

        var minY = Mathf.Min(cb.pointA.y, cb.pointB.y, cb.pointC.y, cb.pointD.y);
        var minX = Mathf.Min(cb.pointA.x, cb.pointB.x, cb.pointC.x, cb.pointD.x);

        cb.pointA = new Vector3(minX, maxY, 0);
        cb.pointB = new Vector3(maxX, maxY, 0);

        cb.pointC = new Vector3(maxX, minY, 0);
        cb.pointD = new Vector3(minX, minY, 0);


        Vector3[] verts = new Vector3[]
        {
            cb.pointA,
            cb.pointB,
            cb.pointC,
            cb.pointD,
        };

        // Store offsets for each vertex
        Vector3[] offsets = new Vector3[]
        {
            cb.pointA,
            cb.pointB,
            cb.pointC,
            cb.pointD,
        };

        var sum = Vector3.zero;
        for (int i = 0; i < verts.Length; i++)
        {
            // Transform to isometric
            verts[i] = IsometricGrid.IsoProject(verts[i]);
            var newVert = verts[i];

            // Move vertex and record offset
            newVert = Handles.Slider2D(newVert, Vector3.forward, Vector3.right, Vector3.up, wpSize, Handles.CircleHandleCap, 0.1f);
            offsets[i] = newVert - verts[i];

            // Set moved vertex to array and sum for center calculation
            verts[i] = newVert;
            sum += verts[i];
        }

        // Compute rectangle center movement
        var oldCenter = sum / 4;
        var newCenter = Handles.Slider2D(oldCenter, Vector3.forward, Vector3.right, Vector3.up, wpSize, Handles.CircleHandleCap, 0.1f);
        var rawOffset = newCenter - oldCenter;


        // Compute combined vertex offsets for border adjustment
        maxY = offsets[0].y + offsets[1].y;
        maxX = offsets[1].x + offsets[2].x;
        minY = offsets[2].y + offsets[3].y;
        minX = offsets[3].x + offsets[0].x;


        // Apply center movement and individual offsets to each corner
        cb.pointA += rawOffset + new Vector3(minX, maxY);
        cb.pointB += rawOffset + new Vector3(maxX, maxY);

        cb.pointC += rawOffset + new Vector3(maxX, minY);
        cb.pointD += rawOffset + new Vector3(minX, minY);


        // Draw the rectangle
        Handles.DrawSolidRectangleWithOutline(verts, cb.guiColour, Color.white);
    }
}

