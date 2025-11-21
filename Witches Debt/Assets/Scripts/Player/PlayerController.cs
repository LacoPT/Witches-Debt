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
    public void Construct(PlayerTargetProvider targetProvider, ModLibrary modLibrary)
    {
        this.targetProvider = targetProvider;
        targetProvider.SetTarget(transform);
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(modLibrary.GetRandomMod());
        }
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
