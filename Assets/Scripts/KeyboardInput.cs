// GENERATED AUTOMATICALLY FROM 'Assets/Keyboard.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @KeyboardInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @KeyboardInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Keyboard"",
    ""maps"": [
        {
            ""name"": ""Keyboard"",
            ""id"": ""e7b5d7ff-eedb-4aaf-9248-02eec9663d2a"",
            ""actions"": [
                {
                    ""name"": ""Space Bar"",
                    ""type"": ""Button"",
                    ""id"": ""7ed00485-8bed-4e36-b26d-922c3e7a6b33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f4b24a3e-bf31-4020-aca7-4a2d633cd212"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space Bar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Keyboard
        m_Keyboard = asset.FindActionMap("Keyboard", throwIfNotFound: true);
        m_Keyboard_SpaceBar = m_Keyboard.FindAction("Space Bar", throwIfNotFound: true);
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

    // Keyboard
    private readonly InputActionMap m_Keyboard;
    private IKeyboardActions m_KeyboardActionsCallbackInterface;
    private readonly InputAction m_Keyboard_SpaceBar;
    public struct KeyboardActions
    {
        private @KeyboardInput m_Wrapper;
        public KeyboardActions(@KeyboardInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @SpaceBar => m_Wrapper.m_Keyboard_SpaceBar;
        public InputActionMap Get() { return m_Wrapper.m_Keyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardActions instance)
        {
            if (m_Wrapper.m_KeyboardActionsCallbackInterface != null)
            {
                @SpaceBar.started -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSpaceBar;
                @SpaceBar.performed -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSpaceBar;
                @SpaceBar.canceled -= m_Wrapper.m_KeyboardActionsCallbackInterface.OnSpaceBar;
            }
            m_Wrapper.m_KeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SpaceBar.started += instance.OnSpaceBar;
                @SpaceBar.performed += instance.OnSpaceBar;
                @SpaceBar.canceled += instance.OnSpaceBar;
            }
        }
    }
    public KeyboardActions @Keyboard => new KeyboardActions(this);
    public interface IKeyboardActions
    {
        void OnSpaceBar(InputAction.CallbackContext context);
    }
}
