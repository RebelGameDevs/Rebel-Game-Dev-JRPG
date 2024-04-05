using RebelGameDevs.ClubWideTutorials.FarmingInSpace;
using RebelGameDevs.Utils.UnrealIntegration;
using System.Collections;
using UnityEngine;

namespace Mythic
{
	public class FarmingPawn : StarterCharacterController
	{
		public UIWidget interactWidgetType;
		private InteractWidget interactWidget;
		private bool changeText = true;
		protected override void BeginPlay()
		{
			base.BeginPlay();
		}
		public override void InitializedByGamemode()
		{
			base.InitializedByGamemode();
			if(hud.AddToViewPort(interactWidgetType, out interactWidget))
			{
				InteractableComponent.OnLookedAt += (data) =>
				{
					var newData = data as LookedAtData;
					if(changeText) interactWidget.interactText.SetText(newData.interactableName);
				};
				InteractableComponent.OnLookedOff += () =>
				{
					if(changeText) interactWidget.interactText.SetText("");
				};
				InteractableComponent.OnInteractedWith += (data) =>
				{
					var newData = data as InteractedWithData;
					interactWidget.interactText.SetText($"Collected: {newData.interactable.gameObject.name}");
					StartCoroutine(WaitToLookOff());
				};
				return;
			}
		}
		private IEnumerator WaitToLookOff()
		{
			changeText = false;
			yield return new WaitForSeconds(3f);
			changeText = true;
		}
	}
}
