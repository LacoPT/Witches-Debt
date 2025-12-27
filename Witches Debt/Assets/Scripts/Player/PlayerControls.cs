using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls
{
    private PlayerInput playerInput;
    public InputAction MoveAction => playerInput.actions.FindActionMap("Player").FindAction("Move");
    private string bindingsPath => Application.persistentDataPath + "/bindings.json";
    public bool InputIsSet { get; private set; } = false;
    public event Action InputSet;
    public event Action<string> BindChanged;
    public void OnRebindMove(int bindingIndex) => OnRebind(MoveAction, bindingIndex);

    private void OnRebind(InputAction action, int bindingIndex)
    {
        Debug.Log(BindChanged == null);
        action.Disable();
        action.PerformInteractiveRebinding(bindingIndex)
            .OnMatchWaitForAnother(0.2f)
            .OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                BindChanged?.Invoke(action.bindings[bindingIndex].effectivePath);
                SaveBindings();
            })
            .Start();
    }

    public void SetPlayerInput(PlayerInput input)
    {
        playerInput = input;
        LoadBindings();
        InputSet?.Invoke();
        InputIsSet = true;
    }

    private void SaveBindings()
    {
        var json = playerInput.actions.SaveBindingOverridesAsJson();
        File.WriteAllText(bindingsPath, json);
    }

    private void LoadBindings()
    {
        if (!File.Exists(bindingsPath)) return;
        var json = File.ReadAllText(bindingsPath);
        playerInput.actions.LoadBindingOverridesFromJson(json);
        playerInput.actions.Enable();
    }

    public void OnSceneUnload() => InputIsSet = false;
}
