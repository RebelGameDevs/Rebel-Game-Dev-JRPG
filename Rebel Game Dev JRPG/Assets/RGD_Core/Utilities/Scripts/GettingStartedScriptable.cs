using RebelGameDevs.Utils.Input;
using UnityEditor;
using UnityEngine;
using RebelGameDevs.Utils;
using UnityEngine.Tilemaps;
using System.ComponentModel;

[CreateAssetMenu(fileName = "Click Me To Getting Started", menuName = "RGDCore/Utils/Create A Getting Started Scriptable")]public class GettingStartedScriptable : ScriptableObject
{
	[HideInInspector] public readonly string rgdWebsiteLink = "http://rebelgamedevs.org";
	[HideInInspector] public readonly string rgdDocsLink = "http://rebelgamedevs.org/Documentation/documentation.html";
	public void OpenLink(string url)
	{
		if(url is null || string.IsNullOrEmpty(url)) return;
		Application.OpenURL(url);
	}
}
#if UNITY_EDITOR
[CustomEditor(typeof(GettingStartedScriptable))]
class GettingStartedScriptableEditor : Editor
{
    private void OnEnable()
    {
        GettingStartedScriptableEditorWindow.OpenScriptableObjectWindow((GettingStartedScriptable)target);
    }

}
public class GettingStartedScriptableEditorWindow : EditorWindow
{
    //My Toolbar:
    string[] titles = new string[] { "Rebel Game Devs Website", "Open Source Docs", "Close"};
    private int currentTitleSelected = -1;
    
    public static void OpenScriptableObjectWindow(GettingStartedScriptable scriptableObject)
    {
        var window = CreateInstance<GettingStartedScriptableEditorWindow>();
        window.titleContent = new GUIContent("Getting Started Window");
        window.scriptableObject = scriptableObject;

        window.Show();
        CenterWindow(window);
    }
    private static void CenterWindow(EditorWindow window)
    {
        Rect position = window.position;
        position.center = new Rect(0f, 0f, Screen.currentResolution.width, Screen.currentResolution.height).center;
        window.position = position;
        window.maxSize = new Vector2(Screen.currentResolution.width * .8f, Screen.currentResolution.height * .75f);
        window.minSize = window.maxSize;
    }

    private GettingStartedScriptable scriptableObject;
    private void OnTitleClicked(GettingStartedScriptable script)
    {
        switch(currentTitleSelected)
        {
            case 0:
                script.OpenLink(script.rgdWebsiteLink);
                break;
            case 1:
                script.OpenLink(script.rgdDocsLink);
                break;
            case 2:
                Close();
                break;
            case 3:
                break;
            default:
                //None selected:
                break;
        }
    }

    private void OnGUI()
    {
                //Check For Script:
        var component = scriptableObject;
        if (component == null) return;
        Undo.RecordObject(component, "Change Component");
        RebelGameDevsEditorHelpers.DrawImage("Assets/RGD_Core/Utilities/Images/Banner_Utility.png", 0.85f, 0.3f);

        GUI.color = Color.white;
        GUIStyle buttonStyles = new GUIStyle(GUI.skin.button);
        buttonStyles.fixedHeight = 120;
        buttonStyles.fontStyle = FontStyle.Bold;
        buttonStyles.fontSize = 24;
        buttonStyles.wordWrap = true;
        for (int i = 0; i < titles.Length; i += 2)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if(i == 0)  GUI.backgroundColor = new Color(0, .8f, 1);
            else GUI.backgroundColor = Color.red;
            if (GUILayout.Button(titles[i].ToString(), buttonStyles, GUILayout.Width(Screen.width / 2.25f)))
            {
                currentTitleSelected = i;
                OnTitleClicked(component);
            }
            if(i == 0)  GUI.backgroundColor = new Color(0, .8f, 1);
            if (i + 1 < titles.Length) // Check if there's a next button
            {
                if (GUILayout.Button(titles[i + 1].ToString(), buttonStyles, GUILayout.Width(Screen.width / 2.25f)))
                {
                    currentTitleSelected = i + 1;
                    OnTitleClicked(component);
                }
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        GUI.color = Color.white;
    }
}
#endif