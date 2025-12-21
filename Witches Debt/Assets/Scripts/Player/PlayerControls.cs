using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    public UnityEvent<string> BindChanged;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindActionMap("Player").FindAction("Move");
    }

    public void OnRebindMove(int bindingIndex) => OnRebind(moveAction, bindingIndex);

    private void OnRebind(InputAction action, int bindingIndex)
    {
        action.Disable();
        action.PerformInteractiveRebinding(bindingIndex)
            .OnMatchWaitForAnother(0.2f)
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                BindChanged?.Invoke(action.bindings[bindingIndex].effectivePath);
            })
            .Start();
        
    }
}
