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
    private PlayerControls playerControls;
    private PlayerStats model;


    public void Awake()
    {
        targetProvider = ProjectContext.Instance.Container.Resolve<PlayerTargetProvider>();
        targetProvider.SetTarget(transform);
        var input = GetComponent<PlayerInput>();
        playerControls = ProjectContext.Instance.Container.Resolve<PlayerControls>();
        playerControls.SetPlayerInput(input);
    }
    //[Inject]
    //public void Construct(DiContainer container, PlayerTargetProvider targetProvider, PlayerControls playerControls)
    //{
    //    this.targetProvider = targetProvider;
    //    targetProvider.SetTarget(transform);
    //    var input = GetComponent<PlayerInput>();
    //    playerControls.SetPlayerInput(input);
    //    this.container = container;
    //}

    public void Initialize(IInstanceModel model)
    {
        this.model = (PlayerStats)model;
        var loader = GetComponent<SpellLoader>();
        loader.ClearAllCasters();
        //loader.TestLoadDefault();
        loader.LoadFromInventoryModel(InventoryModel.GetInstance());
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
