using UnityEngine;
namespace RebelGameDevs.Utils.UnrealIntegration
{
	public class DefaultPawn : Pawn
	{
		protected override void BeginPlay()
		{
			UnrealInput.Map(this);
			UnrealInput.CreateInput<RGD_Controls>(this);
			UnrealInput.EnableInput(this);
			UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Jump, InputActionType.Started, (context) => { Debug.Log("Testing Space Btn"); });
			UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<RGD_Controls>(this).DefaultMapping.Jump, InputActionType.Held, (context) => { Debug.Log("Holding Space Btn"); });
		}
	}
}

