namespace RebelGameDevs.Extra
{
    using RebelGameDevs.Utils.Input;
    using System.Collections;
    using UnityEngine;
    using RebelGameDevs.HelperComponents;
    public class RGD_Extra_I_CharacterButtons : RGD_InteractorTypes
    {
        private enum actionToPerform
        {
            none,
            canMoveButton,
            canSprintButton,
            canJumpButton,
            canHoldJumpButton
        }
        [SerializeField] private RGD_CharacterController characterController = null;
        [SerializeField] private actionToPerform buttonType = actionToPerform.none;
        [SerializeField] private AnimationCurve lerpCurve;
        private Coroutine isAnimating = null;
        private void Awake()
        {
            buttonsInstancer = GetComponent<RebelGameDev_Instancer>();
            if(buttonType is actionToPerform.canMoveButton) {CheckColor(characterController.CanMove());return;}
            if(buttonType is actionToPerform.canSprintButton) {CheckColor(characterController.CanSprint());return;}
            if(buttonType is actionToPerform.canJumpButton) {CheckColor(characterController.CanJump());return;}
            if(buttonType is actionToPerform.canHoldJumpButton) {CheckColor(characterController.CanHoldJump());return;}
        }
        private RebelGameDev_Instancer buttonsInstancer;
        public override string LookAtMessenger()
        {
            return LookedAtButton();
        }
        public override void InteractedWithMessenger()
        {
            if(isAnimating != null) return; 
            isAnimating = StartCoroutine(AnimateButtonIE());
            PressedButton();
        }
        private string LookedAtButton()
        {
            if(isAnimating != null) return "";
            if(buttonType is actionToPerform.canMoveButton) return $"Set player can move: {OnOrOffButton(!characterController.CanMove())}";
            if(buttonType is actionToPerform.canSprintButton) return $"Set player can sprint: {OnOrOffButton(!characterController.CanSprint())}";
            if(buttonType is actionToPerform.canJumpButton) return $"Set player can jump: {OnOrOffButton(!characterController.CanJump())}";
            if(buttonType is actionToPerform.canHoldJumpButton) return $"Set player hold to jump: {OnOrOffButton(!characterController.CanHoldJump())}";
            return "";
        }
        private void PressedButton()
        {
            if(buttonType is actionToPerform.canMoveButton)
            {
                bool value = !characterController.CanMove();
                characterController.SetCanMove(value);
                CheckColor(value);
                return;
            }
            if(buttonType is actionToPerform.canSprintButton)
            {
                bool value = !characterController.CanSprint();
                characterController.SetCanSprint(value);
                CheckColor(value);
                return;
            }
            if(buttonType is actionToPerform.canJumpButton)
            {
                bool value = !characterController.CanJump();
                characterController.SetCanJump(value);
                CheckColor(value);
                return;
            }
            if(buttonType is actionToPerform.canHoldJumpButton)
            {
                bool value = !characterController.CanHoldJump();
                characterController.SetCanHoldJump(value);
                CheckColor(value);
                return;
            }
        }
        private string OnOrOffButton(bool value)
        {
            if(value)return $"<color=green>true</color>";
            return $"<color=red>false</color>";
        }
        private void CheckColor(bool value)
        {
            if(value)
            {
                buttonsInstancer.InstanceANewColorBase(new Color(0, 1, 0));
                return;
            }
            buttonsInstancer.InstanceANewColorBase(new Color(1, 0, 0));
        }
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
