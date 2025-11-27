using System.Collections;
using UnityEngine;

public class EnemyStatsScaler : MonoBehaviour 
{
    [SerializeField] private float scaleCooldown = 5f;
    [SerializeField] private float statsModifierScaler = 0.1f; // TODO: make differnet scaler for each stat
    public float HealthModifier { get; private set; } = 1f;
    public float MovingSpeedModifier { get; private set; } = 1f;
    public float DamageModifier { get; private set; } = 1f;
    public float AttackSpeedModifier { get; private set; } = 1f;

    private void Awake()
    {
        StartCoroutine(WaitForScaleCooldown());
    }

    private IEnumerator WaitForScaleCooldown()
    {
        yield return new WaitForSeconds(scaleCooldown);
        IncreaseModifiers();
        StartCoroutine(WaitForScaleCooldown());
    }
    public void IncreaseModifiers()
    {
        HealthModifier += statsModifierScaler;
        MovingSpeedModifier += statsModifierScaler;
        DamageModifier += statsModifierScaler;
        AttackSpeedModifier += statsModifierScaler;
    }
}