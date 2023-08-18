namespace RebelGameDevs.Utils
{
    #if UNITY_EDITOR
    using UnityEngine;
    using UnityEditor;

    public static class RebelGameDevsEditorHelpers
    {
	    public static void DrawImage(string location, float width, float height)
	    {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.clear;

		    Texture banner = (Texture)AssetDatabase.LoadAssetAtPath(location, typeof(Texture));

		    EditorGUILayout.BeginHorizontal();
 
            EditorGUILayout.Space();
 
            GUILayout.Box(banner, GUILayout.Width(Screen.width / 1.5f), GUILayout.Height(Screen.width / 4f));
 
            EditorGUILayout.Space();
 
            EditorGUILayout.EndHorizontal();
 
            GUI.backgroundColor = oldColor;
	    }
    }
    #endif
}
