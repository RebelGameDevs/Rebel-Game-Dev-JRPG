using UnityEngine;
using UnityEngine.Events;

namespace RebelGameDevs.Interaction
{
    /*
    ======================================================================
    public class RGD_Trigger:

    Description:
        is a trigger component that can be added to any object that has a
        trigger collider. On entry,exit, or stay of those gameobjects if 
        they have some type of physics system like a rigidbody or a 
        character collider they will call the onTriggerEnter method. Then 
        if the collider has the tag then it will call the event:
    ======================================================================
    */
    public class RGD_Trigger : MonoBehaviour
    {
        //Tag or Tags to look for:
        [SerializeField] private string[] tagToLookFor = new string[0];
        [SerializeField] private bool oneTimeUseTrigger = false;

        //Unity Event To Call
        [SerializeField] private UnityEvent EnterEvent;
        [SerializeField] private UnityEvent StayEvent;
        [SerializeField] private UnityEvent ExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (EventInovker(EnterEvent, other)) { if (oneTimeUseTrigger) Destroy(this); }
        }
        private void OnTriggerExit(Collider other)
        {
            EventInovker(ExitEvent, other);
        }
        private void OnTriggerStay(Collider other)
        {
            EventInovker(StayEvent, other);
        }
        private bool EventInovker(UnityEvent eventToInvoke, Collider other)
        {
            bool atleast1TagFound = false;
            foreach (string tag in tagToLookFor)
            {
                //Check tag to invoke event:
                if (tag == other.gameObject.tag)
                {
                    eventToInvoke.Invoke();
                    atleast1TagFound = true;
                }
            }
            return atleast1TagFound;
        }
    }
}
