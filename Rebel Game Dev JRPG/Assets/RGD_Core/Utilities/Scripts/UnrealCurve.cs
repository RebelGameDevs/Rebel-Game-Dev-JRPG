using UnityEngine;
namespace RebelGameDevs.Utils.UnrealIntegration
{
    [System.Serializable] public enum UnrealCurveType
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInEaseOut,
        Exponential,
        Bouncy
    }
    [CreateAssetMenu(menuName = "RGDCore/UnrealEngine/Modules/Curve", fileName = "New Curve")]public class UnrealCurve : UnrealModule
    {
        [SerializeField] private AnimationCurve curve;
        public AnimationCurve Curve() { return curve; }
        public float CurveEvaluatedOverTime(float time) { return curve.Evaluate(time); }
    }
}
