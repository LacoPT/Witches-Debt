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
    [XmlElement("PlayerSaveData", IsNullable = false)]
    public PlayerSaveData PlayerSaveData { get; set; }
    [XmlElement] public PlayerStats PlayerStats { get; set; }
    [XmlElement("NextSceneIndex")] public int NextSceneIndex { get; set; }

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
        var player = PlayerStats.CreateInstance().GetComponent<PlayerController>();
        PlayerStats.FromSaveData(PlayerSaveData);
        ProjectContext.Instance.Container.Inject(player);
    }
}