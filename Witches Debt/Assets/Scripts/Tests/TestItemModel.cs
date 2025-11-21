using System;
using System.Xml.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;
using Vector3 = UnityEngine.Vector3;

public class TestItemModel : IInstanceModel
{
    [XmlIgnore]
    private const string PREFAB_PATH = "TestItem";
    [XmlIgnore]
    private static readonly TestItem testItemPrefab = Resources.Load<TestItem>(PREFAB_PATH);

    [XmlElement("Position")]
    public Vector3 Position;

    public TestItemModel()
    {
    }

    public GameObject CreateInstance()
    {
        var instance = Object.Instantiate(testItemPrefab);
        instance.Initialize(this);
        return instance.gameObject;
    }
}