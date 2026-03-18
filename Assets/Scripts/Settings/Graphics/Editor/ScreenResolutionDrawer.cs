using UnityEngine;

using UnityEditor;

[CustomPropertyDrawer(typeof(ScreenResolution))]
public class ScreenResolutionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var aspectProp = property.FindPropertyRelative("aspectRatio");
        var widthProp = property.FindPropertyRelative("Width");

        float line = EditorGUIUtility.singleLineHeight;
        float spacing = 2;

        Rect r1 = new Rect(position.x, position.y, position.width, line);
        Rect r2 = new Rect(position.x, position.y + line + spacing, position.width, line);
        Rect r3 = new Rect(position.x, position.y + (line + spacing) * 2, position.width, line);

        EditorGUI.PropertyField(r1, aspectProp);
        EditorGUI.PropertyField(r2, widthProp);

        AspectRatio ratio = (AspectRatio)aspectProp.enumValueIndex;
        int width = widthProp.intValue;

        int height = AspectRatioUtility.GetHeight(ratio, width);

        Vector2Int r = AspectRatioUtility.GetRatio(ratio);

        string preview = $"Resolution: {r.x}x{r.y} - {width}x{height}";

        EditorGUI.LabelField(r3, preview, EditorStyles.helpBox);

        EditorGUI.EndProperty();
    }
}
