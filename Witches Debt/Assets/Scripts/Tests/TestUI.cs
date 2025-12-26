using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> children = new();
    public void OnHide()
    {
        foreach(var child in children)
        {
            child.SetActive(!child.activeSelf);
        }
    }
}
