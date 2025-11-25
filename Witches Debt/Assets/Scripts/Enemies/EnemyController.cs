using UnityEngine;
using Zenject.SpaceFighter;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyModelMB modelMB;
    [SerializeField] private Rigidbody2D rb;
    private PlayerTargetProvider target;
    private EnemyModel model;

    private void Start()
    {
        model = modelMB.EnemyModel;
        model.EnemyDeath += OnDeath;
    }

    //private void OnDisable()
    //{
    //    model.EnemyDeath -= OnDeath;
    //}

    public void SetTarget(PlayerTargetProvider target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        var posDiff = target.Position - transform.position;
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
            Debug.Log("Flip");
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

    //Temporary solution for testing purposes
    //TODO: make a better one
    private void OnDeath()
    {
        //Destroy(gameObject);
    }
}