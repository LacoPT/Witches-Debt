using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Monobehaviour class, that creates gameplay snene (Main scene)
/// and managing initialization flow
/// game can be loaded normally (first default load on Awake) 
/// reloaded (default load, caused by OnReload)
/// saved and loaded from save file
/// </summary>
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private int mainSceneBuildIndex;

    private GameState gameState;
    /// <summary>
    /// Passed to the SaveLoader, to support different types of serialization
    /// (Useful, for example, when you change serialized type from xml/json to binary on release)
    /// </summary>
    private GameSerializer serializer;
    private GameSaveLoader saveLoader = new();

    public static EntryPoint Instance; // TODO: remove with zenject
    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //You can't reference the Application not in method
        serializer = new XMLGameSerializer(Application.persistentDataPath + "/save.xml");
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// Called by input, saves the game
    /// </summary>
    public void OnSave()
    {
        saveLoader.SaveGame(serializer, gameState);
    }

    /// <summary>
    /// loads the game (using Restore state load)
    /// </summary>
    public void OnLoad()
    {
        if (!saveLoader.TryLoadGame(serializer, ref gameState))
        {
            Debug.Log("Failed to load game");
            return;
        }

        StartCoroutine(UnloadScene());
        StartCoroutine(LoadScene(false));
    }

    /// <summary>
    /// Called by input, reloads the game using default load
    /// </summary>
    /// <param name="context"></param>
    public void OnReload(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Reload();
    }

    /// <summary>
    /// A general method that loads the gameplay *scene*, using SceneManager
    /// </summary>
    private IEnumerator LoadScene(bool defaultLoad = true)
    {
        var load = SceneManager.LoadSceneAsync(mainSceneBuildIndex, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(mainSceneBuildIndex));
        if (defaultLoad)
        {
            DefaultLoad();
        }
        else
        {
            RestoreStateLoad();
        }
        BindEvents();
    }

    /// <summary>
    /// Default state of the game, when it's just launched / reloaded.
    /// </summary>
    private void DefaultLoad()
    {
        gameState = new GameState();
        var itemModel = new TestItemModel();
        var item = itemModel.CreateInstance().GetComponent<TestItem>();
        gameState.Create(itemModel);
        gameState.BindItemDespawned(item.ItemPicked);
    }

    /// <summary>
    /// Uses deserialized gameState
    /// </summary>
    private void RestoreStateLoad()
    {
        gameState.Initialize();
    }

    /// <summary>
    /// SceneManager.UnloadScene() is deprecated, and you can only use Async version, but since
    /// PlayerInput with UnityEvents invocation doesn't support Async methods, you simply create a coroutine
    /// that checks every frame if AsyncOperation is done yet
    /// There's also nothing wrong (i think) with creating and destroying scene ant the same time
    /// </summary>
    private IEnumerator UnloadScene()
    {
        var unload = SceneManager.UnloadSceneAsync(mainSceneBuildIndex);
        while (!unload.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Classes that require events from model data subsribe to it by itself
    /// </summary>
    private void BindEvents()
    {
        //UI.Instance.BindEvents(gameState);
    }

    /// <summary>
    /// Uses default scene load
    /// </summary>
    private void Reload()
    {
        StartCoroutine(UnloadScene());
        StartCoroutine(LoadScene());
    }
}