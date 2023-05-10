//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/RGD_Core/Input/RGD_MainControls.inputactions
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

public partial class @RGD_MainControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @RGD_MainControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RGD_MainControls"",
    ""maps"": [
        {
            ""name"": ""MainInputActionMap"",
            ""id"": ""c1271a13-bcf6-4f6c-980e-ecb63cc6c662"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""78f89e48-19f0-47f9-80d2-6e6b1f6a0258"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ActionSpace"",
                    ""type"": ""Button"",
                    ""id"": ""ed63d780-d19d-4691-888c-b964acb54dd8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionQ"",
                    ""type"": ""Button"",
                    ""id"": ""3e6877fb-062d-439a-ada8-fc9e41ab3006"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionE"",
                    ""type"": ""Button"",
                    ""id"": ""9229edad-a477-49a2-955e-94c8f4fde8ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionR"",
                    ""type"": ""Button"",
                    ""id"": ""73337b22-a735-4807-941c-3be560ebedd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ActionF"",
                    ""type"": ""Button"",
                    ""id"": ""ea72fd6b-723d-4c46-add8-5d17d128909e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action1"",
                    ""type"": ""Button"",
                    ""id"": ""943a0903-d45f-422a-b264-95e61396faec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action2"",
                    ""type"": ""Button"",
                    ""id"": ""13872233-1a17-4f35-ad1c-feb420454baa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Action3"",
                    ""type"": ""Button"",
                    ""id"": ""37aca26a-b0e4-4196-82f4-fffbe22e714b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""429ea529-3308-4b73-a80b-25ca34b60723"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc589ef2-2e1a-443f-b0ed-eeec8eafe269"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""92c093b8-b5bc-4203-ad50-a95ac787a9a1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7a1f362c-9a1e-441b-a118-2d5b1ae431c1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7e771b1a-054f-44bf-8cdc-a4e044bcad87"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2a94f7ad-ad39-46ff-85e4-fa7de0c00af4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionSpace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb81474e-9e2a-49a7-8f37-5cc956331355"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionQ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d8c1524-9060-4912-b281-63eceef61b0d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionE"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ad52574-984b-4b41-a06d-1eec9b271f3d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7aa200a1-77cb-480e-9692-d08e9be51127"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionF"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2566a2cd-31d3-47f7-8558-bb76187c89c2"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2239020a-2f41-44d8-8fd1-5777e3f2ed3a"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec036337-1690-46be-85e9-0e00c1d78184"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Action3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainInputActionMap
        m_MainInputActionMap = asset.FindActionMap("MainInputActionMap", throwIfNotFound: true);
        m_MainInputActionMap_Movement = m_MainInputActionMap.FindAction("Movement", throwIfNotFound: true);
        m_MainInputActionMap_ActionSpace = m_MainInputActionMap.FindAction("ActionSpace", throwIfNotFound: true);
        m_MainInputActionMap_ActionQ = m_MainInputActionMap.FindAction("ActionQ", throwIfNotFound: true);
        m_MainInputActionMap_ActionE = m_MainInputActionMap.FindAction("ActionE", throwIfNotFound: true);
        m_MainInputActionMap_ActionR = m_MainInputActionMap.FindAction("ActionR", throwIfNotFound: true);
        m_MainInputActionMap_ActionF = m_MainInputActionMap.FindAction("ActionF", throwIfNotFound: true);
        m_MainInputActionMap_Action1 = m_MainInputActionMap.FindAction("Action1", throwIfNotFound: true);
        m_MainInputActionMap_Action2 = m_MainInputActionMap.FindAction("Action2", throwIfNotFound: true);
        m_MainInputActionMap_Action3 = m_MainInputActionMap.FindAction("Action3", throwIfNotFound: true);
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

    // MainInputActionMap
    private readonly InputActionMap m_MainInputActionMap;
    private List<IMainInputActionMapActions> m_MainInputActionMapActionsCallbackInterfaces = new List<IMainInputActionMapActions>();
    private readonly InputAction m_MainInputActionMap_Movement;
    private readonly InputAction m_MainInputActionMap_ActionSpace;
    private readonly InputAction m_MainInputActionMap_ActionQ;
    private readonly InputAction m_MainInputActionMap_ActionE;
    private readonly InputAction m_MainInputActionMap_ActionR;
    private readonly InputAction m_MainInputActionMap_ActionF;
    private readonly InputAction m_MainInputActionMap_Action1;
    private readonly InputAction m_MainInputActionMap_Action2;
    private readonly InputAction m_MainInputActionMap_Action3;
    public struct MainInputActionMapActions
    {
        private @RGD_MainControls m_Wrapper;
        public MainInputActionMapActions(@RGD_MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_MainInputActionMap_Movement;
        public InputAction @ActionSpace => m_Wrapper.m_MainInputActionMap_ActionSpace;
        public InputAction @ActionQ => m_Wrapper.m_MainInputActionMap_ActionQ;
        public InputAction @ActionE => m_Wrapper.m_MainInputActionMap_ActionE;
        public InputAction @ActionR => m_Wrapper.m_MainInputActionMap_ActionR;
        public InputAction @ActionF => m_Wrapper.m_MainInputActionMap_ActionF;
        public InputAction @Action1 => m_Wrapper.m_MainInputActionMap_Action1;
        public InputAction @Action2 => m_Wrapper.m_MainInputActionMap_Action2;
        public InputAction @Action3 => m_Wrapper.m_MainInputActionMap_Action3;
        public InputActionMap Get() { return m_Wrapper.m_MainInputActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainInputActionMapActions set) { return set.Get(); }
        public void AddCallbacks(IMainInputActionMapActions instance)
        {
            if (instance == null || m_Wrapper.m_MainInputActionMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainInputActionMapActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @ActionSpace.started += instance.OnActionSpace;
            @ActionSpace.performed += instance.OnActionSpace;
            @ActionSpace.canceled += instance.OnActionSpace;
            @ActionQ.started += instance.OnActionQ;
            @ActionQ.performed += instance.OnActionQ;
            @ActionQ.canceled += instance.OnActionQ;
            @ActionE.started += instance.OnActionE;
            @ActionE.performed += instance.OnActionE;
            @ActionE.canceled += instance.OnActionE;
            @ActionR.started += instance.OnActionR;
            @ActionR.performed += instance.OnActionR;
            @ActionR.canceled += instance.OnActionR;
            @ActionF.started += instance.OnActionF;
            @ActionF.performed += instance.OnActionF;
            @ActionF.canceled += instance.OnActionF;
            @Action1.started += instance.OnAction1;
            @Action1.performed += instance.OnAction1;
            @Action1.canceled += instance.OnAction1;
            @Action2.started += instance.OnAction2;
            @Action2.performed += instance.OnAction2;
            @Action2.canceled += instance.OnAction2;
            @Action3.started += instance.OnAction3;
            @Action3.performed += instance.OnAction3;
            @Action3.canceled += instance.OnAction3;
        }

        private void UnregisterCallbacks(IMainInputActionMapActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @ActionSpace.started -= instance.OnActionSpace;
            @ActionSpace.performed -= instance.OnActionSpace;
            @ActionSpace.canceled -= instance.OnActionSpace;
            @ActionQ.started -= instance.OnActionQ;
            @ActionQ.performed -= instance.OnActionQ;
            @ActionQ.canceled -= instance.OnActionQ;
            @ActionE.started -= instance.OnActionE;
            @ActionE.performed -= instance.OnActionE;
            @ActionE.canceled -= instance.OnActionE;
            @ActionR.started -= instance.OnActionR;
            @ActionR.performed -= instance.OnActionR;
            @ActionR.canceled -= instance.OnActionR;
            @ActionF.started -= instance.OnActionF;
            @ActionF.performed -= instance.OnActionF;
            @ActionF.canceled -= instance.OnActionF;
            @Action1.started -= instance.OnAction1;
            @Action1.performed -= instance.OnAction1;
            @Action1.canceled -= instance.OnAction1;
            @Action2.started -= instance.OnAction2;
            @Action2.performed -= instance.OnAction2;
            @Action2.canceled -= instance.OnAction2;
            @Action3.started -= instance.OnAction3;
            @Action3.performed -= instance.OnAction3;
            @Action3.canceled -= instance.OnAction3;
        }

        public void RemoveCallbacks(IMainInputActionMapActions instance)
        {
            if (m_Wrapper.m_MainInputActionMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainInputActionMapActions instance)
        {
            foreach (var item in m_Wrapper.m_MainInputActionMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainInputActionMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainInputActionMapActions @MainInputActionMap => new MainInputActionMapActions(this);
    public interface IMainInputActionMapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnActionSpace(InputAction.CallbackContext context);
        void OnActionQ(InputAction.CallbackContext context);
        void OnActionE(InputAction.CallbackContext context);
        void OnActionR(InputAction.CallbackContext context);
        void OnActionF(InputAction.CallbackContext context);
        void OnAction1(InputAction.CallbackContext context);
        void OnAction2(InputAction.CallbackContext context);
        void OnAction3(InputAction.CallbackContext context);
    }
}