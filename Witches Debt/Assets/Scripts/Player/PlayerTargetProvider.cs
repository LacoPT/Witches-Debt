using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerTargetProvider
{
    private Transform target;
    public Transform Target => target; // transform is needed for following camera 
    public Vector3 Position => (target == null) ? default : target.position;
    public event Action TargetSet;
    public void SetTarget(Transform target)
    {
        this.target = target;
        TargetSet?.Invoke();
    }
}