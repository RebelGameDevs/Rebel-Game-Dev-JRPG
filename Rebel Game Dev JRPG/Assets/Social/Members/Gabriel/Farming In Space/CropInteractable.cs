using RebelGameDevs.Utils.UnrealIntegration;
using UnityEngine;
using System;

namespace Gabriel
{

    public class CropInteractable : Interactable
    {
        public LookedAtData data = new LookedAtData();
        public override InteractableData LookedAt()
        {
            return data;
        }
        public override InteractableData InteractedWith()
        {
            return new InteractedWithData(this);   
        }
    }
}
