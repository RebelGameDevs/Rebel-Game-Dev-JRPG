using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

// https://discussions.unity.com/t/custom-inspector-if-bool-is-true-then-show-variable/178698
namespace RebelGameDevs.Extra
{
    public class RGD_SkyboxControl : MonoBehaviour
    {
        const int RED = 0;
        const int GREEN = 1;
        const int BLUE = 2;
        const int ALPHA = 3;
        const int MAX_TARGET_OUTPUTS = 4;
        [Tooltip("The skybox that will be handled.")]
        [SerializeField] private Material baseSkybox;
        public bool outputValuesToTextMeshes = true;
        [Tooltip("TextMeshPro outputs for the RGBA values (0123 for RGBA respectively).")]
        [SerializeField] private Transform[] targetOutputs = new Transform[MAX_TARGET_OUTPUTS];
        private TextMeshPro[] textMeshes = new TextMeshPro[MAX_TARGET_OUTPUTS];
        private Material newMaterial;

        public Color tintColor;

        private void Awake() {
            // skybox
            newMaterial = new Material(baseSkybox);
            tintColor = newMaterial.GetColor("_Tint");
            RenderSettings.skybox = newMaterial;
            // text meshes
            for(int i=0; i<MAX_TARGET_OUTPUTS; i++) {
                textMeshes[i] = targetOutputs[i].GetComponent<TextMeshPro>();
            }
        }

        private void Update() {
            if(outputValuesToTextMeshes) {
                UpdateMeshes();
            }
        }

        // methods
        // allow other scripts to update the skybox after handling newMaterial properties
        public void UpdateSkybox() {
            newMaterial.SetColor("_Tint", tintColor);
            RenderSettings.skybox = newMaterial;
        }

        private void UpdateMeshes() {
            textMeshes[RED].text = tintColor.r.ToString("0.0");
            textMeshes[GREEN].text = tintColor.g.ToString("0.0");
            textMeshes[BLUE].text = tintColor.b.ToString("0.0");
            textMeshes[ALPHA].text = tintColor.a.ToString("0.0");
        }
    }

    // refer to "Assets\RGD_Core\Utilities\Scripts\RebelGameDev_Instancer.cs"
    // NOTE: using this means you need to redefine every field accessible in the Inspector in
    //  this Editor class
    #if UNITY_EDITOR
    [CustomEditor(typeof(RGD_SkyboxControl))]
    class SkyboxControl_Editor : Editor
    {
        //variables:
        SerializedProperty targetOutputs;
        SerializedProperty outputValuesToTextMeshes;
        SerializedProperty baseSkybox;
        private void OnEnable()
        {
            targetOutputs = serializedObject.FindProperty("targetOutputs");
            outputValuesToTextMeshes = serializedObject.FindProperty("outputValuesToTextMeshes");
            baseSkybox = serializedObject.FindProperty("baseSkybox");
        }
        public override void OnInspectorGUI()
        {
        //Check For Script:
            var component = (RGD_SkyboxControl)target;
            if (component == null) return;
            Undo.RecordObject(component, "Change Component");

            //Custom Inspector:
            serializedObject.Update();
            EditorGUILayout.PropertyField(baseSkybox);
            EditorGUILayout.PropertyField(outputValuesToTextMeshes);
            // will only show target outputs if checked
            if(component.outputValuesToTextMeshes) {
                EditorGUILayout.PropertyField(targetOutputs);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
