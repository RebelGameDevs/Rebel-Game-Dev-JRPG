using RebelGameDevs.Utils.Input;
using UnityEngine;
using UnityEditor;
using RebelGameDevs.Utils.UnrealIntegration;
namespace RebelGameDevs.Utils.Input
{
    [RequireComponent(typeof(UnityEngine.CharacterController))] public class RGD_2p5DController : Pawn
    {
        [HideInInspector] public bool isSprinting;

        //Inspector Parameters:
        [Header("Functional Options: ")]
        [SerializeField] protected bool canMove = true;
        [SerializeField] protected bool canSprint = true;
        [SerializeField] protected bool canJump = true;
        [SerializeField] protected bool holdKeyToJump = true;

        [Header("Movement Parameters: ")]
        public float walkSpeed = 3.0f;
        public float sprintSpeed = 6.0f;

        [Header("Jumping Parameters: ")]
        public float jumpForce = 8.0f;

        [Header("Physics: ")]
        public float gravity = 30.0f;
        
        [Header("Cursor:")]
        [SerializeField] protected Texture2D cursorTexture = null;
        [SerializeField] protected CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] protected Vector2 hotSpotZone = Vector2.zero;

        //Private Variables:
        private UnityEngine.CharacterController characterController;
        private Vector3 moveDirection;
        private Vector2 currentInput;
        private Vector2 rawInput;

        //Methods:
        protected override void BeginPlay()
        {
            InitializeInput();
            SetMouseOptions(true, CursorLockMode.Confined);
            SetCursor();
            characterController = GetComponent<UnityEngine.CharacterController>();
        }
        private void Update()
        {
            if(!Application.isPlaying) return;
            if(canMove)
            {
                HandleMovementInput();
                ApplyFinalMovement();
            }
        }
        private void InitializeInput()
        {
            CreateInput<RGD_Controls>();
            SubscribeToEvent(GetInputMappingContext<RGD_Controls>().DefaultMapping.Jump, InputActionType.Performed, (context) => { HandleJump(); });
            SubscribeToEvent(GetInputMappingContext<RGD_Controls>().DefaultMapping.Jump, InputActionType.Held, (context) => { if(holdKeyToJump) HandleJump(); });
            SubscribeToEvent(GetInputMappingContext<RGD_Controls>().DefaultMapping.Sprint, InputActionType.Performed, (context) => { if(canSprint) isSprinting = true; });
            SubscribeToEvent(GetInputMappingContext<RGD_Controls>().DefaultMapping.Sprint, InputActionType.Canceled, (context) => { isSprinting = false; });
            EnableInput();
        }
        public void DisableOrEnableAllInput(bool value) { canMove = value; }
        
        private void HandleMovementInput()
        {
            rawInput = GetInputMappingContext<RGD_Controls>().DefaultMapping.Move.ReadValue<Vector2>();
            currentInput = new Vector2((isSprinting ? sprintSpeed : walkSpeed) * rawInput.y, (isSprinting ? sprintSpeed : walkSpeed) * rawInput.x);
            float moveDirectionY = moveDirection.y;
            moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
            moveDirection.y = moveDirectionY;
        }
        private void HandleJump()
        {
            if(!characterController.isGrounded) return;
            moveDirection.y = jumpForce;
        }
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
        public Vector3 GetVelocity()
        {
            return characterController.velocity;
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

        public void SetCursor()
        {
            Cursor.SetCursor(cursorTexture, hotSpotZone, cursorMode);
        }
    }
}
#if UNITY_EDITOR
public class RGD_2p5DCharacterControllerSetup : EditorWindow
{
    private RGD_2p5DController controller;
    private bool IsSelectingPlayerModifications = false;
    private static readonly string note = $"2.5D CharacterController Notes:\n" +
			                             $"------------------------\n\n" +
			                             $"This is a base controller. The intended use is for beginner programmers and/or to build off of. " +
								         $"We strongly recommend to only use this to debug things in your games. There is also a FPS controller " +
								         $"called \"FPS Controller\" if this suites your needs better. \n\n" +
								         $"To get started on customization please click the player in the scene. NOTE: it already " +
								         $"comes with a base type set camera. Please ensure you delete any other cameras in your scene or set them "+
                                         $"to inactive or as a overlay cam.";
                
    private void OnEnable()
    {
        IsSelectingPlayerModifications = false;
    }
    private void OnGUI()
    {
        GUILayout.Label("2.5D Character Controller: ");
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

        if(!IsSelectingPlayerModifications && GUILayout.Button("Read Notes About Character Controller")) IsSelectingPlayerModifications = true;
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
    public void SetPlayer(RGD_2p5DController controller)
    {
        this.controller = controller;
    }
}
#endif