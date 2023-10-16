using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RGD_UIGenericButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private enum ButtonPointerType
    {
        StartedHovered,
        StoppedHover,
        Clicked
    }
    [Header("Animation Stuff:")]
    [SerializeField] private AnimationCurve lerpCurve;
    [SerializeField] private float timeToAnimation;

    [Header("Button Params:")]
    [SerializeField] private Vector3 rotationEulerEnd;
    [SerializeField] private Vector3 scaleEnd;
    [SerializeField] private Vector3 positionLocalEnd;

    //Privates:
    private Coroutine lerpCo = null;
    private Vector3 buttonsStartPos;
    private Vector3 buttonsStartScale;
    private Vector3 buttonsStartEulerAngles;
    private void Awake()
    {
        buttonsStartPos = transform.localPosition;
        buttonsStartScale = transform.localScale;
        buttonsStartEulerAngles = transform.localEulerAngles;
    }
    private void Clicked(ButtonPointerType type)
    {
        if(lerpCo != null) StopCoroutine(lerpCo);
        if (type is ButtonPointerType.StartedHovered)
        {
            lerpCo = StartCoroutine(LerpButton(true));
            return;
        }
        if (type is ButtonPointerType.StoppedHover)
        {
            lerpCo = StartCoroutine(LerpButton(false));
            return;
        }
        //Else we clicked:
    }
    //When Hovered:
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("Hover");
       Clicked(ButtonPointerType.StartedHovered);
    }
    //When Stopped Hover:
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("Stop Hover");
       Clicked(ButtonPointerType.StoppedHover);
    }
    //When Clicking:
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log("clicked");
       Clicked(ButtonPointerType.Clicked);
    }
    private IEnumerator LerpButton(bool value)
    {
        float localTTime = 0;
        Vector3 currentPos = transform.localPosition;
        Vector3 currentEulerAngle = transform.localEulerAngles;
        Vector3 currentScale = transform.localScale;
        while(localTTime < 1)
        {
            localTTime += Time.deltaTime / timeToAnimation;
            transform.localPosition = Vector3.Lerp(currentPos, value ? positionLocalEnd : buttonsStartPos,
                                        lerpCurve.Evaluate(localTTime));
            transform.localEulerAngles = Vector3.Lerp(currentEulerAngle, value ? rotationEulerEnd : buttonsStartEulerAngles,
                                        lerpCurve.Evaluate(localTTime));
            transform.localScale = Vector3.Lerp(currentScale, value ? scaleEnd : buttonsStartScale,
                                        lerpCurve.Evaluate(localTTime));
            yield return null;
        }
        transform.localPosition =  value ? positionLocalEnd : buttonsStartPos;
        transform.localEulerAngles = value ? rotationEulerEnd : buttonsStartEulerAngles;
        transform.localScale = value ? scaleEnd : buttonsStartScale;
        lerpCo = null;
    }
}
