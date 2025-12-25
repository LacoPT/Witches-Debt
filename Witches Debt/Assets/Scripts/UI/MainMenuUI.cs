using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startRun;
    [SerializeField] private Button continueRun;
    private void Awake()
    {
        startRun.onClick.AddListener(() => EntryPoint.Instance.OnDefaultLoad());
        continueRun.onClick.AddListener(() => EntryPoint.Instance.OnLoad());
    }
}
