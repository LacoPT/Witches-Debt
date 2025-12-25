using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IActor
{
    [SerializeField] Rigidbody2D rb;
    //TEMP SOLUTION
    private const float MoveSpeed = 15f;    
    private Vector2 moveInput;
    private EnemyRegistry enemyRegistry;
    private PlayerTargetProvider targetProvider;
    private PlayerModel model;

    [Inject]
    public void Construct(PlayerTargetProvider targetProvider, PlayerControls playerControls)
    {
        this.targetProvider = targetProvider;
        targetProvider.SetTarget(transform);
        var input = GetComponent<PlayerInput>();
        playerControls.SetPlayerInput(input);
    }

    public void Initialize(IInstanceModel model)
    {
        this.model = (PlayerModel)model;
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
