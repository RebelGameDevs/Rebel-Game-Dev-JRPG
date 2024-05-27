using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using RebelGameDevs.Utils.UnrealIntegration;
namespace Mythic
{
	public class Interactor : UnrealObject
	{
		private Camera playerCamera;
		public float interactLength = 5f;
		private void Awake()
		{
			UnrealInput.Map(this);
			var mapping = UnrealInput.CreateInput<Mythic_Input>(this);
			UnrealInput.SubscribeToEvent(this, mapping.FarmingInSpace.Interact,
				RebelGameDevs.Utils.UnrealIntegration.InputActionType.Performed, Interacted);
			UnrealInput.EnableInput(this);
			playerCamera = gameObject.GetComponentInChildren<Camera>();
		}
		private void Interacted(InputAction.CallbackContext context)
		{
			if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hitResult, interactLength))
			{
				Debug.Log(hitResult.collider.gameObject.name);
			}
		}
	}
}
