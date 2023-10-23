using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.EditorTools;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace RebelGameDevs.Extra
{
    public class dory_SkyboxControlButtons : RGD_InteractorTypes
    {
        // data
        const float MAX_COLOR_VALUE = 1.0f;
        private enum SkyboxHandleType {
            none,
            Red,
            Green,
            Blue,
            Alpha
        }
        
        [Tooltip("Decrease?")]
        [SerializeField] private bool _decrease;
        [Tooltip("Object with RGD_SkyboxControl script attached.")]
        [SerializeField] private RGD_SkyboxControl control;
        [Tooltip("How much each button will increase or decrease the given RGBA value.")]
        [SerializeField] private float colorStep = 0.1f;
        [Tooltip("What the button will do to the skybox.")]
        [SerializeField] private SkyboxHandleType buttonType = SkyboxHandleType.none;
        [SerializeField] private AnimationCurve animationCurve;
        
        private Coroutine isAnimating = null;

        // override
        public override void InteractedWithMessenger()
        {
            if(isAnimating != null) return; 
            isAnimating = StartCoroutine(AnimateButtonIE());
            HandleRGBAButton();
        }

        private void HandleRGBAButton() {
            switch(buttonType) {
                case SkyboxHandleType.Red:
                    UpdateColorValue(ref control.tintColor.r);
                    break;
                case SkyboxHandleType.Green:
                    UpdateColorValue(ref control.tintColor.g);
                    break;
                case SkyboxHandleType.Blue:
                    UpdateColorValue(ref control.tintColor.b);
                    break;
                case SkyboxHandleType.Alpha:
                    UpdateColorValue(ref control.tintColor.a);
                    break;
                case SkyboxHandleType.none:
                default:
                    break;
            }

            control.UpdateSkybox();
        }

        /* UpdateColorValue: Helper function
            @param value Value to be handled, passed by reference
            @param increase Increase the value? => True to increment, false to decrement.
        */
        private void UpdateColorValue(ref float value) {
            float ans = value;
            if(!_decrease) {
                ans += colorStep;
            } else {
                ans -= colorStep;
            }
            value = Mathf.Clamp(ans, 0, MAX_COLOR_VALUE);
            return;
        }

        // from RGD_Extra_I_CharacterButtons
        private IEnumerator AnimateButtonIE()
        {
            float localTTime = 0;
            Vector3 buttonsPosition = transform.localPosition;
            Vector3 buttonsEndPosition = transform.localPosition + new Vector3(0, 0, 0.125f);
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / .1f;
                transform.localPosition = Vector3.Lerp(buttonsPosition, buttonsEndPosition, animationCurve.Evaluate(localTTime));
                yield return null;
            }
            
            // stay pressed for a bit
            yield return new WaitForSeconds(0.2f);

            localTTime = 0;
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / .1f;
                transform.localPosition = Vector3.Lerp(buttonsEndPosition, buttonsPosition, animationCurve.Evaluate(localTTime));
                yield return null;
            }
            transform.localPosition = buttonsPosition;
            isAnimating = null;
        }
    }
}
