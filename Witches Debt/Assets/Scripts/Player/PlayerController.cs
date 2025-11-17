using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //TEMP SOLUTION
    private const float MoveSpeed = 15f;
    
    [SerializeField] Rigidbody2D rb;
    
    private Vector2 moveInput;
    private EnemyRegistry enemyRegistry;
    private PlayerTargetProvider targetProvider;


    [Inject]
    public void Construct(EnemyRegistry enemyRegistry, PlayerTargetProvider targetProvider)
    {
        this.enemyRegistry = enemyRegistry;
        this.targetProvider = targetProvider;
        targetProvider.SetTarget(transform);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.fixedDeltaTime * MoveSpeed * moveInput.normalized);
    }
}
