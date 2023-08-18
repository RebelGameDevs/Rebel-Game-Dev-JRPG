namespace RebelGameDevs.Interaction
{
    using static RebelGameDevs.Utils.World.RGD_GrabComponentMethods;
    using UnityEngine;
    using TMPro;
    public class RGD_MainInteractor : MonoBehaviour
    {
        //Current Object in Hand:
        [SerializeField] private RGD_S_BaseHandHeld currentHandHeld = null;

        //Camera (not using, but could be useful):
        private Camera playersCamera = null;

        //Input:
        [SerializeField] private KeyCode interactKeyCode = KeyCode.Mouse0;
        
        //Ignore Layers:
        [SerializeField] private LayerMask whatToIgnore = new LayerMask();
        
        //Canvas:
        [SerializeField] private TextMeshProUGUI interactText = null;
        private void Awake()
        {
            playersCamera = gameObject.GetComponent<Camera>();
        }
        private void Update()
        {
            if(currentHandHeld == null) return;

            //Shoot a raycast from transform given the length of the current hand held object.
            if(RGD_TryGrabRayCasterFromPoint(transform, currentHandHeld.raycastLength, out RaycastHit hitResult, ~whatToIgnore, out RGD_InteractorTypes interactable))
            {
                //Check for input for Interact Messenger:
                if(Input.GetKeyDown(interactKeyCode))
                {
                    interactable.InteractedWithMessenger();
                    return;
                }

                //Else Look At Messenger:
                interactText.SetText($"{interactable.LookAtMessenger()}");
                return;
            }

            //If the raycast fails set the text to null (emulating the text being blank):
            interactText.SetText("");
        }
        private void OnDrawGizmos()
        {
            if(currentHandHeld == null) return;
            Gizmos.color = Color.red; 
         
            //Show the ray in the unity editor (for debug purposes :) <you're welcome>).
            Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * currentHandHeld.raycastLength);
        }
    }
}
