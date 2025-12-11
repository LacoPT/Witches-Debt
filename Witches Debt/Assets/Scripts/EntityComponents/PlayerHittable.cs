using UnityEngine;
using UnityEngine.Events;

public class PlayerHittable : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    public UnityEvent Death;
    public UnityEvent<float> HealthChanged;
    public void TakeDamage(float damage)
    {
        hp -= damage;
        CheckHealth();
        HealthChanged.Invoke(hp);
    }

    private void CheckHealth()
    {
        if (hp > 0) return;
        Death.Invoke();
        Destroy(gameObject);
    }
}
