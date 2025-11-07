using System;
using UnityEngine;

public class ShotSpell : MonoBehaviour
{
    [SerializeField] private float speed = 15f;

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.up;
    }
}
