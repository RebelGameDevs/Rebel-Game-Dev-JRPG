namespace RebelGameDevs.Utils.Input
{
    using RebelGameDevs.Utils.UnrealIntegration;
    using UnityEngine;
    public class RGD_2p5DAnimator : MonoBehaviour
    {
        //Hashing:
        protected static int Idle = Animator.StringToHash("Idle");
        protected static int Walk = Animator.StringToHash("Walk");
        protected static int Run = Animator.StringToHash("Run");
        protected static int JumpUp = Animator.StringToHash("JumpUp");
        protected static int JumpDown = Animator.StringToHash("JumpDown");

        [SerializeField] private RGD_2p5DController playerController;
        private SpriteRenderer playersSpriteRenderer;
        private Animator charactersAnimator = null;
        private int currentState = 0;
        private void Awake()
        {
            playersSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            charactersAnimator = GetComponent<Animator>();
        }
        private void Update()
        {
            HandleRotation();
            var state = HandleStates();
            if(state == currentState) return;
            charactersAnimator.CrossFade(state, 0, 0);
            currentState = state;
        }
        private int HandleStates()
        {
            if(playerController.GetVelocity().y >= .1f) return JumpUp;
            if(playerController.GetVelocity().y <= -.1f) return JumpDown;
            if(playerController.isSprinting) return Run;
            if((Mathf.Abs(playerController.GetVelocity().x) >= .1f) || (Mathf.Abs(playerController.GetVelocity().z) >= .1f)) return Walk;
            return Idle;
        }
        private void HandleRotation()
        {
            var currentVelocity = playerController.GetVelocity();
            if (currentVelocity.x >= .1f && playersSpriteRenderer.flipX)
            {
                playersSpriteRenderer.flipX = false;
                return;
            }
            if(currentVelocity.x <= -0.1f && !playersSpriteRenderer.flipX)
            {
                playersSpriteRenderer.flipX = true;
                return;
            }
        }
    }
}
