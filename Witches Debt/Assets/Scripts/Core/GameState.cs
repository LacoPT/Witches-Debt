using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

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
    /// <summary>
    /// ParameterLess constructor is required by XML.Serialization
    /// </summary>
    public GameState()
    {
    }

    /// <summary>
    /// Creates all of the instances, AFTER SERIALIZATION
    /// I couldn't come up with universal IInstanceModel ListCreation
    /// </summary>
    public void Initialize()
    {
        var playerStats = new PlayerStats();
        playerStats.FromSaveData(PlayerSaveData);
        var player = playerStats.CreateInstance().GetComponent<PlayerController>();
        ProjectContext.Instance.Container.Inject(player);
    }
}