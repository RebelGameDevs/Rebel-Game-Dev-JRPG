using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using static UnityEngine.Input;
namespace RebelGameDevs.Utils.Input
{
    public class RGD_FlappyBirdController : MonoBehaviour
    {
        [SerializeField] private float flapSpeed = .2f;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float velocity = 1.5f;
        [SerializeField] private float rotationSpeed = 10;
        private Rigidbody flappysRb;
        private SpriteRenderer spriteRenderer;
        private Coroutine flapperCo = null;
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            flappysRb = GetComponent<Rigidbody>();
            flapperCo = StartCoroutine(Flap());
        }
        private void Update()
        {
            if(Mouse.current.leftButton.wasPressedThisFrame || GetKeyDown(KeyCode.Space))
            {
                flappysRb.velocity = Vector2.up * velocity;
            }
        }
        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, flappysRb.velocity.y * rotationSpeed);
        }
        private IEnumerator Flap()
        {
            while(true)
            {
                foreach(Sprite sprite in sprites)
                {
                    spriteRenderer.sprite = sprite;
                    yield return new WaitForSeconds(flapSpeed);
                }
            }
        }
    }
}
