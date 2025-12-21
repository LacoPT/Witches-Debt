using AYellowpaper.SerializedCollections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControlsUI : MonoBehaviour
{
    [SerializedDictionary] public SerializedDictionary<Button, PlayerControlsElement> buttonToText;
    [SerializeField] private PlayerControls playerControls;
    [SerializeField] private string changeBindText;
    private TMP_Text lastChanged;
    private void Awake()
    {
        foreach (var (button, element) in buttonToText)
        {
            element.desc.text = element.defaultPath;
            button.onClick.AddListener(() => OnRebind(element.desc, element.index));
        }
        playerControls.BindChanged.AddListener(OnBindChanged);
    }

    private void OnRebind(TMP_Text text, int bindingIndex)
    {
        text.text = changeBindText;
        playerControls.OnRebindMove(bindingIndex);
        lastChanged = text;
    }

    private void OnBindChanged(string newBindText)
    {
        lastChanged.text = newBindText.Replace("<Keyboard>/", "").ToUpper();
    }
}   