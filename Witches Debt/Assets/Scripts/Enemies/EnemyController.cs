using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyModelMB modelMB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform target;
    private EnemyModel model;

    private void Start()
    {
        model = modelMB.EnemyModel;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + model.CurrentMovingSpeed * Time.fixedDeltaTime * (target.position - transform.position).normalized);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<PlayerHittable>(out var playerHittable))
        {
            //playerHittable.TakeDamage(model.ContactDamage);
            Debug.Log($"Player took {model.ContactDamage} damage");
        }
    }
}