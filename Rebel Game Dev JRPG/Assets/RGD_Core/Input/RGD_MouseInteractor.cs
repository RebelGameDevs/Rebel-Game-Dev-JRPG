namespace RebelGameDevs.Interaction
{
    using static RebelGameDevs.Utils.World.RGD_GrabComponentMethods;
    using UnityEngine;
    using TMPro;

    public class RGD_MouseInteractor : MonoBehaviour
    {

        [SerializeField] private LayerMask layersToIgnore;

        //Canvas Element(s):
        [SerializeField] private TextMeshProUGUI interactText = null;

        //Camera:
        private Camera playersCamera;
        //CurrentHoveredObject:
        private RGD_MouseInteractable currentHoveredObject;

        private void Awake()
        {
            //Grab reference to Camera:
            playersCamera = GetComponent<Camera>();
        }
        private void Update()
        {
            DetectMouse();
        }
        private void DetectMouse()
        {
            //If Successfully grabbed mouse position and grabbed gameObject with the base abstract mouse interact component:
            if(RGD_TryGrabMouseWorldPosition(playersCamera, out RGD_MouseInteractable mouseInteractable))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if(currentHoveredObject is not null) currentHoveredObject.StoppedHovering();
                    currentHoveredObject = null;
                    mouseInteractable.ClickedOn();
                    return;
                }

                if (currentHoveredObject != null  && currentHoveredObject == mouseInteractable) return;
                if(currentHoveredObject != null) currentHoveredObject.StoppedHovering();

                currentHoveredObject = mouseInteractable;


                //Else set the text:
                interactText.SetText(mouseInteractable.HoveredOver());
                return;
            }

            //If not all conditions are met, then we simply set the text to be blank:
            if(currentHoveredObject != null)
            {
                currentHoveredObject.StoppedHovering();
                currentHoveredObject = null;
            }
            interactText.SetText("");
        }
    }
}
