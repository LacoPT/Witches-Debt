using UnityEngine;
using UnityEngine.UI;

public class SaveAndQuitButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private NextSceneIndexSelector sceneIndexSelector;
    private void Awake()
    {
        button.onClick.AddListener(SaveAndQuit);
    }

    private void SaveAndQuit()
    {
        var entryPoint = EntryPoint.Instance;
        entryPoint.Save(sceneIndexSelector.NextSceneIndex);
        entryPoint.LoadMenu();
    }
}
