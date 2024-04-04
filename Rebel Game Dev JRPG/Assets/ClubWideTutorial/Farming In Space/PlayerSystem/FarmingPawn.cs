using RebelGameDevs.Utils.UnrealIntegration;
using RebelGameDevs.Utils.World;
using UnityEngine;
namespace RebelGameDevs.ClubWideTutorials.FarmingInSpace
{
    public class FarmingPawn : StarterCharacterController
    {
        [SerializeField] private UIWidget interactWidgetType;
        private InteractWidget interactWidget;
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
                    var lookedAtData = data as LookedAtData;
                    interactWidget.interactText.SetText(lookedAtData.interactableName);
                };
                InteractableComponent.OnLookedOff += () =>
                {
                    interactWidget.interactText.SetText("");
                };
                InteractableComponent.OnInteractedWith += (data) =>
                {
                    var interactData = data as InteractedWithData;
                    StartCoroutine(RGD_LerpMethods.SimpleLerpScaleToDestroy(interactData.interactable.gameObject, .5f));
                };
                return;
            }
            Debug.Log("Could not add interact widget to viewport");
        }
    }
}

