using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RebelGameDevs.Utils.UnrealIntegration
{
    public class UnrealUIButton : UnrealUIObject
    {
        [System.Serializable] public struct UIButtonState
        {
            public bool updateColor;
            public bool updateScale;
            public Color color;
            public Vector2 scale;
            public float transitionTime;
            public UIButtonState(Color color, Vector2 scale)
            {
                updateColor = false;
                updateScale = false;
                transitionTime = .5f;
                this.color = color;
                this.scale = scale;
            }
        }
        private UIButtonState currentState;

        public UIButtonState idleState = new UIButtonState(color: new Color(1, 1, 1, 1), scale: Vector2.one); //released, enabled, hovered off: (like an idle state)
        public UIButtonState startHoverState = new UIButtonState(color: new Color(1, 1, 1, 1), scale: Vector2.one); // mouse over:
        public UIButtonState pressedState = new UIButtonState(color: new Color(1, 1, 1, 1), scale: Vector2.one); //pressed:
        public UIButtonState disabledState = new UIButtonState(color: new Color(1, 1, 1, 1), scale: Vector2.one); //disabled:
        [HideInInspector] public UnrealCurve lerpCurve;

        [Header("Lerp Curve: loaded only on BeginPlay()")]
        [SerializeField] private UnrealCurveType lerpCurveType;

        private Image btnImage;
        private Coroutine lerpCo = null;

        protected override void BeginPlay()
        {
            lerpCurve = UnrealCurve.LoadCurve(lerpCurveType);
            btnImage = graphic as Image;

            currentState = idleState;
            ChangePhase(idleState);
        }
        protected override void StartHovering()
        {
            if(lerpCo is not null) StopCoroutine(lerpCo);
            lerpCo = StartCoroutine(LerpToPhase(startHoverState));
        }
        protected override void StopHovering()
        {
            if(lerpCo is not null) StopCoroutine(lerpCo);
            lerpCo = StartCoroutine(LerpToPhase(idleState));
        }
        protected override void Pressed()
        {
            if(lerpCo is not null) StopCoroutine(lerpCo);
            lerpCo = StartCoroutine(LerpToPhase(pressedState));
        }
        protected override void Released()
        {
            if(lerpCo is not null) StopCoroutine(lerpCo);
            lerpCo = StartCoroutine(LerpToPhase(idleState));
        }
        protected override void Disabled()
        {
            if(lerpCo is not null) StopCoroutine(lerpCo);
            lerpCo = StartCoroutine(LerpToPhase(disabledState));
        }
        protected override void Enabled()
        {
            if(lerpCo is not null) StopCoroutine(lerpCo);
            lerpCo = StartCoroutine(LerpToPhase(idleState));
        }
        private IEnumerator LerpToPhase(UIButtonState newPhase)
        {
            UIButtonState oldState = currentState;
            float localTTime = 0;
            UIButtonState lerpedState = new UIButtonState();
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / currentState.transitionTime;
                lerpedState.color = Color.Lerp(oldState.color, newPhase.color, lerpCurve.CurveEvaluatedOverTime(localTTime));
                lerpedState.scale = Vector3.Lerp(oldState.scale, newPhase.scale, lerpCurve.CurveEvaluatedOverTime(localTTime));
                ChangePhase(lerpedState);
                yield return null;
            }
            lerpedState.color = newPhase.color;
            lerpedState.scale = newPhase.scale;
            ChangePhase(lerpedState);
            lerpCo = null;
        }
        private void ChangePhase(UIButtonState newPhase)
        {
            UIButtonState tempPhase = currentState;
            //tempPhase.color = newPhase.updateColor ? newPhase.color : tempPhase.color;
            //tempPhase.scale = newPhase.updateScale ? newPhase.scale : tempPhase.scale;
            tempPhase.color = newPhase.color;
            tempPhase.scale = newPhase.scale;

            currentState = tempPhase;

            btnImage.color = currentState.color;
            transform.localScale = currentState.scale;
        }
    }
}
