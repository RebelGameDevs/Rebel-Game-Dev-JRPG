using UnityEngine;
namespace RebelGameDevs.Utils.UnrealIntegration
{
	public class DefaultPawn : Pawn
	{
		protected override void BeginPlay()
		{
			CreateInput<RGD_Controls>();
			EnableInput();
			SubscribeToEvent(GetInputMappingContext<RGD_Controls>().DefaultMapping.Jump, InputActionType.Started, (context) => { Debug.Log("Testing Space Btn"); });
			SubscribeToEvent(GetInputMappingContext<RGD_Controls>().DefaultMapping.Jump, InputActionType.Held, (context) => { Debug.Log("Holding Space Btn"); });
		}
	}
}

