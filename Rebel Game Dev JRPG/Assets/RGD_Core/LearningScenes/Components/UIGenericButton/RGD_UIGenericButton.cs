/*
===========================================================================
Creator: 
    Brandhon Bird (Mythic)
Date: 
    10/22/23
Purpose:
    A rebel game dev component to help create UI buttons that have more
    freedom vs. Unity's generic button. This button allows for callee 
    methods on click with delegates or Unity Events
Contact:
    Should you have any questions or concers, feel free to contact me
    via phone - +1 (702) - 857 - 1869 | email: mythicgaming234@gmail.com
===========================================================================
*/
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RGD_UIGenericButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //Enum for ButtonPointerState:
    private enum ButtonPointerType
    {
        StartedHovered,
        StoppedHover,
        Clicked
    }
    //Enum for ButtonClickType
    public enum ButtonClickType
    {
        OnDown,
        OnRelease
    }

    [Header("Animation Stuff:")]
    [SerializeField] private AnimationCurve lerpCurve;

    [Header("Hover Event Params:")]
    [SerializeField] private float hoverTimeToAnimation;
    [SerializeField] private Vector3 hoverRotationEulerEnd;
    [SerializeField] private Vector3 hoverScaleEnd;
    [SerializeField] private Vector3 hoverPositionEnd;
    [SerializeField] private Color hoverColorEnd;

    [Header("Clicked Event Params:")]
    public ButtonClickType buttonClickAction;
    [SerializeField] private float clickTimeToAnimation;
    [SerializeField] private Vector3 clickedRotationEulerEnd;
    [SerializeField] private Vector3 clickedScaleEnd;
    [SerializeField] private Vector3 clickedPositionEnd;
    [SerializeField] private Color clickedColorEnd;

    //Delegates (script use case only):
    public delegate void ButtonDelegateEvent();
    public ButtonDelegateEvent eventWhenHovered;
    public ButtonDelegateEvent eventWhenStopHovering;
    public ButtonDelegateEvent eventWhenPressed;

    //Unity Events (Non script use case only, but can be):
    public UnityEvent unityEventWhenHovering;
    public UnityEvent unityEventWhenStopHovering;
    public UnityEvent unityEventWhenPressed;

    //Privates:
    //Coroutine to keep track of IEnumerator:
    private Coroutine lerpCo = null;

    //Start Local Position
    private Vector3 buttonStartPos;

    //Start Scale:
    private Vector3 buttonStartScale;

    //Start Rotation:
    private Vector3 buttonStartEulerAngles;

    //Start Color:
    private Color buttonStartColor;

    //Buttons Rect:
    private RectTransform rectTransform;
    private void Awake()
    {
        //Set Start Params:
        rectTransform = GetComponent<RectTransform>();
        buttonStartPos = rectTransform.localPosition;
        buttonStartScale = rectTransform.localScale;
        buttonStartEulerAngles = rectTransform.localEulerAngles;
        buttonStartColor = rectTransform.GetComponent<Image>().color;

        //Set states to change so the button can be placed anywhere:
        hoverPositionEnd += buttonStartPos;
        clickedPositionEnd += buttonStartPos;

        hoverRotationEulerEnd += buttonStartEulerAngles;
        clickedRotationEulerEnd += buttonStartEulerAngles;

        hoverScaleEnd.x *= buttonStartScale.x;
        hoverScaleEnd.y *= buttonStartScale.y;
        hoverScaleEnd.z *= buttonStartScale.z;
        clickedScaleEnd.x *= buttonStartScale.x;
        clickedScaleEnd.y *= buttonStartScale.y;
        clickedScaleEnd.z *= buttonStartScale.z;
    }
    /*
    ===========================================================================
    void Clicked(ButtonPointerType type):
        Description:
            stops last Coroutine to stop last state from overriding. Then checks
            state and performs the unity and/or delegate event when performing.
    ===========================================================================
    */
    private void Clicked(ButtonPointerType type)
    {
        if (lerpCo != null) StopCoroutine(lerpCo);
        if (type is ButtonPointerType.StartedHovered)
        {
            if(eventWhenHovered is not null) eventWhenHovered.Invoke();
            if(unityEventWhenHovering is not null) unityEventWhenHovering.Invoke();
            lerpCo = StartCoroutine(LerpButton(true));
            return;
        }
        if (type is ButtonPointerType.StoppedHover)
        {
            if(eventWhenStopHovering is not null) eventWhenStopHovering.Invoke();
            if(unityEventWhenStopHovering is not null) unityEventWhenStopHovering.Invoke();
            lerpCo = StartCoroutine(LerpButton(false));
            return;
        }
        //Else we clicked:
        if(eventWhenPressed is not null) eventWhenPressed.Invoke();
        if(unityEventWhenPressed is not null) unityEventWhenPressed.Invoke();
        lerpCo = StartCoroutine(RGDButtonPressed());
    }
    //When Hovered:
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Clicked(ButtonPointerType.StartedHovered);
    }
    //When Stopped Hover:
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Clicked(ButtonPointerType.StoppedHover);
    }
    //When Pressed Down:
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (buttonClickAction is ButtonClickType.OnDown) Clicked(ButtonPointerType.Clicked);
    }
    //When Pressed Up:
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (buttonClickAction is ButtonClickType.OnRelease) Clicked(ButtonPointerType.Clicked);
    }
    private IEnumerator LerpButton(bool value)
    {
        //Keep track of time:
        float localTTime = 0;

        //Grab current state of  button:
        Vector3 currentPos = rectTransform.localPosition;
        Vector3 currentEulerAngle = rectTransform.localEulerAngles;
        Vector3 currentScale = rectTransform.localScale;

        var img = rectTransform.GetComponent<Image>();
        Color currentColor = img.color;

        while (localTTime < 1)
        {
            //Calculate how far we are from [0 - 1].
            localTTime += Time.deltaTime / hoverTimeToAnimation;

            //Animate position:
            rectTransform.localPosition = Vector3.Lerp(currentPos, value ? hoverPositionEnd : buttonStartPos,
                                        lerpCurve.Evaluate(localTTime));
            //Animate rotation:
            //NOTE: using Lerp Angle to take the shortest route other wise going negative will lerp wrong.
            Vector3 rotation = new Vector3(
                Mathf.LerpAngle(currentEulerAngle.x, value ? hoverRotationEulerEnd.x : buttonStartEulerAngles.x, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.y, value ? hoverRotationEulerEnd.y : buttonStartEulerAngles.y, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.z, value ? hoverRotationEulerEnd.z : buttonStartEulerAngles.z, lerpCurve.Evaluate(localTTime))
            );
            transform.eulerAngles = rotation;

            //Animate scale:
            rectTransform.localScale = Vector3.Lerp(currentScale, value ? hoverScaleEnd : buttonStartScale,
                                        lerpCurve.Evaluate(localTTime));

            //Animate color:
            img.color = Color.Lerp(currentColor, value ? hoverColorEnd : buttonStartColor, lerpCurve.Evaluate(localTTime));

            //Wait 1 frame:
            yield return null;
        }

        //Most of the time lerping never fully get's to destination so we will just set it here:
        rectTransform.localPosition = value ? hoverPositionEnd : buttonStartPos;
        rectTransform.localEulerAngles = value ? hoverRotationEulerEnd: buttonStartEulerAngles;
        rectTransform.localScale = value ? hoverScaleEnd : buttonStartScale;
        img.color = value ? hoverColorEnd : buttonStartColor;

        //Set coroutine to null for stopping co's.
        lerpCo = null;
    }
    private IEnumerator RGDButtonPressed()
    {
        //For tracking time:
        float localTTime = 0;

        //Get current state of button:
        Vector3 currentPos = rectTransform.localPosition;
        Vector3 currentEulerAngle = rectTransform.localEulerAngles;
        Vector3 currentScale = rectTransform.localScale;

        var img = rectTransform.GetComponent<Image>();
        Color currentColor = img.color;

        while (localTTime < 1)
        {
            //Calculate how far we are from [0 - 1].
            localTTime += Time.deltaTime / (clickTimeToAnimation * .5f);

            //Animate position:
            rectTransform.localPosition = Vector3.Lerp(currentPos,
                                      clickedPositionEnd, lerpCurve.Evaluate(localTTime));

            //Animate rotation:
            //NOTE: using Lerp Angle to take the shortest route other wise going negative will lerp wrong.
            Vector3 rotation = new Vector3(
                Mathf.LerpAngle(currentEulerAngle.x, clickedRotationEulerEnd.x, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.y, clickedRotationEulerEnd.y, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.z, clickedRotationEulerEnd.z, lerpCurve.Evaluate(localTTime))
            );
            rectTransform.eulerAngles = rotation;

            //Animate scale:
            rectTransform.localScale = Vector3.Lerp(currentScale,
                                   clickedScaleEnd, lerpCurve.Evaluate(localTTime));

            //Animate color:
            img.color = Color.Lerp(currentColor, clickedColorEnd, lerpCurve.Evaluate(localTTime));

            //Wait one frame:
            yield return null;
        }

        //Most of the time lerping never fully get's to destination so we will just set it here:
        currentPos = rectTransform.localPosition;
        currentEulerAngle = rectTransform.localEulerAngles;
        currentScale = rectTransform.localScale;
        currentColor = img.color;

        //Now animate the entire thing backwards:
        localTTime = 0;
        while (localTTime < 1)
        {
            localTTime += Time.deltaTime / (clickTimeToAnimation * .5f);
            rectTransform.localPosition = Vector3.Lerp(currentPos,
                                      hoverPositionEnd, lerpCurve.Evaluate(localTTime));
            Vector3 rotation = new Vector3(
                Mathf.LerpAngle(currentEulerAngle.x, hoverRotationEulerEnd.x, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.y, hoverRotationEulerEnd.y, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.z, hoverRotationEulerEnd.z, lerpCurve.Evaluate(localTTime))
            );
            rectTransform.eulerAngles = rotation;

            rectTransform.localScale = Vector3.Lerp(currentScale,
                                   hoverScaleEnd, lerpCurve.Evaluate(localTTime));

            img.color = Color.Lerp(currentColor, hoverColorEnd, lerpCurve.Evaluate(localTTime));
            yield return null;
        }
        rectTransform.localPosition = hoverPositionEnd;
        rectTransform.localEulerAngles = hoverRotationEulerEnd;
        rectTransform.localScale = hoverScaleEnd;
        img.color = hoverColorEnd;
    }
}
