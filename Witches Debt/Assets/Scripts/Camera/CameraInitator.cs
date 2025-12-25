using System.Collections;
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

    private void Awake()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitWhile(() => targetProvider.Target == null);
        cinemachineCamera.Follow = targetProvider.Target;

    }
}
