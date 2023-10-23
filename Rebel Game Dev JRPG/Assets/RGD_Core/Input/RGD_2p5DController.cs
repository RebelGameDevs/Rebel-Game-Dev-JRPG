using RebelGameDevs.Utils.Input;
using UnityEditor;
using UnityEngine;

namespace RebelGameDevs.Utils.Input
{
    using RebelGameDevs.Utils.World;
    using UnityEngine;
    using static RebelGameDevs.Utils.World.RGD_GrabComponentMethods;

    [RequireComponent(typeof(CharacterController))]public class RGD_2p5DController : MonoBehaviour
    {
        //Lambdas:
        [HideInInspector] public bool IsSprinting => canSprint && Input.GetKey(sprintKey);
        [HideInInspector] public bool ShouldJump => (!holdKeyToJump ? Input.GetKeyDown(jumpKey) : Input.GetKey(jumpKey)) && characterController.isGrounded;

        //Inspector Parameters:
        [Header("Functional Options: ")]
        [SerializeField] protected bool canMove = true;
        [SerializeField] protected bool canSprint = true;
        [SerializeField] protected bool canJump = true;
        [SerializeField] protected bool holdKeyToJump = true;

        [Header("Controls: ")]
        public KeyCode sprintKey = KeyCode.LeftShift;
        public KeyCode jumpKey = KeyCode.Space;

        [Header("Movement Parameters: ")]
        [SerializeField] public float walkSpeed = 3.0f;
        [SerializeField] public float sprintSpeed = 6.0f;
        //Getter Setters:
        public float getter_walkSpeed { get {return walkSpeed;} set {walkSpeed = value;} }
        public float getter_sprintSpeed { get {return sprintSpeed;} set {sprintSpeed = value;} }

        [Header("Jumping Parameters: ")]
        [SerializeField] protected float jumpForce = 8.0f;
        public float getter_jumpForce { get {return jumpForce;} set {jumpForce = value;} }


        [Header("Physics: ")]
        [SerializeField] protected float gravity = 30.0f;
        
        //Depreciated:
        //[SerializeField] protected LayerMask ground;
        
        public float getter_gravity { get {return gravity;} set {gravity = value;} }

        [Header("Cursor:")]
        [SerializeField] protected Texture2D cursorTexture = null;
        [SerializeField] protected CursorMode cursorMode = CursorMode.Auto;
        [SerializeField] protected Vector2 hotSpotZone = Vector2.zero;

        //Camera - Depreciated:
        //[SerializeField] protected Camera playerCamera;

        //Private Variables:
        private CharacterController characterController;
        private Vector3 moveDirection;
        private Vector2 currentInput;

        //Methods:
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            SetMouseOptions(true, CursorLockMode.Confined);
            SetCursor();
            //Depereciated: Now using cinemachine:
            //if (playerCamera == null) { Debug.LogError("<color=red> ERROR:</color> No Camera assigned for the <color=green>character controller</color>."); Destroy(this);}
        }
        private void Update()
        {
            if(!Application.isPlaying) return;
            if(canMove)
            {
                HandleMovementInput();
                if(canJump) HandleJump();
                ApplyFinalMovement();
            }
        }
        public void DisableOrEnableAllInput(bool value)
        {
            canMove = value;
        }
        
        private void HandleMovementInput()
        {
            currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal"));
            float moveDirectionY = moveDirection.y;
            moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
            moveDirection.y = moveDirectionY;
        }
        private void HandleJump()
        {
            if(ShouldJump) moveDirection.y = jumpForce;
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