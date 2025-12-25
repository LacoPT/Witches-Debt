using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.Asteroids;

/// <summary>
/// Monobehaviour class, that creates gameplay snene (Main scene)
/// and managing initialization flow
/// game can be loaded normally (first default load on Awake) 
/// reloaded (default load, caused by OnReload)
/// saved and loaded from save file
/// </summary>
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private int menuSceneIndex = 1;
    [SerializeField] private int firstLevelSceneIndex = 2;
    private int nextSceneIndex;
    private int lastSceneIndex;
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
        lastSceneIndex = 0;
        nextSceneIndex = menuSceneIndex;
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //You can't reference the Application not in method
        serializer = new XMLGameSerializer(Application.persistentDataPath + "/save.xml");
        OnMenuLoad();
    }

    /// <summary>
    /// Called by input, saves the game
    /// </summary>
    public void OnSave(int nextSceneIndex = -1)
    {
        if (nextSceneIndex != -1)
        {
            gameState.NextSceneIndex = nextSceneIndex;
            this.nextSceneIndex = nextSceneIndex;
        }
        saveLoader.SaveGame(serializer, gameState);
    }

    /// <summary>
    /// loads the game (using Restore state load)
    /// </summary>
    public void OnLoad(int newSceneIndex = -1)
    {
        lastSceneIndex = nextSceneIndex;
        if (!saveLoader.TryLoadGame(serializer, ref gameState))
        {
            Debug.Log("Failed to load game");
            return;
        }
        nextSceneIndex = (newSceneIndex != -1) ? newSceneIndex : gameState.NextSceneIndex;
        StartCoroutine(LoadScene(defaultLoad: false));
    }

    public void OnDefaultLoad()
    {
        lastSceneIndex = this.nextSceneIndex;
        nextSceneIndex = firstLevelSceneIndex;
        StartCoroutine(LoadScene());
    }

    public void OnMenuLoad()
    {
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = menuSceneIndex;
        StartCoroutine(LoadScene(defaultLoad: false, menuLoad: true));
    }

    /// <summary>
    /// Called by button, reloads the game using default load
    /// </summary>
    public void OnReload()
    {
        Reload();
    }

    /// <summary>
    /// A general method that loads the gameplay *scene*, using SceneManager
    /// </summary>
    private IEnumerator LoadScene(bool defaultLoad = true, bool menuLoad = false)
    {

        var load = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive);
        while (!load.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextSceneIndex));
        if (defaultLoad)
        {
            DefaultLoad();
        }
        else if (!menuLoad)
        {
            RestoreStateLoad();
        }
        StartCoroutine(UnloadScene());
        BindEvents();
    }

    /// <summary>
    /// Default state of the game, when it's just launched / reloaded.
    /// </summary>
    private void DefaultLoad()
    {
        gameState = new GameState();
        var playerModel = new PlayerModel();
        var player = playerModel.CreateInstance().GetComponent<PlayerController>();
        var playerSaveData = playerModel.ToSaveData();
        gameState.PlayerModel = playerModel;
        gameState.PlayerSaveData = playerSaveData;
        ProjectContext.Instance.Container.Inject(player);
        OnSave(); // is here for test purposes only TODO: remove
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
    /// There's also nothing wrong (i think) with creating and destroying scene at the same time
    /// </summary>
    private IEnumerator UnloadScene()
    {
        var unload = SceneManager.UnloadSceneAsync(lastSceneIndex);
        while (!unload.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// Classes that require events from model data subscribe to it by itself
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
        OnLoad(nextSceneIndex);
    }

    public bool IsContinueAvailable()
    {
        GameState testState = new();
        if (!saveLoader.TryLoadGame(serializer, ref testState)) return false;
        return testState.NextSceneIndex != 0;
    }
}