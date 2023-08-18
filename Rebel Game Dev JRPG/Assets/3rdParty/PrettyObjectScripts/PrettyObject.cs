using UnityEngine;
using UnityEditor;

[DisallowMultipleComponent]
[AddComponentMenu("Pretty Objects/Pretty Object")]
public class PrettyObject : MonoBehaviour
{
    /*
         TextAnchor alignment
         bool textDropShadow
         */
    [Header("Background")]
    [SerializeField]
    public  Color32 backgroundColor = new Color32(255, 255, 255, 255);
    [Header("Text")]
    [SerializeField]
    public Font font;
    [SerializeField]
    public Color32 textColor = new Color32(255, 255, 255, 255);
    [SerializeField]
    [Range(1,16)]
    public int fontSize = 12;
    [SerializeField]
    public FontStyle fontStyle = FontStyle.Normal;
    [SerializeField]
    public TextAnchor alignment = TextAnchor.UpperLeft;
    [SerializeField]
    public bool textDropShadow;

    public Color32 BackgroundColor { get { return new Color32(backgroundColor.r, backgroundColor.g, backgroundColor.b, 255); } set { } }
    public Font Font { get { return font; } set { } }
    public Color32 TextColor { get { return textColor; } set { } }
    public int FontSize { get { return fontSize; } set { } }
    public FontStyle FontStyle { get { return fontStyle; } set { } }
    public TextAnchor Alignment { get { return alignment; } set { } }
    public bool TextDropShadow { get { return textDropShadow; } set { } }

    private void OnValidate()
    {
        EditorApplication.RepaintHierarchyWindow();
    }

    [ContextMenu("Edit Editor Window")]
    void DoSomething()
    {
        string lPath = "FoldoutUsage.cs";
        foreach (var lAssetPath in AssetDatabase.GetAllAssetPaths())
        {
            if (lAssetPath.EndsWith(lPath))
            {
                var lScript = (MonoScript)AssetDatabase.LoadAssetAtPath(lAssetPath, typeof(MonoScript));
                if (lScript != null)
                {
                    AssetDatabase.OpenAsset(lScript,2);
                    break;
                }
            }
        }
    }
}