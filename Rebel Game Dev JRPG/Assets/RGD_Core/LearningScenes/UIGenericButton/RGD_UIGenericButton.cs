using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RGD_UIGenericButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private enum ButtonPointerType
    {
        StartedHovered,
        StoppedHover,
        Clicked
    }
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

    //Unity Events (Non script use case only):
    public UnityEvent unityEventWhenHovering;
    public UnityEvent unityEventWhenStopHovering;
    public UnityEvent unityEventWhenPressed;

    //Privates:
    private Coroutine lerpCo = null;
    private Vector3 buttonStartPos;
    private Vector3 buttonStartScale;
    private Vector3 buttonStartEulerAngles;
    private Color buttonStartColor;
    private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonStartPos = rectTransform.localPosition;
        buttonStartScale = rectTransform.localScale;
        buttonStartEulerAngles = rectTransform.localEulerAngles;
        buttonStartColor = rectTransform.GetComponent<Image>().color;

    }
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
        if(eventWhenPressed is not null) eventWhenPressed.Invoke();
        if(unityEventWhenPressed is not null) unityEventWhenPressed.Invoke();
        lerpCo = StartCoroutine(RGDButtonPressed());
        //Else we clicked:
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
        rectTransform.localEulerAngles = value ? hoverRotationEulerEnd : buttonStartEulerAngles;
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
                                      buttonStartPos, lerpCurve.Evaluate(localTTime));
            Vector3 rotation = new Vector3(
                Mathf.LerpAngle(currentEulerAngle.x, buttonStartEulerAngles.x, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.y, buttonStartEulerAngles.y, lerpCurve.Evaluate(localTTime)),
                Mathf.LerpAngle(currentEulerAngle.z, buttonStartEulerAngles.z, lerpCurve.Evaluate(localTTime))
            );
            rectTransform.eulerAngles = rotation;

            rectTransform.localScale = Vector3.Lerp(currentScale,
                                   buttonStartScale, lerpCurve.Evaluate(localTTime));

            img.color = Color.Lerp(currentColor, buttonStartColor, lerpCurve.Evaluate(localTTime));
            yield return null;
        }
        rectTransform.localPosition = buttonStartPos;
        rectTransform.localEulerAngles = buttonStartEulerAngles;
        rectTransform.localScale = buttonStartScale;
        img.color = buttonStartColor;
    }
}
