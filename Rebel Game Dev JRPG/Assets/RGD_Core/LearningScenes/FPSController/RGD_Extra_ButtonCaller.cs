namespace RebelGameDevs.Extra
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;

    public class RGD_Extra_ButtonCaller : RGD_InteractorTypes
    {
        [SerializeField] private string messageToRespondWith = "Press me";
        [SerializeField] private Vector3 directionToLerp = new Vector3(0, 0, -0.125f);
        [SerializeField] private AnimationCurve lerpCurve = null;
        private Coroutine isAnimating = null;
        public UnityEvent actionToPerform = null;
        public override string LookAtMessenger()
        {
            if(isAnimating != null) return "";
            return messageToRespondWith;
        }
        public override void InteractedWithMessenger()
        {
            if(isAnimating != null) return;
            isAnimating = StartCoroutine(AnimateButtonIE());
            actionToPerform.Invoke();
        }
        private IEnumerator AnimateButtonIE()
        {
            float localTTime = 0;
            Vector3 buttonsPosition = transform.localPosition;
            Vector3 buttonsEndPosition = transform.localPosition + directionToLerp;
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / .1f;
                transform.localPosition = Vector3.Lerp(buttonsPosition, buttonsEndPosition, lerpCurve.Evaluate(localTTime));
                yield return null;
            }
            localTTime = 0;
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / .1f;
                transform.localPosition = Vector3.Lerp(buttonsEndPosition, buttonsPosition, lerpCurve.Evaluate(localTTime));
                yield return null;
            }
            transform.localPosition = buttonsPosition;
            isAnimating = null;
        }
    }

}
