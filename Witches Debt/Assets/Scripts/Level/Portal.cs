using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EntryPoint.Instance.OnLoad(nextSceneIndex);
        }
    }
}
