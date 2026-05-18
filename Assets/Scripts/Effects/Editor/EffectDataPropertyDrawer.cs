using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EffectData), true)]
public class EffectDataPropertyDrawer : PropertyDrawer
{
    private const int LINE_HEIGHT = 18;
    private const int SPACING = 4;
    private const int INDENT_WIDTH = 15;
    private const int LABEL_WIDTH = 80;

    private static readonly GUIContent EFFECT_VALUE_LABEL = new("Effect Value");
    private static readonly GUIContent FILTER_LABEL = new("Filter");
    private static readonly GUIContent STACK_TYPE_LABEL = new("Stack Type");
    private static readonly GUIContent STAT_TARGET_LABEL = new("Stat Target");
    private static readonly GUIContent AREA_TYPE_LABEL = new("Area Type");
    private static readonly GUIContent RANGE_LABEL = new("Range");
    private static readonly GUIContent EFFECT_TYPE_LABEL = new("Effect Type");

    private bool _showTargeting = true;
    private bool _showEffectDetails = true;
    private bool _showModifiers = true;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float line = LINE_HEIGHT + SPACING;

        // Each foldout header
        float targetingSection = LINE_HEIGHT + SPACING; // foldout header
        targetingSection += LINE_HEIGHT + SPACING; // effect area (1 line)
        targetingSection += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("filter"), GUIContent.none, true) + SPACING;

        float effectDetailsSection = LINE_HEIGHT + SPACING; // foldout header
        effectDetailsSection += LINE_HEIGHT + SPACING; // effectValue
        effectDetailsSection += LINE_HEIGHT + SPACING; // effectType

        float modifiersSection = LINE_HEIGHT + SPACING; // foldout header
        modifiersSection += LINE_HEIGHT + SPACING; // stackType
        modifiersSection += LINE_HEIGHT + SPACING; // statTarget

        return targetingSection + effectDetailsSection + modifiersSection;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = DrawFoldoutGroup(position, property, "Targeting", ref _showTargeting, DrawTargetingSection);
        position = DrawFoldoutGroup(position, property, "Effect Details", ref _showEffectDetails, DrawEffectDetailsSection);
        position = DrawFoldoutGroup(position, property, "Modifiers", ref _showModifiers, DrawModifiersSection);

        EditorGUI.EndProperty();
    }

    private Rect DrawFoldoutGroup(Rect position, SerializedProperty property, string label, ref bool isExpanded, System.Func<Rect, SerializedProperty, Rect> drawContent)
    {
        position.height = LINE_HEIGHT;
        isExpanded = EditorGUI.Foldout(position, isExpanded, label, true);

        if (isExpanded)
        {
            position.y += LINE_HEIGHT + SPACING;
            EditorGUI.indentLevel++;
            position = drawContent(position, property);
            EditorGUI.indentLevel--;
        }
        else
        {
            position.y += LINE_HEIGHT + SPACING;
        }

        return position;
    }

    private Rect DrawTargetingSection(Rect position, SerializedProperty property)
    {
        position = DrawEffectArea(position, property);
        position = DrawFilter(position, property);
        return position;
    }

    private Rect DrawEffectDetailsSection(Rect position, SerializedProperty property)
    {
        position = DrawEffectValue(position, property);
        position = DrawEffectType(position, property);
        return position;
    }

    private Rect DrawModifiersSection(Rect position, SerializedProperty property)
    {
        position = DrawField(position, property, "stackType", STACK_TYPE_LABEL);
        position = DrawField(position, property, "statTarget", STAT_TARGET_LABEL);
        return position;
    }

    private Rect DrawEffectArea(Rect position, SerializedProperty property)
    {
        var effectArea = property.FindPropertyRelative("effectArea");
        var areaType = effectArea.FindPropertyRelative("areaType");
        var range = effectArea.FindPropertyRelative("range");

        position.height = LINE_HEIGHT;

        if ((EffectAreaType)areaType.enumValueIndex == EffectAreaType.Radius)
            position = DrawTwoFields(position, areaType, AREA_TYPE_LABEL, range, RANGE_LABEL);
        else
            position = DrawSingleField(position, areaType, AREA_TYPE_LABEL);

        return position;
    }

    private Rect DrawFilter(Rect position, SerializedProperty property)
    {
        var filter = property.FindPropertyRelative("filter");
        var height = EditorGUI.GetPropertyHeight(filter, GUIContent.none, true);
        position.height = height;
        position = DrawPropertyWithLabel(position, filter, FILTER_LABEL);
        return position;
    }

    private Rect DrawEffectValue(Rect position, SerializedProperty property)
    {
        return DrawField(position, property, "effectValue", EFFECT_VALUE_LABEL);
    }

    private Rect DrawEffectType(Rect position, SerializedProperty property)
    {
        var filter = property.FindPropertyRelative("filter");
        var targetType = filter.FindPropertyRelative("targetType");
        string fieldName = (EffectTarget)targetType.enumValueIndex == EffectTarget.Unit ? "unitEffectType" : "buildingEffectType";
        return DrawField(position, property, fieldName, EFFECT_TYPE_LABEL);
    }

    private Rect DrawField(Rect position, SerializedProperty property, string fieldName, GUIContent label)
    {
        position.height = LINE_HEIGHT;
        var fieldProp = property.FindPropertyRelative(fieldName);
        var fieldRect = new Rect(position.x, position.y, position.width, LINE_HEIGHT);
        EditorGUI.PropertyField(fieldRect, fieldProp, label, false);
        position.y += LINE_HEIGHT + SPACING;
        return position;
    }

    private Rect DrawPropertyWithLabel(Rect position, SerializedProperty prop, GUIContent label)
    {
        var labelRect = new Rect(position.x, position.y, LABEL_WIDTH, LINE_HEIGHT);
        var fieldRect = new Rect(position.x + LABEL_WIDTH, position.y, position.width - LABEL_WIDTH, LINE_HEIGHT);
        EditorGUI.LabelField(labelRect, label);
        EditorGUI.PropertyField(fieldRect, prop, GUIContent.none, false);
        position.y += EditorGUI.GetPropertyHeight(prop) + SPACING;
        return position;
    }

    private Rect DrawTwoFields(Rect position, SerializedProperty prop1, GUIContent label1, SerializedProperty prop2, GUIContent label2)
    {
        float halfWidth = (position.width - SPACING) / 2f;

        var label1Rect = new Rect(position.x, position.y, LABEL_WIDTH, LINE_HEIGHT);
        var field1Rect = new Rect(position.x + LABEL_WIDTH, position.y, halfWidth - LABEL_WIDTH, LINE_HEIGHT);
        var label2Rect = new Rect(position.x + halfWidth, position.y, LABEL_WIDTH, LINE_HEIGHT);
        var field2Rect = new Rect(position.x + halfWidth + LABEL_WIDTH, position.y, halfWidth - LABEL_WIDTH, LINE_HEIGHT);

        EditorGUI.LabelField(label1Rect, label1);
        EditorGUI.PropertyField(field1Rect, prop1, GUIContent.none, false);
        EditorGUI.LabelField(label2Rect, label2);
        EditorGUI.PropertyField(field2Rect, prop2, GUIContent.none, false);

        position.y += LINE_HEIGHT + SPACING;
        return position;
    }

    private Rect DrawSingleField(Rect position, SerializedProperty prop, GUIContent label)
    {
        var labelRect = new Rect(position.x, position.y, LABEL_WIDTH, LINE_HEIGHT);
        var fieldRect = new Rect(position.x + LABEL_WIDTH, position.y, position.width - LABEL_WIDTH, LINE_HEIGHT);

        EditorGUI.LabelField(labelRect, label);
        EditorGUI.PropertyField(fieldRect, prop, GUIContent.none, false);

        position.y += LINE_HEIGHT + SPACING;
        return position;
    }
}
