using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RGD_InteractorTypes : MonoBehaviour
{
    //Bridger Methods:

        //When Looked At:
        public abstract string LookAtMessenger();
    
        //When Interacted With:
        public abstract void InteractedWithMessenger();
    
}
