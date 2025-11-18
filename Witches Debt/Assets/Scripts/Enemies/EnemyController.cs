using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyModelMB modelMB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform target;
    public PlayerTargetProvider targetProvider { get; set; }
    private EnemyModel model;

    private void Start()
    {
        model = modelMB.EnemyModel;
        model.EnemyDeath += OnDeath;
    }

    private void OnDisable()
    {
        model.EnemyDeath -= OnDeath;
    }

    private void FixedUpdate()
    {
        //var posDiff = target.position - transform.position;
        var posDiff = targetProvider.Position - transform.position;
        Flip(posDiff);
        rb.MovePosition(transform.position + model.CurrentMovingSpeed * Time.fixedDeltaTime * posDiff.normalized);
    }

    //TODO: move to EnemyView
    private void Flip(Vector3 posDiff )
    {
        if (posDiff.x > 0 != transform.localScale.x > 0)
        {
            transform.localScale = new(transform.localScale.x * (-1),
                                                     transform.localScale.y,
                                                     transform.localScale.z);
            Debug.Log("Flip");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<PlayerHittable>(out var playerHittable))
        {
            //playerHittable.TakeDamage(model.ContactDamage);
            Debug.Log($"Player took {model.ContactDamage} damage");
        }
    }

    //Temporary solution for testing purposes
    //TODO: make a better one
    private void OnDeath()
    {
        Destroy(gameObject);
    }
}