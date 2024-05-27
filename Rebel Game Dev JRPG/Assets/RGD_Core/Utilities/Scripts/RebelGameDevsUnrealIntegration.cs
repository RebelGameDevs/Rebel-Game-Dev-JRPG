/*
Author: Brandon Bird (Mythic)
Date: 3/30/24
Purpose: To allow for Unreal Engine Integration support in Unity for UNLV Rebel Game Devs Open Source project.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR.Haptics;
using System.Xml.Linq;
using Unity.VisualScripting;

namespace RebelGameDevs.Utils.UnrealIntegration
{
	//Defined Types:
	public enum InputActionType
	{
		Started,
		Performed,
		Held,
		Canceled
	}

	//Defined static class helpers:
	public static class UnrealIntegrationAddons
	{

		public static void SetMouseOptions(bool isVisible)
		{
			Cursor.visible = isVisible;
		}
		public static void SetMouseOptions(CursorLockMode state)
		{
			Cursor.lockState = state;
		}
		public static void SetMouseOptions(bool isVisible, CursorLockMode state)
		{
			Cursor.lockState = state;
			Cursor.visible = isVisible;
		}

		//Casting:
		public static bool CastToActor(MonoBehaviour mono, out Actor actor)
		{
			if (mono is Actor)
			{
				actor = (Actor)mono;
				return true;
			}
			actor = default(Actor);
			return false;
		}


		public static bool CastToUnrealModule(UnityEngine.Object scriptable, out UnrealModule uModule)
		{
			if (scriptable is ScriptableObject)
			{
				uModule = (UnrealModule)scriptable;
				return true;
			}
			uModule = default(UnrealModule);
			return false;
		}

		//Very costly, use sparingly (it goes through every GameObejct in the scene and check the type and stops on first find) - O(1) best case, O(N^N) worst case:
		public static bool TryGetActorOfClass(out Actor actor)
		{
			actor = UnityEngine.Object.FindObjectOfType<Actor>();
			if (actor is null) return false;
			return true;
		}

		//Very costly, use sparingly (it goes through every GameObejct in the scene and check the type and iterates through the entire scene) - O(N^N) best case time:
		public static bool TryGetActorsOfClass(out Actor[] actors)
		{
			actors = UnityEngine.Object.FindObjectsOfType<Actor>();
			if (actors is null || actors.Length < 1) return false;
			return true;
		}
		//If actor is properly destroyed it will call the on destroy method:
		public static void DestroyUnrealObject(UnrealObject uObject)
		{
			if (uObject is null) return;
			uObject.onDestroyCall?.Invoke();
			UnityEngine.Object.Destroy(uObject);
		}
		public static void DestroyActor(this Actor actor)
		{
			if (actor is null) return;
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
		private static EventSystem _eventSystem;
		public static EventSystem EventSystem;
		public static void SetGamemode(Gamemode gamemode)
		{
			_instance = gamemode;
		}
		public static void SetLevelBlueprint(LevelBlueprint levelBlueprint)
		{
			_levelBlueprint = levelBlueprint;
		}
		public static void InitializeEventSystem()
		{
			_eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
			if (_eventSystem is not null) return;

			GameObject eventSystemObject = new GameObject("EventSystem", new Type[] { typeof(EventSystem), typeof(StandaloneInputModule) });
			_eventSystem = eventSystemObject.GetComponent<EventSystem>();
		}
	}

	//Defined Classes:
	public abstract class UnrealObject : MonoBehaviour
	{
		//On proper actor destroys:
		public Action onDestroyCall;
		public Action onInitializeByGamemode;

		//Methods:
		public virtual void InitializedByGamemode() { onInitializeByGamemode?.Invoke(); }
		private void Awake() { BeginPlay(); }
		private void Update() { EventTick(); }
		private void FixedUpdate() { EventTickLate(); }
		private void LateUpdate() { EventTickFixed(); }
		protected virtual void BeginPlay() { }
		protected virtual void EventTick() { }
		protected virtual void EventTickLate() { }
		protected virtual void EventTickFixed() { }
	}
	public abstract class UnrealUIObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		[HideInInspector] public UIWidget parent;
		protected UnityEngine.UI.Graphic graphic;
		public Action onStartHover, onStopHover, onPressed, onReleased, onEnabled, onDisabled;
		protected bool isDisabled => !graphic.raycastTarget;
		private void Awake()
		{
			//Find graphic:
			if (!TryGetComponent(out graphic)) Debug.Log($"ERROR: No Graphic on UI Object: <color=green>{this}</color>. " +
				$"NOTE: Graphic components are items such as Image, Text Mesh Pro, Button, Sprite, etc.");

			//Find parent widget:
			parent = GetComponentInParent<UIWidget>();
			if (parent is null) Debug.Log($"ERROR: No parent Widget found for UI Object: <color=green>{this}</color>.");

			//Call "BeginPlay" method:
			BeginPlay();
		}
		public void Enable()
		{
			graphic.raycastTarget = true;
			onEnabled?.Invoke();
			Enabled();
		}
		public void Disable()
		{
			graphic.raycastTarget = false;
			onDisabled?.Invoke();
			Disabled();
		}
		protected virtual void BeginPlay() { }

		//Hovered On:
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (isDisabled) return;
			onStartHover?.Invoke();
			StartHovering();
		}
		//Hovered Off:
		public void OnPointerExit(PointerEventData eventData)
		{
			if (isDisabled) return;
			onStopHover?.Invoke();
			StopHovering();
		}

		//Clicked:
		public void OnPointerDown(PointerEventData eventData)
		{
			if (isDisabled) return;
			onPressed?.Invoke();
			Pressed();
		}

		//Released:
		public void OnPointerUp(PointerEventData eventData)
		{
			if (isDisabled) return;
			onReleased?.Invoke();
			Released();
		}
		protected virtual void StartHovering() { }
		protected virtual void StopHovering() { }
		protected virtual void Pressed() { }
		protected virtual void Released() { }
		protected virtual void Disabled() { }
		protected virtual void Enabled() { }
	}
	public abstract class UnrealModule : ScriptableObject { }
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
			if (!ErrorCatch()) return;

			//Set gamemode:
			GameManager.SetGamemode(this);

			//Setup event system if there is none in the scene:
			GameManager.InitializeEventSystem();

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
			pawn.InitializedByGamemode();
			pawn.hud.InitializedByGamemode();
			if (GameManager.LevelBlueprint is not null) StartCoroutine(GameManager.LevelBlueprint.LevelStart());
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
			if (playerStart != null) return playerStart.transform.position;
			if (Physics.Raycast(Vector3.up * HEIGHTCHECK, Vector3.down, out RaycastHit hitResult, 999999)) return hitResult.point;
			return Vector3.zero;
		}
		private Quaternion GetWorldPlayerRotation()
		{
			if (playerStart != null) return playerStart.transform.rotation;
			return Quaternion.identity;
		}
	}
	public abstract class HUD : UnrealObject
	{
		[HideInInspector] public Pawn pawn;
		public bool AddToViewPort<Widget>(UIWidget widget, out Widget instancedWidget) where Widget : UIWidget
		{
			if (widget is null || widget is not Widget)
			{
				instancedWidget = default(Widget);
				return false;
			}
			instancedWidget = Instantiate(widget, transform).GetComponent<Widget>();
			instancedWidget.parent = pawn;
			return true;
		}
	}
	public abstract class LevelBlueprint : UnrealObject
	{
		public virtual IEnumerator LevelStart() { yield break; }
	}
	public abstract class Actor : UnrealObject { }
	[System.Serializable] public abstract class ActorComponent
	{
		//No Methods like Update or Begin Play, this is meant to house methods
		//Not tied to an actor to act as a component module class created inside of the actor class.
		[HideInInspector] public UnrealObject parent;
		public virtual void Initialize(UnrealObject uObject) { parent = uObject; }
	}
	public static class UnrealInput
	{
		private static Dictionary<UnrealObject, ObjectInputData> dictionary = new Dictionary<UnrealObject, ObjectInputData>();
		
		private class InputPerformedHandler
		{
			public Coroutine currentCo;
			public Action<InputAction.CallbackContext> onPerformed;
			public Action<InputAction.CallbackContext> onCanceled;
			public InputPerformedHandler(Action<InputAction.CallbackContext> actionToCall, UnrealObject uObject)
			{
				onPerformed += (context) =>
				{
					currentCo = uObject.StartCoroutine(WhileHeld(actionToCall, context));
				};
				onCanceled += (context) =>
				{
					if (currentCo is not null) uObject.StopCoroutine(currentCo);
				};
			}

			private IEnumerator WhileHeld(Action<InputAction.CallbackContext> eventToCall, InputAction.CallbackContext context)
			{
				//while the action has not been cancelled
				while(true)
				{
					eventToCall?.Invoke(context);
					yield return null;
				}
			}
		}
		private class ObjectInputData
		{
			public UnrealObject parent;

			//Controls - [NOTE needs to be casted to get Controls back]:
			public IInputActionCollection inputInstance = null;
			public Dictionary<InputAction, InputPerformedHandler> whileHeldActions;
			//Validate Input will take in a Template of a IInputActionCollection interface and make a new control:

			public ObjectInputData (UnrealObject uObject)
			{
				parent = uObject;
			}
			private Controls ValidateInput<Controls>() where Controls : IInputActionCollection, new()
			{
				if(inputInstance is null)
				{
					inputInstance = new Controls();
					whileHeldActions = new Dictionary<InputAction, InputPerformedHandler>();
				}
				return (Controls)inputInstance;
			}
			public Controls GetInputMappingContext<Controls>() where Controls : IInputActionCollection, new()
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
					case InputActionType.Held:
						//Create a new class to hold the event and pass a Unreal Object type and store in dictionary:
						whileHeldActions[action] = new InputPerformedHandler(eventToCall, parent);

						//subscribe:
						action.performed += whileHeldActions[action].onPerformed;
						action.canceled += whileHeldActions[action].onCanceled;
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
					case InputActionType.Held:
						//If not contians key - leave:
						if(!whileHeldActions.ContainsKey(action)) return;

						//else unsubscribe:
						action.performed -= whileHeldActions[action].onPerformed;
						action.canceled -= whileHeldActions[action].onCanceled;
						whileHeldActions.Remove(action);
						return;
					case InputActionType.Canceled:
						action.canceled -= eventToCall;
						return;
				}
			}
		}
		public static bool EnableInput(UnrealObject uObject)
		{
			bool value = false;
			if(dictionary.TryGetValue(uObject, out ObjectInputData associtatedInput))
				value = associtatedInput.EnableInput();
			return value;
		}
		public static bool DisableInput(UnrealObject uObject)
		{
			bool value = false;
			if(dictionary.TryGetValue(uObject, out ObjectInputData associtatedInput)) 
				value = associtatedInput.DisableInput();
			return value;
		}
		public static void SubscribeToEvent(UnrealObject uObject, InputAction action, InputActionType type, Action<InputAction.CallbackContext> eventToCall)
		{
			if(dictionary.TryGetValue(uObject, out ObjectInputData associtatedInput))
				associtatedInput.SubscribeToEvent(action, type, eventToCall);
		}
		public static void UnsubscribeToEvent(UnrealObject uObject, InputAction action, InputActionType type, Action<InputAction.CallbackContext> eventToCall)
		{
			if(dictionary.TryGetValue(uObject, out ObjectInputData associtatedInput))
				associtatedInput.UnsubscribeToEvent(action, type, eventToCall);
		}
		public static Controls GetInputMappingContext<Controls>(UnrealObject uObject) where Controls : IInputActionCollection, new()
		{
			if(dictionary.TryGetValue(uObject, out ObjectInputData associtatedInput))
				return associtatedInput.GetInputMappingContext<Controls>();
			return default(Controls);
		}
		public static Controls CreateInput<Controls>(UnrealObject uObject) where Controls : IInputActionCollection, new()
		{
			if(dictionary.TryGetValue(uObject, out ObjectInputData associtatedInput))
				return associtatedInput.CreateInput<Controls>();
			return default(Controls);
		}
		public static bool Map(UnrealObject uObject)
		{
			if(dictionary.ContainsKey(uObject)) return false;
			ObjectInputData newInputData = new ObjectInputData(uObject);
			dictionary.Add(uObject, newInputData);
			return true;
		}
		public static bool UnMap(UnrealObject unrealObject)
		{
			if(dictionary.ContainsKey(unrealObject)) return false;
			dictionary.Remove(unrealObject);
			return true;
		}
	}
	public abstract class Pawn : Actor
	{
		
		[HideInInspector] public HUD hud;
	}
	public abstract class UIWidget : UnrealObject 
	{
		[HideInInspector] public Actor parent;
	}
}

