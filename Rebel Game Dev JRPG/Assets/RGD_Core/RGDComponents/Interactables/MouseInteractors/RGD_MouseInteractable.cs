using UnityEngine;

public abstract class RGD_MouseInteractable : MonoBehaviour
{
    //Bridger Methods:

    //When Mouse Starts Hovering:
    public abstract string HoveredOver();

    //When Mouse Stops Hovering:
    public abstract void StoppedHovering();

    //When Mouse Clicks On:
    public abstract void ClickedOn();
}
