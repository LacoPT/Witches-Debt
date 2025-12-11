using TMPro;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private PlayerHittable hittable;
    [SerializeField] private TMP_Text text;
    private void Awake()
    {
        hittable.HealthChanged.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged(float health)
    {
        text.text = health.ToString();
    }
}
