namespace RebelGameDevs.Extra
{
    using RebelGameDevs.Utils.Input;
    using UnityEngine;
    public class RGD_ExtraCharacterControllerButtonManager : MonoBehaviour
    {
        [SerializeField] private RGD_CharacterController characterController;
        public void ChangeWalkSpeed(float speedChange)
        {
            characterController.getter_walkSpeed += speedChange;
        }
        public void ChangeSprintSpeed(float speedChange)
        {
            characterController.getter_walkSpeed += speedChange;
        }
        public void ChangeJumpForce(float amount)
        {
            characterController.getter_jumpForce += amount;
        }
        public void ChangeGravity(float amount)
        {
            characterController.getter_gravity += amount;
        }
    }
}
