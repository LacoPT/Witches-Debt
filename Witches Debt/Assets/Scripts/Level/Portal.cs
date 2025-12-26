using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D portalCollider;
    [SerializeField] private NextSceneIndexSelector sceneIndexSelector;
    private void Awake()
    {
        if (spawnPoints.Count == 0) return;
        gameObject.transform.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
        StartCoroutine(WaitForSpawn());
    }

    private IEnumerator WaitForSpawn()
    {
        spriteRenderer.enabled = false;
        portalCollider.enabled = false;
        yield return new WaitForSeconds(timeToSpawn);
        spriteRenderer.enabled = true;
        portalCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EntryPoint.Instance.Load(sceneIndexSelector.NextSceneIndex);
        }
    }
}