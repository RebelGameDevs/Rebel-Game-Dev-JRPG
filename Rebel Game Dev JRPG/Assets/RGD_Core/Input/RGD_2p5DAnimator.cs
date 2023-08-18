namespace RebelGameDevs.Utils.Input
{
    using UnityEngine;
    using static RebelGameDevs.Utils.World.RGD_Vector3Methods;
    public class RGD_2p5DAnimator : MonoBehaviour
    {
        //Hashing:
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Run = Animator.StringToHash("Run");
        private static readonly int JumpUp = Animator.StringToHash("JumpUp");
        private static readonly int JumpDown = Animator.StringToHash("JumpDown");

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
            if(playerController.IsSprinting) return Run;
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
