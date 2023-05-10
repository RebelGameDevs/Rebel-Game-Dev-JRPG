using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class RebelGameDev_Instancer : MonoBehaviour
{
    public Material material;
    public Material baseMaterial;
    [ColorUsage(true,false)] 
    public Color colorToInstance;
    [ColorUsage(true,true)]
    public Color ColorToInstanceEmmision;
}
#if UNITY_EDITOR
[CustomEditor(typeof(RebelGameDev_Instancer))]
class RebelGameDevInstancer_Editor : Editor
{

    //variables:
    public Material instancedMaterial;
    public Renderer renderer;
    SerializedProperty colorToInstance;
    SerializedProperty ColorToInstanceEmmision;
    SerializedProperty baseMaterial;
    private void OnEnable()
    {
        colorToInstance = serializedObject.FindProperty("colorToInstance");
        ColorToInstanceEmmision = serializedObject.FindProperty("ColorToInstanceEmmision");
        baseMaterial = serializedObject.FindProperty("baseMaterial");
    }
    public override void OnInspectorGUI()
    {
    //Check For Script:
        var component = (RebelGameDev_Instancer)target;
        if (component == null) return;
        Undo.RecordObject(component, "Change Component");

        //Custom Inspector:
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("")) { }
        GUI.backgroundColor = Color.clear;
        Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/RGD_Core/Utilities/Images/Banner.png", typeof(Texture));
        GUILayout.Box(banner, GUILayout.Width(495), GUILayout.Height(165));

        //Serialize Property Variables:
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(colorToInstance);
        EditorGUILayout.PropertyField(baseMaterial);
        serializedObject.ApplyModifiedProperties();

        if(component.baseMaterial == null) return;
        if(component.baseMaterial.IsKeywordEnabled("_EMISSION")) EditorGUILayout.PropertyField(ColorToInstanceEmmision);
        else EditorGUILayout.LabelField("Current Material Does Not Have Keyword _EMISSION enabled");
        
        serializedObject.ApplyModifiedProperties();

        //Variables:
        
        instanceMaterial(component);

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("")) { }
    }
    public void instanceMaterial(RebelGameDev_Instancer component)
    {
        if(component.baseMaterial == null) return;
        if(Application.isPlaying) return;
        if(renderer == null) renderer = component.GetComponent<Renderer>();

        if(instancedMaterial == null) instancedMaterial = new Material(component.baseMaterial);
        instancedMaterial.color = component.colorToInstance;
        if(component.baseMaterial.IsKeywordEnabled("_EMISSION")) instancedMaterial.SetColor("_EmissionColor", component.ColorToInstanceEmmision);

        //Set Material 
        renderer.material = instancedMaterial;
    }

}
#endif