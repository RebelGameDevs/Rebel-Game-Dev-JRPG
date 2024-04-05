using System;
using UnityEngine;
namespace RebelGameDevs.Utils.UnrealIntegration
{
    [System.Serializable] public class MovementComponent : ActorComponent
    {
        [Header("Movement Parameters: ")]
        public float walkSpeed = 3.0f;
        public float sprintSpeed = 6.0f;

        [Header("Functional Options: ")]
        public bool canMove = true;
        public bool canSprint = true;
        public bool canJump = true;
        public bool holdKeyToJump = true;
        [HideInInspector] public bool isSprinting = false;

        [Header("Jumping Parameters: ")]
        public float jumpForce = 8.0f;

        [Header("Physics: ")]
        public float gravity = 30.0f;
        private Vector3 moveDirection;

        private UnityEngine.CharacterController characterController;
        private Camera playerCamera;

        [Header("Look Parameters: ")]
        [SerializeField, Range(1, 10)] protected float lookSpeedX = 3.0f;
        [SerializeField, Range(1, 10)] protected float lookSpeedY = 2.0f;
        [SerializeField, Range(1, 180)] protected float upperLookLimit = 80.0f;
        [SerializeField, Range(1, 180)] protected float lowerLookLimit = 80.0f;

        //Additional varaible:
        private Vector2 currentInput;
        private Vector2 rawInput;

        private float rotationX = 0;

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
        public override void Initialize(UnrealObject uObject)
        {
            parent = uObject;
            InitializeInput();
            characterController = uObject.GetComponent<UnityEngine.CharacterController>();
            playerCamera = uObject.GetComponentInChildren<Camera>();
                if (playerCamera == null) { Debug.LogError("<color=red> ERROR:</color> No Camera as a child on the <color=green>character controller</color>."); UnityEngine.Object.Destroy(uObject);}
        }
        private void InitializeInput()
        {
            UnrealInput.Map(parent);
            UnrealInput.CreateInput<RGD_Controls>(parent);
            UnrealInput.SubscribeToEvent(parent, UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.Sprint, InputActionType.Performed, (context) => { if(canSprint) isSprinting = true; });
            UnrealInput.SubscribeToEvent(parent, UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.Sprint, InputActionType.Canceled, (context) => { isSprinting = false; });
            UnrealInput.SubscribeToEvent(parent, UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.Jump, InputActionType.Performed, (context) => { HandleJump(); });
            UnrealInput.SubscribeToEvent(parent, UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.Jump, InputActionType.Held, (context) => { if(holdKeyToJump) HandleJump(); });
            UnrealInput.EnableInput(parent);
        }
        public void OnUpdate()
        {
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
        private void HandleMouseLook()
        {
            Vector2 delta = UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.MouseDelta.ReadValue<Vector2>() * 0.1f;
            rotationX -= delta.y * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            parent.transform.rotation *= Quaternion.Euler(0, delta.x * lookSpeedX, 0);
        }
        private void HandleJump()
        {
            if(!canJump || !characterController.isGrounded) return; 
            moveDirection.y = jumpForce;
        }
        private void HandleMovementInput()
        {
            rawInput = UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.Move.ReadValue<Vector2>();
            currentInput = new Vector2((isSprinting ? sprintSpeed : walkSpeed) * rawInput.y, (isSprinting ? sprintSpeed : walkSpeed) * rawInput.x);
            float moveDirectionY = moveDirection.y;
            moveDirection = (parent.transform.TransformDirection(Vector3.forward) * currentInput.x) + (parent.transform.TransformDirection(Vector3.right) * currentInput.y);
            moveDirection.y = moveDirectionY;
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
    }
    [System.Serializable] public class FirstPersonInteractComponent : ActorComponent
    {
        public LayerMask interactableLayer;
        public float interactDistance = 3f;
        private Camera playerCamera;

        public Action<InteractableData> OnLookedAt;
        public Action<InteractableData> OnInteractedWith;
        public Action OnLookedOff;

        public override void Initialize(UnrealObject uObject)
        {
            parent = uObject;
            playerCamera = uObject.GetComponentInChildren<Camera>();
            InitializeInput();
        }
        private void InitializeInput()
        {
            UnrealInput.Map(parent);
            UnrealInput.CreateInput<RGD_Controls>(parent);
            UnrealInput.SubscribeToEvent(parent, UnrealInput.GetInputMappingContext<RGD_Controls>(parent).DefaultMapping.LMB, InputActionType.Performed, (context) =>
            {
                if(InteractableHelpers.TryFindInteractableFromPoint<Interactable>(playerCamera.transform.position, 
                    playerCamera.transform.forward, interactDistance, interactableLayer, out RaycastHit hitResult, out Interactable interactable))
                        OnInteractedWith?.Invoke(interactable.InteractedWith());

            });
            UnrealInput.EnableInput(parent);
        }
        public void OnUpdate()
        {
            if(InteractableHelpers.TryFindInteractableFromPoint<Interactable>(playerCamera.transform.position, 
                    playerCamera.transform.forward, interactDistance, interactableLayer, out RaycastHit hitResult, out Interactable interactable))
            {
                OnLookedAt?.Invoke(interactable.LookedAt());
                return;
            }
            OnLookedOff?.Invoke();
        }
    }

    [RequireComponent(typeof(CharacterController))] public class StarterCharacterController : Pawn
	{
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var cam = GetComponentInChildren<Camera>();
            Gizmos.DrawRay(cam.transform.position, cam.transform.forward * InteractableComponent.interactDistance);
        }
        public MovementComponent MovementComponent = new MovementComponent();
        public FirstPersonInteractComponent InteractableComponent = new FirstPersonInteractComponent();

        //Methods:
        protected override void BeginPlay()
        {
            //Initialize actor components:
            MovementComponent.Initialize(this);
            InteractableComponent.Initialize(this);

            //Initialize Cursor:
            UnrealIntegrationAddons.SetMouseOptions(false, CursorLockMode.Locked);
        }
        protected override void EventTick()
        {
            MovementComponent.OnUpdate();
            InteractableComponent.OnUpdate();
        }
	}

}
