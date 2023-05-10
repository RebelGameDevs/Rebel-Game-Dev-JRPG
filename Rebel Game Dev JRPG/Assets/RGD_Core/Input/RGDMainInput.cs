using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RebelGameDev.Core.Input
{
    public class RGDMainInput : MonoBehaviour
    {
        public abstract class ActionEvents { }
        [System.Serializable]public class MainControls_ActionEvent : ActionEvents
        {
            public delegate void InputActionDelegate(InputAction.CallbackContext direciton);
            public InputActionDelegate Movement;
            public InputActionDelegate ActionSpace;
            public InputActionDelegate ActionQ;
            public InputActionDelegate ActionE;
            public InputActionDelegate ActionR;
            public InputActionDelegate ActionF;
            public InputActionDelegate Action1;
            public InputActionDelegate Action2;
            public InputActionDelegate Action3;
        }
        public MainControls_ActionEvent MainControlsEvents = new MainControls_ActionEvent();
        public RGD_MainControls mainControls;
        private void Awake()
        {
            mainControls = new RGD_MainControls();
            EnableOrDisableInput(true);
            InitializeMapping();
            Transform temp;
            temp = transform;
        }
        public void InitializeMapping()
        {
            mainControls.MainInputActionMap.Movement.performed += OnMovement;
            mainControls.MainInputActionMap.Movement.canceled += OnMovement;
            mainControls.MainInputActionMap.ActionSpace.performed += OnActionSpace;
            mainControls.MainInputActionMap.ActionQ.performed  += ActionQ;
            mainControls.MainInputActionMap.ActionE.performed  += ActionE;
            mainControls.MainInputActionMap.ActionR.performed  += ActionR;
            mainControls.MainInputActionMap.ActionF.performed  += ActionF;
            mainControls.MainInputActionMap.Action1.performed  += Action1;
            mainControls.MainInputActionMap.Action2.performed  += Action2;
            mainControls.MainInputActionMap.Action3.performed  += Action3;
        }
        public void EnableOrDisableInput(bool value)
        {
            if(value)
            {
                mainControls.MainInputActionMap.Enable();
                return;
            }
            mainControls.MainInputActionMap.Disable();
        }
        private void OnMovement(InputAction.CallbackContext value)    {if(MainControlsEvents.Movement != null)     MainControlsEvents.Movement(value);}
        private void OnActionSpace(InputAction.CallbackContext value) {if(MainControlsEvents.ActionSpace != null)  MainControlsEvents.ActionSpace(value);}
        private void ActionQ(InputAction.CallbackContext value)       {if(MainControlsEvents.ActionQ != null)      MainControlsEvents.ActionQ(value);}
        private void ActionE(InputAction.CallbackContext value)       {if(MainControlsEvents.ActionE != null)      MainControlsEvents.ActionE(value);}
        private void ActionR(InputAction.CallbackContext value)       {if(MainControlsEvents.ActionR != null)      MainControlsEvents.ActionR(value);}
        private void ActionF(InputAction.CallbackContext value)       {if(MainControlsEvents.ActionF != null)      MainControlsEvents.ActionF(value);}
        private void Action1(InputAction.CallbackContext value)       {if(MainControlsEvents.Action1 != null)      MainControlsEvents.Action1(value);}
        private void Action2(InputAction.CallbackContext value)       {if(MainControlsEvents.Action2 != null)      MainControlsEvents.Action2(value);}
        private void Action3(InputAction.CallbackContext value)       {if(MainControlsEvents.Action3 != null)      MainControlsEvents.Action3(value);}
    }
     #if UNITY_EDITOR
    [CustomEditor(typeof(RGDMainInput))]
    class RGDMainInputEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //Check For Script:
            var component = (RGDMainInput)target;
            if (component == null) return;
            Undo.RecordObject(component, "Change Component");

            //Custom Inspector:
            changeColorBackground(Color.red);
            if (GUILayout.Button("")) { }

            changeColorBackground(new Color32(0, 0, 0, 0));

            Texture banner = (Texture)AssetDatabase.LoadAssetAtPath("Assets/RGD_Core/Utilities/Images/Banner_Input.png", typeof(Texture));
            GUILayout.Box(banner, GUILayout.Width(495), GUILayout.Height(165));
            
            changeColorBackground(Color.grey);
            EditorGUILayout.LabelField("NOTE: the actions are taken from the input settings[RGD_MainControls]");

            changeColorBoth(Color.white, Color.red);
            if (GUILayout.Button("")) { }
        }
        private void changeColorBoth(Color32 color, Color32 backgroundColor)
        {
            GUI.color = color;
            GUI.backgroundColor = backgroundColor;
        }
        private void changeColorSingle(Color32 color)
        {
            GUI.color = color;
        }
        private void changeColorBackground(Color32 color)
        {
            GUI.backgroundColor = color;
        }

    }
#endif
}
