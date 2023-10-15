using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

namespace RebelGameDevs.Extra{
    public class RGD_I_SkyboxControl : RGD_InteractorTypes
    {
        // data
        const float MAX_COLOR_VALUE = 255.0f;
        const float MAX_ALPHA_VALUE = 1.0f;
        private enum MyActions {
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
        [SerializeField] private Material baseSkybox;
        [Tooltip("How much each button will increase or decrease the given color value.")]
        [SerializeField] private float stepAmount = 15.0f;
        [Tooltip("How much each button will increase or decrease the alpha value")]
        [SerializeField] private float alphaStepAmount = 0.1f;
        [Tooltip("What the button will do for the skybox.")]
        [SerializeField] private MyActions buttonType = MyActions.none;
        [SerializeField] private AnimationCurve lerpCurve;
        private Material newMaterial;
        private Coroutine isAnimating = null;

        // awake
        private void Awake() {
            newMaterial = baseSkybox;
        }

        private void Update() {
            RenderSettings.skybox = newMaterial;
        }

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
            HandleButton();
        }

        // methods
        private void HandleButton() {
            float val;
            Color newColor = newMaterial.color;
            switch(buttonType) {
                case MyActions.increaseRed:
                    val = newMaterial.color.r;
                    if(newMaterial.color.r > MAX_COLOR_VALUE+stepAmount) {
                        val = MAX_COLOR_VALUE;
                    } else if (newMaterial.color.r < stepAmount) {
                        val = 0;
                    }
                    newColor.r = val+stepAmount;
                    break;
                case MyActions.decreaseRed:
                    val = newMaterial.color.r;
                    if(newMaterial.color.r > MAX_COLOR_VALUE+stepAmount) {
                        val = MAX_COLOR_VALUE;
                    } else if (newMaterial.color.r < stepAmount) {
                        val = 0;
                    }
                    newColor.r = val+stepAmount;
                    break;
                case MyActions.increaseGreen:
                    val = newMaterial.color.g;
                    if(newMaterial.color.g > MAX_COLOR_VALUE+stepAmount) {
                        val = MAX_COLOR_VALUE;
                    } else if (newMaterial.color.g < stepAmount) {
                        val = 0;
                    }
                    newColor.g = val+stepAmount;
                    break;
                case MyActions.decreaseGreen:
                    break;
                case MyActions.increaseBlue:
                    val = newMaterial.color.b;
                    if(newMaterial.color.b > MAX_COLOR_VALUE+stepAmount) {
                        val = MAX_COLOR_VALUE;
                    } else if (newMaterial.color.b < stepAmount) {
                        val = 0;
                    }
                    newColor.b = val+stepAmount;
                    break;
                case MyActions.decreaseBlue:
                    break;
                case MyActions.increaseAlpha:
                    break;
                case MyActions.decreaseAlpha:
                    break;
                case MyActions.none:
                default:
                    break;
            }
            newMaterial.color = newColor;
        }

        // from RGD_Extra_I_CharacterButtons
        private IEnumerator AnimateButtonIE()
        {
            float localTTime = 0;
            Vector3 buttonsPosition = transform.localPosition;
            Vector3 buttonsEndPosition = transform.localPosition + new Vector3(0, 0, -0.125f);
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / .1f;
                transform.localPosition = Vector3.Lerp(buttonsPosition, buttonsEndPosition, lerpCurve.Evaluate(localTTime));
                yield return null;
            }
            
            // stay pressed for a bit
            yield return new WaitForSeconds(0.2f);

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
