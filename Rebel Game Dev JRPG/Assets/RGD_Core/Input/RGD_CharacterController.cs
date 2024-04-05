using RebelGameDevs.Utils.UnrealIntegration;
using RebelGameDevs.Utils.Input;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using static RebelGameDevs.Utils.RebelGameDevsEditorHelpers;
#endif
namespace RebelGameDevs.Utils.Input
{
    using UnityEngine;
    [RequireComponent(typeof(CharacterController))] [ExecuteInEditMode] public class RGD_CharacterController : Pawn
    {
        //Lambdas:
        [HideInInspector] public bool isSprinting = false;

        //Inspector Parameters:
        [Header("Functional Options: ")]
        [SerializeField] protected bool canMove = true;
        [SerializeField] protected bool canSprint = true;
        [SerializeField] protected bool canJump = true;
        [SerializeField] protected bool holdKeyToJump = true;

        [Header("Movement Parameters: ")]
        public float walkSpeed = 3.0f;
        public float sprintSpeed = 6.0f;

        [Header("Look Parameters: ")]
        [SerializeField, Range(1, 10)] protected float lookSpeedX = 3.0f;
        [SerializeField, Range(1, 10)] protected float lookSpeedY = 2.0f;
        [SerializeField, Range(1, 180)] protected float upperLookLimit = 80.0f;
        [SerializeField, Range(1, 180)] protected float lowerLookLimit = 80.0f;

        //Getter Setters:
        public float getter_lookSpeedX { get {return lookSpeedX;} set {lookSpeedX = value;} }
        public float getter_lookSpeedY { get {return lookSpeedY;} set {lookSpeedY = value;} }
        public float getter_upperLookLimit { get {return upperLookLimit;} 
            set 
            {
                upperLookLimit = value;
                upperLookLimit = Mathf.Clamp(upperLookLimit, 1, 180);
            }
        }
        public float getter_lowerLookLimit { get {return lowerLookLimit;} 
            set 
            {
                lowerLookLimit = value;
                lowerLookLimit = Mathf.Clamp(lowerLookLimit, 1, 180);
            }
        }

        [Header("Jumping Parameters: ")]
        public float jumpForce = 8.0f;

        [Header("Physics: ")]
        public float gravity = 30.0f;

        //Private Variables:
        private Camera playerCamera;
        private CharacterController characterController;

        private Vector3 moveDirection;
        private Vector2 currentInput;
        private Vector2 rawInput;

        private float rotationX = 0;

        //Methods:
        protected override void BeginPlay()
        {
            //Initialize input:
            InitializeInput();


            //Initialize Cursor:
            SetMouseOptions(false, CursorLockMode.Locked);

            //Grab references:
            characterController = GetComponent<CharacterController>();
            playerCamera = GetComponentInChildren<Camera>();
            if (playerCamera == null) { Debug.LogError("<color=red> ERROR:</color> No Camera as a child on the <color=green>character controller</color>."); Destroy(this);}
        }
        private void Update()
        {
            //Make sure that this is not running in editor as this is using "[ExecuteInEditMode]".
            if(!Application.isPlaying) return;

            if(canMove)
            {
                //Handles Player Movement:
                HandleMovementInput();

                //Handles Mouse Orientation:
                HandleMouseLook();

                //Finally Apply Movement:
                ApplyFinalMovement();
            }
        }
        private void InitializeInput()
        {
            UnrealInput.Map(this);
            UnrealInput.CreateInput<RGD_Controls>(this);
            UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Sprint, InputActionType.Performed, (context) => { if(canSprint) isSprinting = true; });
            UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Sprint, InputActionType.Canceled, (context) => { isSprinting = false; });
            UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Jump, InputActionType.Performed, (context) => { HandleJump(); });
            UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Jump, InputActionType.Held, (context) => { if(holdKeyToJump) HandleJump(); });
            UnrealInput.EnableInput(this);
        }
        /*
        =========================================================================================================
        HandleMovementInput:

        Params:
            None

        Description:
            Takes account of sprint speed and walk speed adjusted to the vertical and horizontal (WASD) axis.
            This will also setup the moveDirection location (used in the "ApplyFinalMovements() method").
        =========================================================================================================
        */
        private void HandleMovementInput()
        {
            rawInput = UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Move.ReadValue<Vector2>();
            currentInput = new Vector2((isSprinting ? sprintSpeed : walkSpeed) * rawInput.y, (isSprinting ? sprintSpeed : walkSpeed) * rawInput.x);
            float moveDirectionY = moveDirection.y;
            moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
            moveDirection.y = moveDirectionY;
        }
        /*
        =========================================================================================================
        HandleMovementInput:

        Params:
            None

        Description:
            Will setup the global params "rotationX" and setup the players localRotation relative to the 
            transform. It will also clamp the values given the upper and lower look limit.
        =========================================================================================================
        */
        private void HandleMouseLook()
        {
            Vector2 delta = UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.MouseDelta.ReadValue<Vector2>() * 0.1f;
            rotationX -= delta.y * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, delta.x * lookSpeedX, 0);
        }
        /*
        =========================================================================================================
        HandleJump:

        Params:
            None

        Description:
            Does what it says. It handles the jump (setting up the movedirection on the y).
        =========================================================================================================
        */
        private void HandleJump()
        {
            if(!canJump || !characterController.isGrounded) return; 
            moveDirection.y = jumpForce;
        }
        /*
        =========================================================================================================
        ApplyFinalMovement:

        Params:
            None

        Description:
            Now after all other calculations are done we can finally move the character, that is only the case
            if we are grounded (meaning the player is on the ground).
        =========================================================================================================
        */
        private void ApplyFinalMovement()
        {
            if(!characterController.isGrounded) moveDirection.y -= gravity * Time.deltaTime;

            characterController.Move(moveDirection * Time.deltaTime);
        }
        //Getter Functions:
        public bool CanMove()
        {
            return canMove;
        }
        public bool CanSprint()
        {
            return canSprint;
        }
        public bool CanJump()
        {
            return canJump;
        }
        public bool CanHoldJump()
        { 
            return holdKeyToJump;    
        }

        //Setter Functions:
        public void SetCanMove(bool value)
        {
            canMove = value;
        }    
        public void SetCanSprint(bool value)
        {
            canSprint = value;
        }
        public void SetCanJump(bool value)
        {
            canJump = value;
        }
        public void SetCanHoldJump(bool value)
        {
            holdKeyToJump = value;
        }
        //Mouse Options:
        public void SetMouseOptions(bool isVisible)
        {
            Cursor.visible = isVisible;
        }
        public void SetMouseOptions(CursorLockMode state)
        {
            Cursor.lockState = state;
        }
        public void SetMouseOptions(bool isVisible, CursorLockMode state)
        {
            Cursor.lockState = state;
            Cursor.visible = isVisible;
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(RGD_CharacterController))]
class RGD_CharacterControllerEditor : Editor
{
    //My Toolbar:
    string[] titles = new string[] {"Functional Options","Controls", "Movement Params", "Physics"};
    private int currentTitleSelected = -1;

    //Setter Params:
    SerializedProperty canMove;
    SerializedProperty canSprint;
    SerializedProperty canJump;
    SerializedProperty holdToJumpKey;

    private void OnEnable () 
    {
        canMove = serializedObject.FindProperty("canMove");
        canSprint = serializedObject.FindProperty("canSprint");
        canJump = serializedObject.FindProperty("canJump");
        holdToJumpKey = serializedObject.FindProperty("holdKeyToJump");
    }
    public override void OnInspectorGUI()
    {
    //Check For Script:
        var component = (RGD_CharacterController)target;
        if (component == null) return;
        Undo.RecordObject(component, "Change Component");
        DrawImage("Assets/RGD_Core/Utilities/Images/Banner_Controller.png", 0.85f, 0.3f);
        

        GUI.backgroundColor = Color.white;
        GUI.color = Color.white;
        currentTitleSelected = GUILayout.SelectionGrid(currentTitleSelected, titles, 2);
        DrawComponentsInInspector(component);

        serializedObject.ApplyModifiedProperties();
    }
    private void DrawComponentsInInspector(RGD_CharacterController script)
    {
        GUI.backgroundColor = Color.red;
        GUI.color = Color.white;
        if (currentTitleSelected == -1){ HandleNothingSelected(script); return;}
        if (currentTitleSelected == 0) { HandleMovementBooleans(); return;}
        if (currentTitleSelected == 1) { HandleKeyCodes(script); return; }
        if (currentTitleSelected == 2) { HandleMovementParams(script); return;}
        if (currentTitleSelected == 3) { HandlePhysicsParams(script); return;}

    }
    private void HandleNothingSelected(RGD_CharacterController script)
    {
        GUI.color = Color.white;
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("NO TOOLBAR SELECTED")) 
        {
            if(showingErrorMessageBool != null) script.StopCoroutine(showingErrorMessageBool);
            showingErrorMessageBool = script.StartCoroutine(ShowingMessageNothingSelected());
            //Debug.Log($"Please select an option in the <color=#0000FF>tool bar</color> for the" +
				//$" <color=green>Rebel Game Devs Controller</color>.");
        }
        if(showingErrorMessageBool != null)
        {
            EditorStyles.textField.wordWrap = true; // This sets the wordwrap value of the property
            EditorGUILayout.TextArea("Please select an option in the tool bar for the Rebel Game Devs Controller.");
        }
        GUI.color = Color.white;
        GUI.backgroundColor = Color.black;
        EditorStyles.textField.wordWrap = true; // This sets the wordwrap value of the property
        EditorGUILayout.TextArea("Know issue with toolbar buttons not working: please minimize the other components attached " +
			                     "to this gameObject (usually the cause is the RebelGameDevInstancer script component), to fix " +
								 "this issue if it occurs. - sorry for the inconvenience."); 
    }
    private Coroutine showingErrorMessageBool = null;
    private IEnumerator ShowingMessageNothingSelected()
    {
        yield return new WaitForSeconds(3);
        showingErrorMessageBool = null;
    }
    private void HandlePhysicsParams(RGD_CharacterController script)
    {
         GUI.color = Color.white;

        //walk speed:
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Players Gravity: {script.gravity}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.gravity += 1f;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.gravity -= 1f;}
        EditorGUILayout.EndHorizontal();
    }
    private void HandleMovementParams(RGD_CharacterController script)
    {
        GUI.color = Color.white;

        //walk speed:
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Walk Speed: {script.walkSpeed}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.walkSpeed += 0.25f;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.walkSpeed -= 0.25f;}
        EditorGUILayout.EndHorizontal();

        //run speed:
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Sprint Speed: {script.sprintSpeed}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.sprintSpeed += 0.25f;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.sprintSpeed -= 0.25f;}
        EditorGUILayout.EndHorizontal();

        //Camera Look Speedx
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Camera X Speed: {script.getter_lookSpeedX}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.getter_lookSpeedX += 0.25f;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.getter_lookSpeedX -= 0.25f;}
        EditorGUILayout.EndHorizontal();

        //Camera Look Speedy
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Camera Y Speed: {script.getter_lookSpeedY}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.getter_lookSpeedY += 0.25f;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.getter_lookSpeedY -= 0.25f;}
        EditorGUILayout.EndHorizontal();

        //Camera Look ClampX
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Camera Upper Look Limit: {script.getter_upperLookLimit}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.getter_upperLookLimit ++;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.getter_upperLookLimit --;}
        EditorGUILayout.EndHorizontal();

        //Camera Look ClampX
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Camera Lower Look Limit: {script.getter_lowerLookLimit}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.getter_lowerLookLimit ++;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.getter_lowerLookLimit --;}
        EditorGUILayout.EndHorizontal();

        //Camera Look ClampX
        GUI.backgroundColor = Color.black;
        GUILayout.SelectionGrid(1, new string[1] {$"Jump Force: {script.jumpForce}"}, 1);
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("+")) {script.jumpForce += 0.5f;}
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("-")) {script.jumpForce -= 0.5f;}
        EditorGUILayout.EndHorizontal();
    }
    private void HandleMovementBooleans()
    {
        //Setter Params:
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = ColorChecker(canMove.boolValue);
        if (GUILayout.Button("Can Move")) {canMove.boolValue = !canMove.boolValue; }
        
        GUI.backgroundColor = ColorChecker(canSprint.boolValue);
        if (GUILayout.Button("Can Sprint")) {canSprint.boolValue = !canSprint.boolValue; }

        GUI.backgroundColor = ColorChecker(canJump.boolValue);
        if (GUILayout.Button("Can Jump")) {canJump.boolValue = !canJump.boolValue; }

        GUI.backgroundColor = ColorChecker(holdToJumpKey.boolValue);
        if (GUILayout.Button("Hold To Jump")) {holdToJumpKey.boolValue = !holdToJumpKey.boolValue; }
        EditorGUILayout.EndHorizontal();
    }
    private void HandleKeyCodes(RGD_CharacterController script)
    {
        GUI.backgroundColor = new Color32(255, 150, 0, 255);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button($"Section Coming Soon!")) { }
        EditorGUILayout.EndHorizontal();
    }

    private Color ColorChecker(bool value)
    {
        if(value) return new Color32(0, 200, 255, 255);
        return Color.red;
    }
}
public class RGD_PlayerControllerSetup : EditorWindow
{
    private RGD_CharacterController controller;
    private bool IsSelectingPlayerModifications = false;
    private static readonly string note = $"Player Controller Notes:\n" +
			                             $"------------------------\n\n" +
			                             $"This is a base controller. The intended use is for beginner programmers and/or to build off of. " +
								         $"We strongly recommend to only use this to debug things in your games. There is also a 2.5D controller " +
								         $"called \"RebelGameDevs_2p5D_Controller\" if this suites your needs better. \n\n" +
								         $"To get started on customization please click the player controller in the scene. NOTE: it already " +
								         $"comes with a base type set camera already as a child of the player controller so make sure any " +
								         $"base (aka main) cameras are: turned off, deleted, or base type switched to an overlay.\n\n" +
								         $"--> Double click the controller prefab field above to select the controller in the scene.";
    private void OnEnable()
    {
        IsSelectingPlayerModifications = false;
    }
    private void OnGUI()
    {
        GUILayout.Label("Player Controller: ");
        if (controller == null)
        {
            GUI.backgroundColor = Color.black;
            GUILayout.Button("Can't Find Player");
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Close")) this.Close();    
            return;
        }
        EditorGUILayout.BeginHorizontal();
        var temp = (GameObject)EditorGUILayout.ObjectField(controller.gameObject, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();

        GUI.backgroundColor = Color.black;

        if(!IsSelectingPlayerModifications && GUILayout.Button("Read Notes About Player Controller")) IsSelectingPlayerModifications = true;
        if (IsSelectingPlayerModifications) Notes();

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Close")) this.Close();    
    }
    private void Notes()
    {
        GUI.color = Color.white;
        GUI.backgroundColor = Color.black;
        EditorGUILayout.TextArea(note);
    }
    public void SetPlayer(RGD_CharacterController controller)
    {
        this.controller = controller;
    }
}
#endif

