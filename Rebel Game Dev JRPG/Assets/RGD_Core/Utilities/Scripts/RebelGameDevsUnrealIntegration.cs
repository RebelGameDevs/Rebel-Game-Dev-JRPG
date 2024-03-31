/*
Author: Brandon Bird (Mythic)
Date: 3/30/24
Purpose: To allow for Unreal Engine Integration support in Unity for UNLV Rebel Game Devs Open Source project.
*/
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RebelGameDevs.Utils.UnrealIntegration
{
	//Defined Types:
	public enum InputActionType
	{
		Started,
		Performed,
		Canceled
	}

	//Defined static class helpers:
	public static class UnrealIntegrationAddons
	{
		//Is an actor casting check:
		public static bool IsActor(MonoBehaviour mono, out Actor actor)
		{
			if(mono is Actor)
			{
				actor = (Actor)mono;
				return true;
			}
			actor = default(Actor);
			return false;
		}

		//Very costly, use sparingly (it goes through every GameObejct in the scene and check the type and stops on first find) - O(1) best case, O(N^N) worst case:
		public static bool TryGetActorOfClass(out Actor actor)
		{
			actor = UnityEngine.Object.FindObjectOfType<Actor>();
			if(actor is null) return false;
			return true;
		}
		
		//Very costly, use sparingly (it goes through every GameObejct in the scene and check the type and iterates through the entire scene) - O(N^N) best case time:
		public static bool TryGetActorsOfClass(out Actor[] actors)
		{
			actors = UnityEngine.Object.FindObjectsOfType<Actor>();
			if(actors is null || actors.Length < 1) return false;
			return true;
		}
		//If actor is properly destroyed it will call the on destroy method:
		public static void DestroyUnrealObject(UnrealObject uObject)
		{
			if(uObject is null) return;
			uObject.onDestroyCall?.Invoke();
			UnityEngine.Object.Destroy(uObject);
		}
		public static void DestroyActor(this Actor actor)
		{
			if(actor is null) return;
			actor.onDestroyCall?.Invoke();
			UnityEngine.Object.Destroy(actor);
		}
	}
	public static class GameManager
	{
		private static Gamemode _instance;
		public static Gamemode Instance
		{
			get { return _instance; }
		}
		private static LevelBlueprint _levelBlueprint;
		public static LevelBlueprint LevelBlueprint
		{
			get { return _levelBlueprint; }
		}
		public static void SetGamemode(Gamemode gamemode)
		{
			_instance = gamemode;
		}
		public static void SetLevelBlueprint(LevelBlueprint levelBlueprint)
		{
			_levelBlueprint = levelBlueprint;
		}
	}

	//Defined Classes:
	public abstract class UnrealObject : MonoBehaviour
	{
		//On proper actor destroys:
		public Action onDestroyCall;

		protected virtual void InitializedByGamemode() { }

		//Methods:
		private void Awake() { BeginPlay(); }
		private void Update() { EventTick(); }
		private void FixedUpdate() { EventTickLate(); }
		private void LateUpdate() { EventTickFixed(); }
		protected virtual void BeginPlay() { }
		protected virtual void EventTick() { }
		protected virtual void EventTickLate() { }
		protected virtual void EventTickFixed() { }
	}
	public abstract class UnrealUIObject : MonoBehaviour 
	{
		protected UIWidget parent;
		private void Awake() 
		{
			parent = GetComponentInParent<UIWidget>();
			BeginPlay(); 
		}
		protected virtual void BeginPlay() { }
	}
	public abstract class Gamemode : UnrealObject 
	{
		private readonly float HEIGHTCHECK = 100000;
		[SerializeField] private Pawn pawnType;
		[SerializeField] private HUD hudType;
		[SerializeField] private LevelBlueprint levelBlueprintType;
		[SerializeField] private PlayerStart playerStart;

		private void Start()
		{
			//Check for empty fields:
			if(!ErrorCatch()) return;
			
			//Set gamemode:
			GameManager.SetGamemode(this);

			//Setup pawn and hud:
			Pawn pawn = Instantiate(pawnType.gameObject, GrabWorldPlayerSpawn(), GetWorldPlayerRotation(), transform).GetComponent<Pawn>();
			pawn.hud = Instantiate(hudType.gameObject, GrabWorldPlayerSpawn(), GetWorldPlayerRotation(), transform).GetComponent<HUD>();

			//Level blueprint:
			if (levelBlueprintType is not null)
			{
				GameManager.SetLevelBlueprint(Instantiate(levelBlueprintType.gameObject, GrabWorldPlayerSpawn(), 
					GetWorldPlayerRotation(), transform).GetComponent<LevelBlueprint>());
			}

			//Finished gamemode initialization: attempt to invoke actions and start level blueprint:
			pawn.GamemodeInitialized?.Invoke();
			pawn.hud.GamemodeInitialized?.Invoke();
			if(GameManager.LevelBlueprint is not null) StartCoroutine(GameManager.LevelBlueprint.LevelStart());
		}
		private bool ErrorCatch()
		{
			if (pawnType is null)
			{
				Debug.Log("<color=#ff8200>ERROR: </color>No Pawn type specified in Gamemode instance.");
				return false;
			}
			if (hudType is null)
			{
				Debug.Log("<color=#ff8200>ERROR: </color>No HUD type specified in Gamemode instance.");
				return false;
			}
			return true;
		}
		private Vector3 GrabWorldPlayerSpawn()
		{
			if(playerStart is null)
				if(Physics.Raycast(Vector3.up * HEIGHTCHECK, Vector3.down, out RaycastHit hitResult, 999999))
					return hitResult.point;
			return Vector3.zero;
		}
		private Quaternion GetWorldPlayerRotation()
		{
			if(playerStart is null) return Quaternion.identity;
			return playerStart.transform.rotation;
		}
	}
	public abstract class HUD : UnrealObject 
	{
		public Action GamemodeInitialized;
		public bool AddToViewPort<Widget>(UIWidget widget, out Widget instancedWidget) where Widget : UIWidget
		{
			if (widget is null || widget is not Widget)
			{
				instancedWidget = default(Widget);
				return false;
			}
			instancedWidget = Instantiate(widget, transform).GetComponent<Widget>();
			return true;
		}
	}
	public abstract class LevelBlueprint : UnrealObject
	{
		public virtual IEnumerator LevelStart() { yield break; }
	}
	public abstract class Actor	: UnrealObject {}
	public abstract class Pawn : Actor
	{
		public Action GamemodeInitialized;
		[HideInInspector] public HUD hud;

		//Controls - [NOTE needs to be casted to get Controls back]:
		private IInputActionCollection inputInstance = null;

		//Validate Input will take in a Template of a IInputActionCollection interface and make a new control:
		private Controls ValidateInput<Controls>() where Controls : IInputActionCollection, new()
		{
			inputInstance ??= new Controls();
			return (Controls)inputInstance;
		}
		public Controls GrabInputMappingContext<Controls>() where Controls : IInputActionCollection, new()
		{
			var controls = ValidateInput<Controls>();
			return controls;
		}

		//Create Input:
		public Controls CreateInput<Controls>() where Controls : IInputActionCollection, new()
		{
			var controls = ValidateInput<Controls>();
			return controls;
		}

		//Enable Input:
		public bool EnableInput()
		{
			if(inputInstance is null) return false;
			inputInstance.Enable();
			return true;
		}

		//Disables input:
		public bool DisableInput()
		{
			if(inputInstance is null) return false;
			inputInstance.Disable();
			return true;
		}

		//Subscribe to an event on button action type:
		public void SubscribeToEvent(InputAction action, InputActionType type, Action<InputAction.CallbackContext> eventToCall)
		{
			switch(type)
			{
				case InputActionType.Started:
					action.started += eventToCall;
					return;
				case InputActionType.Performed:
					action.performed += eventToCall;
					return;
				case InputActionType.Canceled:
					action.canceled += eventToCall;
					return;
			}
		}

		//Ubsubscribe to an event on button action type:
		public void UnsubscribeToEvent(InputAction action, InputActionType type, Action<InputAction.CallbackContext> eventToCall)
		{
			switch(type)
			{
				case InputActionType.Started:
					action.started -= eventToCall;
					return;
				case InputActionType.Performed:
					action.performed -= eventToCall;
					return;
				case InputActionType.Canceled:
					action.canceled -= eventToCall;
					return;
			}
		}
	}
	public abstract class CharacterController : Pawn 
	{
		
	}
	public abstract class UIWidget : UnrealObject 
	{
		
	}
}

