namespace RebelGameDevs.Interaction
{
    #if UNITY_EDITOR
    using UnityEditor;
    using static RebelGameDevs.Utils.RebelGameDevsEditorHelpers;
    #endif
    using UnityEngine;
    [CreateAssetMenu(menuName = "Rebel Game Devs/Core/Create A New Hand Held", fileName = "New_Hand_Held")]
    public class RGD_S_BaseHandHeld : ScriptableObject
    {
        //Name of Hand Held:
        public string nameOfHandHeld = "None";

        //How Far the item can detect rayCasts:
        public float raycastLength = 5f;

        //What type of raycast the hand held can do
        public bool canLookRayCast = true;
        public bool canInteractRayCast = true;

        //Messenger Methods:
        public virtual bool CanLookAtMessenger()       { return canLookRayCast; }
        public virtual bool CanInteractWithMessenger() { return canInteractRayCast; }
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(RGD_S_BaseHandHeld))]
    class RGD_S_BaseHandHeldEditor : Editor
    {

        SerializedProperty NameOfHandHeld;
        SerializedProperty RaycastLength;
        private void OnEnable()
        {
            NameOfHandHeld = serializedObject.FindProperty("nameOfHandHeld");
            RaycastLength = serializedObject.FindProperty("raycastLength");
        }
        public override void OnInspectorGUI()
        {
        //Check For Script:
            var component = (RGD_S_BaseHandHeld)target;
            if (component == null) return;
            Undo.RecordObject(component, "Change Component");

            //Custom Inspector:
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("")) { }
            DrawImage("Assets/RGD_Core/Utilities/Images/Banner_ScriptableObject.png", 0.85f, 0.3f);

            //Serialize Property Variables:
            serializedObject.Update();
            GUI.backgroundColor = Color.black;
            EditorGUILayout.PropertyField(NameOfHandHeld);
            EditorGUILayout.PropertyField(RaycastLength);

            serializedObject.ApplyModifiedProperties();

            //Booleans:
            //Setter Params:
            EditorGUILayout.BeginHorizontal();
            GUI.backgroundColor = ColorChecker(component.canLookRayCast);
            if (GUILayout.Button("Look Raycast")) {component.canLookRayCast = !component.canLookRayCast; }

            GUI.backgroundColor = ColorChecker(component.canInteractRayCast);
            if (GUILayout.Button("Interact Raycast")) {component.canInteractRayCast = !component.canInteractRayCast; }

            EditorGUILayout.EndHorizontal();

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("")) { }
        }
        private Color ColorChecker(bool value)
        {
            if(value) return new Color32(0, 200, 255, 255);
            return new Color32(255,120, 0, 255);
        }
    }
    #endif
}