using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startRun;
    [SerializeField] private Button continueRun;
    private void Start()
    {
        var entryPoint = EntryPoint.Instance;
        startRun.onClick.AddListener(() => entryPoint.OnDefaultLoad());
        continueRun.onClick.AddListener(() => entryPoint.Load());
        continueRun.enabled = entryPoint.IsContinueAvailable();
    }
}
