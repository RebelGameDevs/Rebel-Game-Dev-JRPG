/** To disable the foldout region - Comment the define's line **/
#define FOLDOUT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PrettyObject))]
[CanEditMultipleObjects]
public class FoldoutUsage : Editor
{
#if FOLDOUT
    bool showPosition = false;
    string status = "Text Style";
#endif
    SerializedProperty font;
    SerializedProperty fontStyle;
    SerializedProperty alignment;

    PrettyObject prettyObject;
    void OnEnable()
    {
        prettyObject = (PrettyObject)target;

        /// Fetch the objects from the GameObject script to display in the inspector
        //prettyObject.backgroundColor = EditorColors.Background;
        //prettyObject.textColor = EditorColors.Text;
        //prettyObject.alignment = TextAnchor.MiddleLeft;

        font = serializedObject.FindProperty("font");
        fontStyle = serializedObject.FindProperty("fontStyle");
        alignment = serializedObject.FindProperty("alignment");

    }
    public override void OnInspectorGUI()
    {
        prettyObject.backgroundColor = EditorGUILayout.ColorField("BackgroundColor", prettyObject.backgroundColor);
#if FOLDOUT
//to diable the fold out comment line #2 in this script
        showPosition = EditorGUILayout.Foldout(showPosition, status);
        if (showPosition)
        {
#else
        GUILayout.Label("", GUI.skin.horizontalSlider);

#endif
        EditorGUILayout.PropertyField(font, new GUIContent("Font"));
        prettyObject.textColor = EditorGUILayout.ColorField("Text Color", prettyObject.textColor);
        prettyObject.fontSize = EditorGUILayout.IntSlider("Font Size", prettyObject.fontSize, 1, 17);
        EditorGUILayout.PropertyField(fontStyle, new GUIContent("Font Style"));
        EditorGUILayout.PropertyField(alignment, new GUIContent("Alignment"));
        prettyObject.textDropShadow = EditorGUILayout.Toggle("Drop Shadow", prettyObject.textDropShadow);
#if FOLDOUT
        }
#endif

        if (GUILayout.Button("Reset Colors"))
        {
            prettyObject.backgroundColor = EditorColors.Background;
            prettyObject.textColor = EditorColors.Text;
            prettyObject.fontSize = 12;
            prettyObject.fontStyle = FontStyle.Normal;
            prettyObject.alignment = TextAnchor.MiddleLeft;
        }
        serializedObject.ApplyModifiedProperties();

        //base.OnInspectorGUI();
    }

}

