using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveAndQuitButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int nextSceneIndex;
    private void Awake()
    {
        button.onClick.AddListener(SaveAndQuit);
    }

    private void SaveAndQuit()
    {
        var entryPoint = EntryPoint.Instance;
        entryPoint.OnSave(nextSceneIndex);
        entryPoint.OnMenuLoad();
    }
}
