using RebelGameDevs.Utils.UnrealIntegration;
using UnityEngine;

namespace Mythic
{
    public class RomeoJulietPawn : Pawn
    {
        [SerializeField] private UIWidget graphHandlerType;
        private GraphHandlerWidget widgetReference;
        public override void InitializedByGamemode()
        {
            hud.AddToViewPort(graphHandlerType, out widgetReference);
            widgetReference.Setup(this);
        }
    }
}
