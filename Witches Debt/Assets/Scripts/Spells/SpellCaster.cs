using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private float TestCastTime = 1.5f;
    [SerializeField] private SpellType TestSpellType;
    private Func<Vector2> targetChooseFunction = RandomAngle;
    private bool onCooldown = false;
    
    private void Update()
    {
        if (!onCooldown)
        {
            Instantiate(TestSpellType.SpellPrefab, transform.position,
                Quaternion.LookRotation(Vector3.forward, targetChooseFunction()));
            onCooldown = true;
            StartCoroutine(WaitForCooldown());
        }
    }

    private IEnumerator WaitForCooldown()
    {
        yield return new WaitForSeconds(TestCastTime);
        onCooldown = false;
    }
    
    private static Vector2 RandomAngle()
    {
        var angle = Random.Range(0f, Mathf.PI * 2f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    } 
}
