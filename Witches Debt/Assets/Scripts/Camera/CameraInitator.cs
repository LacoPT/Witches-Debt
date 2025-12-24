using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraInitator : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private PlayerTargetProvider targetProvider;
    [Inject]
    public void Construct(PlayerTargetProvider targetProvider)
    {
        this.targetProvider = targetProvider;
    }

    private void Start()
    {
        cinemachineCamera.Follow = targetProvider.Target;
    }

}
