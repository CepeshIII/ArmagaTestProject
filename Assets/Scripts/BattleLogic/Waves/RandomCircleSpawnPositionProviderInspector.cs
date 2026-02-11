using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomCircleSpawnPositionProvider))]
public class RandomCircleSpawnPositionProviderInspector : Editor
{
    private RandomCircleSpawnPositionProvider cb;

    private void OnEnable()
    {
        cb = (RandomCircleSpawnPositionProvider)target;
    }

    private void OnSceneGUI()
    {
        Handles.color = Color.white;

        EditorGUI.BeginChangeCheck();

        float newRadius = Handles.RadiusHandle(
            Quaternion.identity,
            cb.transform.position,
            cb.MaxRadius
        );

        Handles.color = Color.red;
        var newCenter = Handles.Slider2D(cb.transform.position,
            Vector3.forward,
            Vector3.right,
            Vector3.up,
            cb.MaxRadius * 0.5f,
            Handles.CircleHandleCap,
            0.1f
        );

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(cb, "Modify Spawn Area");
            cb.transform.position = newCenter;
            cb.SetMaxRadius(Mathf.Max(0f, newRadius));
            EditorUtility.SetDirty(cb);
        }

        Handles.DrawWireDisc(
            cb.transform.position,
            Vector3.up,
            cb.MaxRadius
        );


    }
}



