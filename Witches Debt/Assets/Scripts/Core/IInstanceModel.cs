using UnityEngine;

/// <summary>
/// Interface for model classes that can create instances (MonoBehaviours)
/// </summary>
public interface IInstanceModel
{
    public GameObject CreateInstance();
}