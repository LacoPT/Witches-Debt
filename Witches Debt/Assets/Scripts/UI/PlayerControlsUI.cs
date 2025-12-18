using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControlsUI : MonoBehaviour
{
    [SerializeField] private Button up;
    [SerializeField] private Button down;
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [SerializeField] private TMP_Text upText;
    [SerializeField] private TMP_Text downText;
    [SerializeField] private TMP_Text leftText;
    [SerializeField] private TMP_Text rightText;
    [SerializeField] private PlayerControls playerControls;

    private void Awake()
    {
        up.onClick.AddListener(() => OnRebind(upText, 1));
        down.onClick.AddListener(() => OnRebind(downText, 3));
        left.onClick.AddListener(() => OnRebind(leftText, 5));
        right.onClick.AddListener(() => OnRebind(rightText, 7));
    }

    private void OnRebind(TMP_Text text, int bindingIndex)
    {
        text.text = playerControls.OnRebindMove(bindingIndex);
    }
}   