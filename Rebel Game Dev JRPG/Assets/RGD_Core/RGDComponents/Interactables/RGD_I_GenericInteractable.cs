using UnityEngine;
using static RebelGameDevs.Utils.World.RGD_LerpMethods;
namespace RebelGameDevs.Interaction
{
    /*
    ============================================================
    public class RGD_I_GenericInteractable:
    
    Description:
        This is a class that can be put on any gameObject.
        It allows for the the following.. It uses the base 
        class as a bridger to have overriden methods to
        return either a message when being looked at
        or is a getting a message from the interactor script
        when interacted with.
    Derived Class: 
        InteractorTypes
    ============================================================
    */
    public class RGD_I_GenericInteractable : RGD_InteractorTypes
    {
        //Name:
        [SerializeField] protected string nameOfInteractable;
        
        //Look at Message string:
        [SerializeField] protected string lookAtMessage;
        
        //Booleans:
        [SerializeField] protected bool canBeLookedAt = true;
        [SerializeField] protected bool canBeInteractedWith = true;
        
        //Other:
        [SerializeField] private float timeToLerpOut;

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
            StartCoroutine(SimpleLerpScaleToDestroy(this.gameObject, timeToLerpOut));
        }
    }
}
