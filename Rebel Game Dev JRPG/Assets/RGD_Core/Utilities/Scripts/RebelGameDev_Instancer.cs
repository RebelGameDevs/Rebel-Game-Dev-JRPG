#if UNITY_EDITOR
using UnityEditor;
using static RebelGameDevs.Utils.RebelGameDevsEditorHelpers;
#endif
using UnityEngine;
namespace RebelGameDevs.HelperComponents
{
	public class RebelGameDev_Instancer : MonoBehaviour
    {
        public bool something;
        public Material material;
        public Material baseMaterial;
        [ColorUsage(true,false)] 
        public Color colorToInstance;
        [ColorUsage(true,true)]
        public Color ColorToInstanceEmmission;
        private MeshRenderer objectsRenderer;
        //Should Only Be Called in game not in editor scripts
        private void Awake()
        {
            objectsRenderer = GetComponent<MeshRenderer>();
        }
        /*
        ========================================================================================================================
        InstanceANewColor():
    
        Params:
            None,   
    
        Description:
            - Creates a new Material when called. It will also check for emission and if applicable also use this. Before calling
              make sure to change the colorToInstance or ColorToInstanceEmision. Also this will immediately leave if you call
              this method in an editor script as it can cause a memory leak if you do so. So to combat this you will have to make
              your own methods like the editor script for the base RebelGameDev_Instancer.cs to make changes in the editor. 

        Notes:
            Should only be called in runtime (this is pretty performant as it creates a new material) 
            so never use this on a frame by frame basis.
        ========================================================================================================================
        */
    
        public void InstanceANewColor()
        {
            //Don't continue if the game is not running (makes it so this cannot be runned outside game time):
            if(!Application.isPlaying)return;

            //Instance a new material:
            Material newMat = new Material(baseMaterial);
            newMat.color = colorToInstance;
            if(newMat.IsKeywordEnabled("_EMISSION")) newMat.SetColor("_EmissionColor", ColorToInstanceEmmission);

            //Assign material:
            if(objectsRenderer == null) objectsRenderer = gameObject.GetComponent<MeshRenderer>();
            objectsRenderer.material = newMat;
        }
        /*
        ========================================================================================================================
        InstanceANewColorBase(Color colorToChange):
    
        Params:
            colorToChange - Color, the color to change the material to.  
    
        Description:
            - Creates a new Material when called. This will immediately leave if you call this method in an editor script as it 
              can cause a memory leak if you do so. So to combat this you will have to make your own methods like the editor 
              script for the base RebelGameDev_Instancer.cs to make changes in the editor. 

        Notes:
            Should only be called in runtime (this is pretty performant as it creates a new material) 
            so never use this on a frame by frame basis.
        ========================================================================================================================
        */
        public void InstanceANewColorBase(Color color)
        {
            //Don't continue if the game is not running (makes it so this cannot be runned outside game time):
            if(!Application.isPlaying)return;

            //Instance a new material:
            Material newMat = new Material(baseMaterial);
            colorToInstance = color;
            newMat.color = color;

            //Assign material:
            if(objectsRenderer == null) objectsRenderer = gameObject.GetComponent<MeshRenderer>();
            objectsRenderer.material = newMat;
        }
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
            ColorToInstanceEmmision = serializedObject.FindProperty("ColorToInstanceEmmission");
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
            DrawImage("Assets/RGD_Core/Utilities/Images/Banner_Utility.png", 0.85f, 0.3f);

            //Serialize Property Variables:
            serializedObject.Update();
        
            EditorGUILayout.PropertyField(colorToInstance);
            EditorGUILayout.PropertyField(baseMaterial);

            if (component.baseMaterial == null) { serializedObject.ApplyModifiedProperties(); return; }
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
            if(component.baseMaterial.IsKeywordEnabled("_EMISSION")) instancedMaterial.SetColor("_EmissionColor", component.ColorToInstanceEmmission);

            //Set Material 
            renderer.material = instancedMaterial;
        }

    }
    #endif
}
