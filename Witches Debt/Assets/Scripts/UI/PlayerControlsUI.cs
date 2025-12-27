using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class PlayerControlsUI : MonoBehaviour
{
    [SerializedDictionary] public SerializedDictionary<Button, PlayerControlsElement> buttonToText;
    [SerializeField] private string changeBindText;
    private TMP_Text lastChanged;
    private PlayerControls playerControls;

    [Inject]
    public void Construct(PlayerControls playerControls)
    {
        this.playerControls = playerControls;
        playerControls.InputSet += Initialize;
        if (playerControls.InputIsSet) Initialize();
        playerControls.BindChanged += OnBindChanged;
    }

    private void Initialize()
    {
        foreach (var (button, element) in buttonToText)
        {
            button.onClick.AddListener(() => OnRebind(element.desc, element.index));
            lastChanged = element.desc;
            OnBindChanged(playerControls.MoveAction.bindings[element.index].effectivePath);
        }
        playerControls.InputSet -= Initialize;
    }

    private void OnDestroy()
    {
        playerControls.BindChanged -= OnBindChanged;
        playerControls.OnSceneUnload();
    }

    private void OnRebind(TMP_Text text, int bindingIndex)
    {
        text.text = changeBindText;
        lastChanged = text;
        playerControls.OnRebindMove(bindingIndex);
    }

    private void OnBindChanged(string newBindText)
    {
        lastChanged.text = newBindText.Replace("<Keyboard>/", "").ToUpper();
    }
}   