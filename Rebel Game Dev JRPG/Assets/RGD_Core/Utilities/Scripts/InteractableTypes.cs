using UnityEngine;
namespace RebelGameDevs.Utils.UnrealIntegration
{
	public static class InteractableHelpers
	{ 
		public static bool TryFindInteractableFromPoint<I>(Vector3 startPoint, Vector3 direction, float distance, 
			LayerMask mask, out RaycastHit hitResult, out I interactableFound)where I : Interactable
		{
			if(Physics.Raycast(startPoint, direction, out hitResult, distance, mask))
				if(hitResult.collider.TryGetComponent(out interactableFound)) return true;
			interactableFound = default(I);
			return false;
		}
	}
	public abstract class InteractableData {}
	[System.Serializable] public class LookedAtData : InteractableData
	{
		public string interactableName;
	}
	public class InteractedWithData : InteractableData
	{
		[HideInInspector] public Interactable interactable;
		public InteractedWithData(Interactable interactable)
		{
			this.interactable = interactable;
		}
	}

	public class Interactable : Actor
	{
		public virtual InteractableData LookedAt() { return null; }
		public virtual InteractableData InteractedWith() { return null; }
	}
}