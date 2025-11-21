using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// GameState is a model class that have references to all others models that are being saved/loaded
/// Can restore state of the game
/// </summary>
[XmlRootAttribute("GameState", IsNullable = false)]
public class GameState
{
    //[XmlElement("Player", IsNullable = false)]
    //public PlayerModel PlayerModel { get; private set; }
    //[XmlArray("Dots")]
    //[XmlArrayItem("Dot", typeof(DotModel))]
    //public List<DotModel> DotModels { get; private set; } = new();

    [XmlArray("TestItems")]
    [XmlArrayItem("TestItem", typeof(TestItemModel))]
    public List<TestItemModel> TestItemModels { get; set; } = new();
    /// <summary>
    /// ParameterLess constructor is required by XML.Serialization
    /// </summary>
    public GameState()
    {
    }

    //public void Create(PlayerModel playerModel)
    //{
    //    PlayerModel = playerModel;
    //}
    public void Create(TestItemModel testItemModel)
    {
        TestItemModels.Add(testItemModel);
    }

    /// <summary>
    /// Creates all of the instances, AFTER SERIALIZATION
    /// I couldn't come up with universal IInstanceModel ListCreation
    /// </summary>
    public void Initialize()
    {
        //if (PlayerModel == null)
        //    throw new Exception("PlayerModel is null / wrong Initialize usage");
        //PlayerModel.CreateInstance();
        foreach (TestItemModel tiModel in TestItemModels)
        {
            tiModel.CreateInstance();
        }
    }

    public void BindItemDespawned(UnityEvent<TestItem> _event)
    {
        _event.AddListener(OnItemDespawned);
    }

    private void OnItemDespawned(TestItem item)
    {
        TestItemModels.Remove(item.Model);
    }
}