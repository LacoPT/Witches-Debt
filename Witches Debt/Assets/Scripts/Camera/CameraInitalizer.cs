using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class CameraInitalizer : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    private PlayerTargetProvider targetProvider;
    [Inject]
    public void Construct(PlayerTargetProvider targetProvider)
    {
        this.targetProvider = targetProvider;
        if (targetProvider.Target != null) Initialize();
        targetProvider.TargetSet += Initialize;
    }

    private void Initialize()
    {
        cinemachineCamera.Follow = targetProvider.Target;
    }

    private void OnDisable()
    {
        targetProvider.TargetSet -= Initialize;
    }
}
