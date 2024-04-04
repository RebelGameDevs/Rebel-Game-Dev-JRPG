using UnityEngine;
namespace RebelGameDevs.Utils.UnrealIntegration
{
    public class GenericInteractable : Interactable
    {
        [SerializeField] LookedAtData lookedAtData = new LookedAtData();
        public override InteractableData LookedAt()
        {
            return lookedAtData;
        }
        public override InteractableData InteractedWith()
        {
            return new InteractedWithData(this);
        }
    }
}
