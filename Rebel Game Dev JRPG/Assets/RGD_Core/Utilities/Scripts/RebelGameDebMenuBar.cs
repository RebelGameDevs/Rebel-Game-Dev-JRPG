#if UNITY_EDITOR
using RebelGameDevs.Utils.Input;
using UnityEditor;
using UnityEngine;
#endif
namespace RebelGameDevs.Utils
{
    public class RebelGameDebMenuBar
    {
        //Read Onlys:
        private static readonly string errorMessage = "REBEL GAME DEVS ERROR POP UP";
        private static readonly string popUpWizard =  "REBEL GAME DEVS Wizard";
        private static readonly Vector2 minwindowSize = new Vector2(510, 450);
        private static readonly Vector2 maxWindowSize = new Vector2(510, 750);

        //Menu Options:
        [MenuItem("Rebel Game Devs/Core/Create A First Person Controller")]
        public static void CreatePlayer()
        {
            int userInput;
            Transform obj;
            Transform controller;
            userInput = EditorUtility.DisplayDialogComplex($"{popUpWizard}:", "Welcome to the Rebel Game Devs FPS Controller Wizard." +
		        "\n\n Where would you like to spawn the player in the Unity Editor?", "Editors Cam Pos. ", "Close", "World Origin");
            RGD_PlayerControllerSetup window;
            if(userInput == 1) return;
            if(userInput == 0)
            {//Editors Camera Position:
                obj = Resources.Load("RGDStarters/FirstPersonController", typeof(Transform)) as Transform;
                controller = Object.Instantiate(obj, SceneView.GetAllSceneCameras()[0].transform.position, Quaternion.identity).transform;
                window = EditorWindow.GetWindow<RGD_PlayerControllerSetup>("RGD Wizard");
                window.Show();
                window.minSize = minwindowSize;
                window.maxSize = maxWindowSize;
                window.SetPlayer(controller.GetComponent<RGD_CharacterController>());
                return;
            }
            //Else world origin: 
            obj = Resources.Load("RGDStarters/FirstPersonController", typeof(Transform)) as Transform;
            controller = Object.Instantiate(obj, Vector3.zero, Quaternion.identity).transform;
            window = EditorWindow.GetWindow<RGD_PlayerControllerSetup>("PRGD Wizard");
            window.Show();
            window.minSize = minwindowSize;
            window.maxSize = maxWindowSize;
            window.SetPlayer(controller.GetComponent<RGD_CharacterController>());
        }
        [MenuItem("Rebel Game Devs/Core/Create A 2.5D Character Controller")]
        public static void CreatePlayer2p5D()
        {
            int userInput;
            Transform obj;
            Transform controller;
            userInput = EditorUtility.DisplayDialogComplex($"{popUpWizard}:", "Welcome to the Rebel Game Devs 2.5D Controller Wizard." +
		        "\n\n Where would you like to spawn the player in the Unity Editor?", "Editors Cam Pos. ", "Close", "World Origin");
            RGD_2p5DCharacterControllerSetup window;
            if(userInput == 1) return;
            if(userInput == 0)
            {//Editors Camera Position:
                obj = Resources.Load("RGDStarters/2.5DHolder", typeof(Transform)) as Transform;
                controller = Object.Instantiate(obj, SceneView.GetAllSceneCameras()[0].transform.position, Quaternion.identity).transform;
                window = EditorWindow.GetWindow<RGD_2p5DCharacterControllerSetup>("RGD Wizard");
                window.Show();
                window.minSize = minwindowSize;
                window.maxSize = maxWindowSize;
                window.SetPlayer(controller.GetComponentInChildren<RGD_2p5DController>());
                return;
            }
            //Else world origin: 
            obj = Resources.Load("RGDStarters/2.5DHolder", typeof(Transform)) as Transform;
            controller = Object.Instantiate(obj, Vector3.zero, Quaternion.identity).transform;
            window = EditorWindow.GetWindow<RGD_2p5DCharacterControllerSetup>("RGD Wizard");
            window.Show();
            window.minSize = minwindowSize;
            window.maxSize = maxWindowSize;
            window.SetPlayer(controller.GetComponentInChildren<RGD_2p5DController>());
        }

        [MenuItem("Rebel Game Devs/Core/Add A Inventory System (Select A Camera)")]
        public static void AddInventorySystem()
        {
            var currentObject = Selection.activeTransform;

            if(currentObject == null)
            {
                EditorUtility.DisplayDialog($"{errorMessage}", "You do not have a object selected.", "Ok");
                return;
            }
            if(!currentObject.gameObject.TryGetComponent(out Camera cam))
            {
                bool output = EditorUtility.DisplayDialog($"{errorMessage}:", "This needs to be on a camera.\n\nWould you like to put this on the main camera?", "Yes", "No");
                if(output)
                {
                    currentObject = Camera.main.transform;
                    var temp = currentObject.GetComponentInChildren<RGD_InventorySystem>();
                    if (temp != null)
                    {
                        output = EditorUtility.DisplayDialog($"{errorMessage}:", "You Silly Goose. You already have a inventory sytem.\nWould you like to remove it and create a new one?", "Yes", "No");
                        if (output)
                        {
                            localInstantiater();
                            Object.DestroyImmediate(temp.gameObject);
                        }
                        return;
                    }
                }
                return;
            }
            var oldComponent = currentObject.GetComponentInChildren<RGD_InventorySystem>();

            if(oldComponent != null)
            {
                bool output = EditorUtility.DisplayDialog($"{errorMessage}:", "You Silly Goose. You already have a inventory sytem.\nWould you like to remove it and create a new one?", "Yes", "No");
                if (output)
                {
                    localInstantiater();
                    Object.DestroyImmediate(oldComponent.gameObject);
                }
                return;
            }

            localInstantiater();

            void localInstantiater()
            {
                var obj = (Transform)AssetDatabase.LoadAssetAtPath("Assets/RGD_Core/Utilities/UtilityPrefabs/InventorySystem.prefab", typeof(Transform));
                Object.Instantiate(obj, currentObject);
            }
        }
    }
}
