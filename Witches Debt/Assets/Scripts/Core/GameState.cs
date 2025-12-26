using System.Xml.Serialization;
using UnityEngine;
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

    public void OnLoadFromSave()
    {
        ProjectContext.Instance.Container.Resolve<PlayerStats>().FromSaveData(PlayerSaveData);
        ProjectContext.Instance.Container.Resolve<InventoryModel>().FromSaveData(InventorySaveData);
    }

    public void OnSave(int nextSceneIndex)
    {
        if (nextSceneIndex != -1) NextSceneIndex = nextSceneIndex;
        PlayerSaveData = ProjectContext.Instance.Container.Resolve<PlayerStats>().ToSaveData();
        InventorySaveData = ProjectContext.Instance.Container.Resolve<InventoryModel>().ToSaveData();
    }

}