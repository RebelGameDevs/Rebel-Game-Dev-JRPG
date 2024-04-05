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
        public static UnrealCurve LoadCurve(UnrealCurveType type)
        {
            UnrealCurve curve = null;
            switch(type)
            {
                case UnrealCurveType.Linear: 
                    RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/LinearCurve", out curve);
                    break;
                case UnrealCurveType.EaseIn: 
                    RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/EaseInCurve", out curve);
                    break;
                case UnrealCurveType.EaseOut: 
                    RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/EaseOutCurve", out curve);
                    break;
                case UnrealCurveType.EaseInEaseOut: 
                    RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/EaseInEaseOutCurve", out curve);
                    break;
                case UnrealCurveType.Exponential: 
                    RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/ExponentialCurve", out curve);
                    break;
                case UnrealCurveType.Bouncy: 
                    RGDResourceLoader.LoadRGDResource("Utils/UnrealModules/BouncyCurve", out curve);
                    break;
            }
            if(curve is null) Debug.Log("Error No Curve Found At Path");
            return curve;
        }
    }
}
