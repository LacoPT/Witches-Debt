using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyModelMB model;
    [SerializeField] private Rigidbody2D rb;
    private bool isContactDamageReady = true;
    private const float contactDamageCooldown = 1f;
    private void FixedUpdate()
    {
        var posDiff = model.Target.Position - transform.position;
        Flip(posDiff);
        rb.MovePosition(transform.position + model.MovingSpeed * Time.fixedDeltaTime * posDiff.normalized);
    }

    //TODO: move to EnemyView
    private void Flip(Vector3 posDiff)
    {
        if ((model.Target.Position - transform.position).x > 0 != transform.localScale.x > 0)
        {
            transform.localScale = new(transform.localScale.x * (-1),
                                                     transform.localScale.y,
                                                     transform.localScale.z);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!isContactDamageReady) return;
        if (other.gameObject.TryGetComponent<PlayerHittable>(out var playerHittable))
        {
            //playerHittable.TakeDamage(model.ContactDamage);
            Debug.Log($"Player took {model.ContactDamage} damage");
            StartCoroutine(WaitForContactDamageCooldown());
        }
    }

    private IEnumerator WaitForContactDamageCooldown()
    {
        isContactDamageReady = false;
        yield return new WaitForSeconds(contactDamageCooldown);
        isContactDamageReady = true;
    }

}