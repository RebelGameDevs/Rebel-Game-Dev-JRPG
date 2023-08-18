namespace RebelGameDevs.Extra
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RGD_Extra_I_AnimatedInteractor : RGD_InteractorTypes
    {
        [SerializeField] protected string nameOfInteractable;
        [SerializeField] protected string lookAtMessage;
        [SerializeField] protected bool canBeLookedAt = true;
        [SerializeField] protected bool canBeInteractedWith = true;

        private Animator RGDSphereAnim;
        private int currentAnimSpeedMultiplier = 0;
        private void Awake()
        {
            RGDSphereAnim = GetComponent<Animator>();
        }
        public override string LookAtMessenger()
        {
            if(canBeLookedAt) return lookAtMessage;
            return null;
        }
        public override void InteractedWithMessenger()
        {
            if(!canBeInteractedWith) return;

            canBeInteractedWith = false;
            canBeLookedAt = false;

            if(currentAnimSpeedMultiplier > 0) currentAnimSpeedMultiplier = 0;
            else currentAnimSpeedMultiplier = 1;

            RGDSphereAnim.SetFloat("speed", currentAnimSpeedMultiplier);
            Invoke(nameof(ResetInteraction), .1f);
        }
        private void ResetInteraction()
        {
            canBeInteractedWith = true;
            canBeLookedAt = true;
        }
    }
}
