using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadButton : MonoBehaviour
{
    [SerializeField] private Button reloadButton;

    private void Awake()
    {
        reloadButton.onClick.AddListener(() => EntryPoint.Instance.OnReload());
    }
}