//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/RGD_Core/Input/RGD_Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @RGD_Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @RGD_Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RGD_Controls"",
    ""maps"": [
        {
            ""name"": ""DefaultMapping"",
            ""id"": ""1665f2bd-b0f0-4af7-9bb5-cc24f882ad65"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""ab5709bb-ba09-466d-a7cf-bf378cd7759b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""c5d252c9-d62e-4b3c-b018-9cff376f3143"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""103949ed-c213-4f95-b646-14d768d93fec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9238b264-e3ab-454c-90bb-25286b2a0052"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7de95bb5-238f-4daf-aa39-d89bad5be42a"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""MoveComposit"",
                    ""id"": ""e1943aea-91d4-4d0a-91a2-74f6f0dcb75e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f1f1ecf8-6166-4ecf-b31b-4b361869fa5c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""30204ddf-3f5e-456c-8574-62a5b2cd8eef"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7c217ea0-9ea3-4036-aa8d-b777adfeb95e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97412231-6415-4c5e-83a1-3e6c9386c578"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // DefaultMapping
        m_DefaultMapping = asset.FindActionMap("DefaultMapping", throwIfNotFound: true);
        m_DefaultMapping_Jump = m_DefaultMapping.FindAction("Jump", throwIfNotFound: true);
        m_DefaultMapping_Move = m_DefaultMapping.FindAction("Move", throwIfNotFound: true);
        m_DefaultMapping_Sprint = m_DefaultMapping.FindAction("Sprint", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // DefaultMapping
    private readonly InputActionMap m_DefaultMapping;
    private List<IDefaultMappingActions> m_DefaultMappingActionsCallbackInterfaces = new List<IDefaultMappingActions>();
    private readonly InputAction m_DefaultMapping_Jump;
    private readonly InputAction m_DefaultMapping_Move;
    private readonly InputAction m_DefaultMapping_Sprint;
    public struct DefaultMappingActions
    {
        private @RGD_Controls m_Wrapper;
        public DefaultMappingActions(@RGD_Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_DefaultMapping_Jump;
        public InputAction @Move => m_Wrapper.m_DefaultMapping_Move;
        public InputAction @Sprint => m_Wrapper.m_DefaultMapping_Sprint;
        public InputActionMap Get() { return m_Wrapper.m_DefaultMapping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultMappingActions set) { return set.Get(); }
        public void AddCallbacks(IDefaultMappingActions instance)
        {
            if (instance == null || m_Wrapper.m_DefaultMappingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DefaultMappingActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
        }

        private void UnregisterCallbacks(IDefaultMappingActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
        }

        public void RemoveCallbacks(IDefaultMappingActions instance)
        {
            if (m_Wrapper.m_DefaultMappingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDefaultMappingActions instance)
        {
            foreach (var item in m_Wrapper.m_DefaultMappingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DefaultMappingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DefaultMappingActions @DefaultMapping => new DefaultMappingActions(this);
    public interface IDefaultMappingActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
    }
}
