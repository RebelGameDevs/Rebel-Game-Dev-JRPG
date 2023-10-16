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
            increaseRed,
            decreaseRed,
            increaseGreen,
            decreaseGreen,
            increaseBlue,
            decreaseBlue,
            increaseAlpha,
            decreaseAlpha
        }
        
        [Tooltip("Target skybox to control.")]
        [SerializeField] private RGD_SkyboxControl control;
        [Tooltip("How much each button will increase or decrease the given RGBA value.")]
        [SerializeField] private float colorStep = 0.1f;
        [Tooltip("What the button will do to the skybox.")]
        [SerializeField] private SkyboxHandleType buttonType = SkyboxHandleType.none;
        [SerializeField] private AnimationCurve animationCurve;
        
        private Coroutine isAnimating = null;

        

        // overrides
        public override string LookAtMessenger()
        {
            Debug.Log("LookAt registered");
            // return empty string; don't want any gui text for these buttons
            return "";
        }

        public override void InteractedWithMessenger()
        {
            if(isAnimating != null) return; 
            isAnimating = StartCoroutine(AnimateButtonIE());
            HandleRGBAButton();
        }

        private void HandleRGBAButton() {
            switch(buttonType) {
                case SkyboxHandleType.increaseRed:
                    UpdateColorValue(ref control.tintColor.r, true);
                    break;
                case SkyboxHandleType.decreaseRed:
                    UpdateColorValue(ref control.tintColor.r, false);
                    break;
                case SkyboxHandleType.increaseGreen:
                    UpdateColorValue(ref control.tintColor.g, true);
                    break;
                case SkyboxHandleType.decreaseGreen:
                    UpdateColorValue(ref control.tintColor.g, false);
                    break;
                case SkyboxHandleType.increaseBlue:
                    UpdateColorValue(ref control.tintColor.b, true);
                    break;
                case SkyboxHandleType.decreaseBlue:
                    UpdateColorValue(ref control.tintColor.b, false);
                    break;
                case SkyboxHandleType.increaseAlpha:
                    UpdateColorValue(ref control.tintColor.a, true);
                    break;
                case SkyboxHandleType.decreaseAlpha:
                    UpdateColorValue(ref control.tintColor.a, false);
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
        private void UpdateColorValue(ref float value, bool increase) {
            float ans = value;
            if(increase) {
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
