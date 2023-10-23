using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RGD_InteractorTypes : MonoBehaviour
{
    //Bridger Methods:

        //When Looked At:
        // Make virtual as people may not always want to have a GUI update
        // for an interactable
        public virtual string LookAtMessenger() {
            return "";
        }
    
        //When Interacted With:
        public abstract void InteractedWithMessenger();
    
}
