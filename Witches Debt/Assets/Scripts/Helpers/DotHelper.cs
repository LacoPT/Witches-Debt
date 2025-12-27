using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotHelper : MonoBehaviour
{
    private List<EnemyHittable> hittables = new();
    private List<EnemyHittable> hittablesToUnregister = new();
    private readonly WaitForSeconds dotTimeout = new WaitForSeconds(0.5f);
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
            foreach (var hittable in hittablesToUnregister)
                hittables.Remove(hittable);
            hittablesToUnregister.Clear();
            foreach (var hittable in hittables) hittable.DotTick();
        }
    }
    
    public void RegisterForDot(EnemyHittable hittable)
    {
        hittables.Add(hittable);
        //Debug.Log($"{hittable} registered for dot");
    }

    public void UnregisterForDot(EnemyHittable hittable)
    {
        if(hittablesToUnregister.Contains(hittable)) return;
        hittablesToUnregister.Add(hittable);
    }

    private void OnDestroy()
    {
        StopCoroutine(dotRoutine);
    }
}