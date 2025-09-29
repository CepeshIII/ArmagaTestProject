using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class InvisibleGraphic : Graphic
{
    public override bool Raycast(Vector2 sp, Camera eventCamera)
    {
        Debug.Log("Raycast");
        //return base.Raycast(sp, eventCamera);
        return true;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        // We don't want to draw anything
        vh.Clear();
    }

    protected override void OnFillVBO(List<UIVertex> vbo)
    {
        //base.OnFillVBO(vbo);
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(InvisibleGraphic))]
    public class InvisibleGraphicEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // nothing
        }
    }
#endif
}