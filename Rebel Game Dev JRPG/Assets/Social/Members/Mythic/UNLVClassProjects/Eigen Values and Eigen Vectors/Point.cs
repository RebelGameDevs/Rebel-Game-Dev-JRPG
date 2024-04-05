using RebelGameDevs.Utils.UnrealIntegration;
using System.Collections;
using System.Drawing;
using UnityEngine;
namespace Mythic
{
    public class Point : UnrealObject
    {
        private MeshRenderer pointsRenderer;
        private Vector3 size;
        [SerializeField] private AnimationCurve lerpCurve;
        protected override void BeginPlay()
        {
            size = transform.localScale;
            transform.localScale = Vector3.zero;
            pointsRenderer = GetComponent<MeshRenderer>();
        }
        public void Initialize(Material material)
        {
            pointsRenderer.material = new Material(material);
        }
        public IEnumerator Show(float time, bool inOrOut)
        {
            Vector3 currentSize = transform.localScale;
            float localTTime = 0;
            while(localTTime < 1)
            {
                localTTime += Time.deltaTime / time;
                transform.localScale = Vector3.Lerp(currentSize, inOrOut ? size : Vector3.zero, lerpCurve.Evaluate(localTTime));
                yield return null;
            }
            transform.localScale = inOrOut ? size : Vector3.zero;
        }
    }
}
