using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(EffectData), true)]
public class EffectDataPropertyDrawer : PropertyDrawer
{
    private const int SPACING = 4;

    public UnitEffect itemInfo;


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = 0f;
        float line = EditorGUIUtility.singleLineHeight + SPACING;

        // Effect Area
        height += 2 * line;

        // Target
        height += line;

        // Unit/Building Effect
        height += 2 * line;

        return height;
    }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        float y = position.y;
        float line = EditorGUIUtility.singleLineHeight + SPACING;


        // Draw EffectArea information
        var effectAreaRect = new Rect(position.x, y, position.width, line * 2);
        DrawEffectAreaInformation(effectAreaRect, property, new Vector2(10f, 0f));
        y += line * 2;


        // Draw effectTarget information
        var effectTargetProp = property.FindPropertyRelative("effectTarget");
        var effectTargetRect = new Rect(position.x, y, position.width, line);
        EditorGUI.PropertyField(effectTargetRect, effectTargetProp);
        y += line;

        
        //Draw unitEffectType or buildingEffectType
        var effectTypeRect = new Rect(position.x, y, position.width, line);
        if ((EffectTarget)effectTargetProp.enumValueIndex == EffectTarget.Unit)
        {
            var unitEffectProp = property.FindPropertyRelative("unitEffectType");
            EditorGUI.PropertyField(effectTypeRect, unitEffectProp, true);
        }
        else
        {
            var buildingEffectProp = property.FindPropertyRelative("buildingEffectType");
            EditorGUI.PropertyField(effectTypeRect, buildingEffectProp, true);
        }
        y += line;

        // Draw Effect Value Property
        var effectValueProp = property.FindPropertyRelative("effectValue");
        var effectValueRect = new Rect(position.x, y, position.width, line);
        EditorGUI.PropertyField(effectValueRect, effectValueProp, new GUIContent("Effect Type"), true);
        y += line;
    }


    private void DrawLineWithHeaders(SerializedProperty property1, string name1,
        SerializedProperty property2, string name2, Rect rect, Vector2 spacing = default)
    {
        // calculating half size for calculating position and size of 2 Props
        var rectHalfSize = rect.size / 2f;

        // apply spacing
        rectHalfSize -= spacing;

        // creating rect's
        Rect prop1Header = rect;
        Rect prop1 = rect;
        Rect prop2Header = rect;
        Rect prop2 = rect;

        // Set width
        prop1Header.width = rectHalfSize.x;
        prop1.width = rectHalfSize.x;
        prop2Header.width = rectHalfSize.x;
        prop2.width = rectHalfSize.x;

        // Set height
        prop1Header.height = rectHalfSize.y;
        prop1.height = rectHalfSize.y;
        prop2Header.height = rectHalfSize.y;
        prop2.height = rectHalfSize.y;

        // Set position
        prop2Header.x += rectHalfSize.x + spacing.x;
        prop1.y += rectHalfSize.y + spacing.y;

        prop2.x += rectHalfSize.x + spacing.x;
        prop2.y += rectHalfSize.y + spacing.y;

        if (property1 != null)
        {
            EditorGUI.LabelField(prop1Header, name1);
            EditorGUI.PropertyField(prop1, property1, GUIContent.none);
        }
        if (property2 != null)
        {
            EditorGUI.LabelField(prop2Header, name2);
            EditorGUI.PropertyField(prop2, property2, GUIContent.none);
        }
    }


    private void DrawEffectAreaInformation(Rect effectAreaRect, SerializedProperty property, Vector2 spacing = default)
    {
        // Get properties
        var effectAreaProp = property.FindPropertyRelative("effectArea");
        var effectAreaTypeProp = effectAreaProp.FindPropertyRelative("areaType");
        var effectAreaRadiusProp = effectAreaProp.FindPropertyRelative("range");

        if ((EffectAreaType)effectAreaTypeProp.enumValueIndex == EffectAreaType.Radius)
        {
            DrawLineWithHeaders(effectAreaTypeProp, "Area type", effectAreaRadiusProp, "Range", effectAreaRect, spacing);
        }
        else
        {
            DrawLineWithHeaders(effectAreaTypeProp, "Area type", null, "null", effectAreaRect, spacing);
        }
    }
}