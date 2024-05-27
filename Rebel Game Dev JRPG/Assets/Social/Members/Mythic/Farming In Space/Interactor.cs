using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RebelGameDevs.Utils.UnrealIntegration;
namespace Mythic
{
	public class Interactor : UnrealObject
	{
		private void Awake()
		{
			UnrealInput.Map(this);
			var mapping = UnrealInput.CreateInput<Mythic_Input>(this);
			//UnrealInput.SubscribeToEvent(this, UnrealInput.GetInputMappingContext<Mythic_Input>);
		}
	}
}
