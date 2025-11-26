using UnityEngine;
using Zenject.SpaceFighter;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyModelMB modelMB;
    [SerializeField] private Rigidbody2D rb;
    private EnemyModel model;

    private void Start()
    {
        model = modelMB.EnemyModel;
    }

    private void FixedUpdate()
    {
        var posDiff = model.Target.Position - transform.position;
        Flip(posDiff);
        rb.MovePosition(transform.position + model.CurrentMovingSpeed * Time.fixedDeltaTime * posDiff.normalized);
    }

    //TODO: move to EnemyView
    private void Flip(Vector3 posDiff)
    {
        if (posDiff.x > 0 != transform.localScale.x > 0)
        {
            transform.localScale = new(transform.localScale.x * (-1),
                                                     transform.localScale.y,
                                                     transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<PlayerHittable>(out var playerHittable))
        {
            //playerHittable.TakeDamage(model.ContactDamage);
            Debug.Log($"Player took {model.ContactDamage} damage");
            model.TakeDamage(1000);
        }
    }

}