namespace RebelGameDevs.Extra
{
    using RebelGameDevs.Utils.Input;
    using UnityEngine;
    public class RGD_ExtraCharacterControllerButtonManager : MonoBehaviour
    {
        [SerializeField] private RGD_CharacterController characterController;
        public void ChangeWalkSpeed(float speedChange)
        {
            characterController.walkSpeed += speedChange;
        }
        public void ChangeSprintSpeed(float speedChange)
        {
            characterController.sprintSpeed += speedChange;
        }
        public void ChangeJumpForce(float amount)
        {
            characterController.jumpForce += amount;
        }
        public void ChangeGravity(float amount)
        {
            characterController.gravity += amount;
        }
    }
}
