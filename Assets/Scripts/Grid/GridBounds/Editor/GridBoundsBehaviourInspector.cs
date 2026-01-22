using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GridBoundsBehaviour))]
public class GridBoundsBehaviourInspector : Editor
{
    GridBoundsBehaviour cb;
    private Rect boundArea;

    private const float wpSize = 0.1f;


    private void OnEnable()
    {
        cb = (GridBoundsBehaviour)target;
    }


    public override void OnInspectorGUI()
    {
        if (cb != null)
        {
            DrawDefaultInspector();
        }
    }


    private void OnSceneGUI()
    {
        if (cb != null)
        {
            DrawBorderHandles();

            if (GUI.changed)
            {
                Undo.RecordObject(cb, "Modify Grid Bounds");
                EditorUtility.SetDirty(cb);
            }
        }
    }


    // Draw and edit the rectangle defined by gridBounds points (A, B, C, D).
    // Points are stored in rectangular space but displayed in isometric space.
    // This allows accurate visual editing while maintaining correct rectangle ratios.
    public void DrawBorderHandles()
    {
        var bounds = cb.GetGridBounds();
        var newBounds = new GridBounds();

        // Reorder points to make sure A is top-left, B is top-right, C is bottom-right, D is bottom-left of the rectangle
        var maxY = Mathf.Max(bounds.pointA.y, bounds.pointB.y, bounds.pointC.y, bounds.pointD.y);
        var maxX = Mathf.Max(bounds.pointA.x, bounds.pointB.x, bounds.pointC.x, bounds.pointD.x);

        var minY = Mathf.Min(bounds.pointA.y, bounds.pointB.y, bounds.pointC.y, bounds.pointD.y);
        var minX = Mathf.Min(bounds.pointA.x, bounds.pointB.x, bounds.pointC.x, bounds.pointD.x);

        newBounds.pointA = new Vector3(minX, maxY, 0);
        newBounds.pointB = new Vector3(maxX, maxY, 0);

        newBounds.pointC = new Vector3(maxX, minY, 0);
        newBounds.pointD = new Vector3(minX, minY, 0);

        Vector3[] verts = new Vector3[]
        {
            newBounds.pointA,
            newBounds.pointB,
            newBounds.pointC,
            newBounds.pointD,
        };

        // Store offsets for each vertex
        Vector3[] offsets = new Vector3[]
        {
            newBounds.pointA,
            newBounds.pointB,
            newBounds.pointC,
            newBounds.pointD,
        };

        var sum = Vector3.zero;
        for (int i = 0; i < verts.Length; i++)
        {
            // Transform to isometric
            verts[i] = IsoMath.IsoProject(verts[i]);
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
        newBounds.pointA += rawOffset + new Vector3(minX, maxY);
        newBounds.pointB += rawOffset + new Vector3(maxX, maxY);

        newBounds.pointC += rawOffset + new Vector3(maxX, minY);
        newBounds.pointD += rawOffset + new Vector3(minX, minY);

        cb.SetGridBounds(newBounds);

        // Draw the rectangle
        Handles.DrawSolidRectangleWithOutline(verts, cb.GetColor(), Color.white);

    }
}


