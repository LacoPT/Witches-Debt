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
    private PlayerStats stats;

    [Inject]
    public void Construct(DiContainer container, PlayerTargetProvider targetProvider, PlayerControls playerControls, PlayerStats stats)
    {
        targetProvider.SetTarget(transform);
        var input = GetComponent<PlayerInput>();
        playerControls.SetPlayerInput(input);
        Initialize(stats);
    }

    public void Initialize(IInstanceModel model)
    {
        stats = (PlayerStats)model;
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
