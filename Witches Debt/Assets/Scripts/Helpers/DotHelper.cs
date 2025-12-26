using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotHelper : MonoBehaviour
{
    private List<EnemyHittable> hittables = new();
    private readonly WaitForSeconds dotTimeout = new WaitForSeconds(0.25f);
    private Coroutine dotRoutine;

    private void Awake()
    {
        dotRoutine = StartCoroutine(DotLoop());
    }

    private IEnumerator DotLoop()
    {
        while (true)
        {
            yield return dotTimeout;
            foreach (var hittable in hittables) hittable.DotTick();
        }
    }
    
    public void RegisterForDot(EnemyHittable hittable)
    {
        hittables.Add(hittable);
    }

    public void UnregisterForDot(EnemyHittable hittable)
    {
        hittables.Remove(hittable);
    }

    private void OnDestroy()
    {
        StopCoroutine(dotRoutine);
    }
}