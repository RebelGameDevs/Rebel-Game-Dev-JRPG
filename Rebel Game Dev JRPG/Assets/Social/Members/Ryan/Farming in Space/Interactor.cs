using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RebelGameDevs.Utils.UnrealIntegration;
namespace Ryan
{

    public class Interactor : UnrealObject
    {
        private Camera theplayercamera;
        public float interactlength = 5f;
        public InteractWidget interactwidgettype;
        private void Awake()
        {
            UnrealInput.Map(this);
            var mapping = UnrealInput.CreateInput<Ryan_Input>(this);
            UnrealInput.SubscribeToEvent(this, mapping.FarmingInSpace.Interact,
                RebelGameDevs.Utils.UnrealIntegration.InputActionType.Performed, Interacted);
            UnrealInput.EnableInput(this);
            theplayercamera = gameObject.GetComponentInChildren<Camera>();
            StartCoroutine(waitforgamemode());
        }
        private IEnumerator waitforgamemode()
        {
            while (!GameManager.Instance.initialized) yield return null;
            Pawn parent = gameObject.GetComponent<Pawn>();
            parent.hud.AddToViewPort(interactwidgettype, out InteractWidget widget);
        }
        private void Interacted(InputAction.CallbackContext context)
        {
            if(Physics.Raycast(theplayercamera.transform.position, theplayercamera.transform.forward,
                out RaycastHit hitresult, interactlength))
            {
                Debug.Log(hitresult.collider.gameObject.name);
            }
        }
    }
}
