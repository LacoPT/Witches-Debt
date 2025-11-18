using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerTargetProvider
{
    private Transform target;
    
    public Vector3 Position => target.position;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}