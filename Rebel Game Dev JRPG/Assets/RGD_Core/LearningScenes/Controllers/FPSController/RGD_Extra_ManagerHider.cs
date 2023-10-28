#if UNITY_EDITOR
using UnityEditor;
using static RebelGameDevs.Utils.RebelGameDevsEditorHelpers;
#endif
namespace RebelGameDevs.HelperComponents
{
    using UnityEngine;
    /*
    ============================================================
    RGD_Extra_ManagerHider:

    Description:
        Has nothing in it and the sole purpose is the Editor
        Script below. The script has some functions to search
        and find all scripts with the script component
        "RGD_Extra_HideInWorld" and either hide in hierarchy
        or show them in hierarchy.
    ============================================================
    */
    public class RGD_Extra_ManagerHider : MonoBehaviour{}
    #if UNITY_EDITOR
    [CustomEditor(typeof(RGD_Extra_ManagerHider))]
    class RGD_Extra_ManagerHider_Editor : Editor
    {
        private bool value = false;
        public override void OnInspectorGUI()
        {
        //Check For Script:
            var component = (RGD_Extra_ManagerHider)target;
            if (component == null) return;
            Undo.RecordObject(component, "Change Component");

            //Custom Inspector:
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("")) { }
            DrawImage("Assets/RGD_Core/Utilities/Images/Banner_Utility.png", 0.85f, 0.3f);
        
            string whatToSay = "";
            if(value) whatToSay = "Show all HideFlags";
            else whatToSay = "HideAllHideFlags";

            if(GUILayout.Button($"{whatToSay}"))FlagStuff();

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("")) { }
        }
        private void FlagStuff()
        {
            var objects = GameObject.FindGameObjectsWithTag("RebelGameDevsTag");
            var component = (RGD_Extra_ManagerHider)target;
            foreach(GameObject obj in objects)
            {
                if(obj == component) continue;

                if(obj.TryGetComponent(out RGD_Extra_HideInWorld hiderScript))
                {
                    if(value)
                    {
                        Debug.Log($"<color=red>{obj.name}</color> un hidden");
                        hiderScript.SetHideFlags(HideFlags.None);   
                        continue;
                    }
                    Debug.Log($"<color=red>{obj.name}</color> hidden");
                    hiderScript.SetHideFlags(HideFlags.HideInHierarchy);
                }
            }
            value = !value;
        }
    }
    #endif
}
