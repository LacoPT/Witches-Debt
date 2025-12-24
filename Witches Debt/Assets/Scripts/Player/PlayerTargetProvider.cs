using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerTargetProvider
{
    private Transform target;
    public Transform Target => target; // transform is needed for following camera 
    public Vector3 Position => target.position;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}