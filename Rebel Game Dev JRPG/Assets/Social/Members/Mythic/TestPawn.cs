using RebelGameDevs.Utils.UnrealIntegration;
using UnityEngine;

public class TestPawn : Pawn
{
	protected override void BeginPlay()
	{
		CreateInput<RGD_Controls>();
		EnableInput();
		SubscribeToEvent(GrabInputMappingContext<RGD_Controls>().DefaultMapping.Jump, InputActionType.Started, (context) => { Debug.Log("WOW"); });
		SubscribeToEvent(GrabInputMappingContext<RGD_Controls>().DefaultMapping.Jump, InputActionType.Held, (context) => { Debug.Log("Performing"); });
	}
}
