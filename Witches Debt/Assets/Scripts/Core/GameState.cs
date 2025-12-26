using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Zenject.Asteroids;

/// <summary>
/// GameState is a model class that have references to all others models that are being saved/loaded
/// Can restore state of the game
/// </summary>
[XmlRootAttribute("GameState", IsNullable = false)]
public class GameState
{
    [XmlElement("NextSceneIndex")] public int NextSceneIndex { get; set; }
    [XmlElement("PlayerSaveData", IsNullable = false)]
    public PlayerSaveData PlayerSaveData { get; set; }
    [XmlElement("InventorySaveData", IsNullable = false)]
    public InventorySaveData InventorySaveData { get; set; }

    [XmlIgnore] public PlayerStats PlayerStats { get; set; }
    [XmlIgnore] public InventoryModel InventoryModel { get; set; }
    /// <summary>
    /// ParameterLess constructor is required by XML.Serialization
    /// </summary>
    public GameState()
    {
    }

    public void OnDefaultLoad()
    {
        InventoryModel = new InventoryModel();
        PlayerStats = new PlayerStats();
        var player = PlayerStats.CreateInstance().GetComponent<PlayerController>();
        ProjectContext.Instance.Container.Inject(player);
    }

    public void OnSave(int nextSceneIndex)
    {
        if (NextSceneIndex != -1) NextSceneIndex = nextSceneIndex;
        PlayerSaveData = PlayerStats.ToSaveData();
        InventorySaveData = InventoryModel.ToSaveData();
    }

    /// <summary>
    /// Creates all of the instances, AFTER SERIALIZATION
    /// I couldn't come up with universal IInstanceModel ListCreation
    /// </summary>
    public void Initialize()
    {
        PlayerStats = new PlayerStats();
        PlayerStats.FromSaveData(PlayerSaveData);
        var player = PlayerStats.CreateInstance().GetComponent<PlayerController>();
        ProjectContext.Instance.Container.Inject(player);

        InventoryModel = new InventoryModel();
        InventoryModel.FromSaveData(InventorySaveData);
        foreach(var mod in InventorySaveData.SpellsStorages[0].mods)
        {
            Debug.Log(mod);
        };
    }
}