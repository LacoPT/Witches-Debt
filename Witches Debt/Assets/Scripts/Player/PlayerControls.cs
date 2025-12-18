using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindActionMap("Player").FindAction("Move");
    }

    public string OnRebindMove(int bindingIndex) => OnRebind(moveAction, bindingIndex);

    private string OnRebind(InputAction action, int bindingIndex)
    {
        Debug.Log(action.bindings[bindingIndex].effectivePath);

        action.Disable();
        action.PerformInteractiveRebinding(bindingIndex)
            .OnMatchWaitForAnother(0.2f)
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                Debug.Log(action.bindings[bindingIndex].effectivePath);
            })
            .Start();
        return action.bindings[bindingIndex].effectivePath;
    }
}
