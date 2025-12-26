using System.Collections.Generic;
using UnityEngine;

public class NextSceneIndexSelector : MonoBehaviour
{
    [SerializeField] private List<int> nextSceneIndices;
    public int NextSceneIndex { get; private set; }
    private void Awake()
    {
        NextSceneIndex = nextSceneIndices[Random.Range(0, nextSceneIndices.Count)];
    }
}
