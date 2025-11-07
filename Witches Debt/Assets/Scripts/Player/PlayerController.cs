using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //TEMP SOLUTION
    private const float MoveSpeed = 15f;
    
    [SerializeField] Rigidbody2D rb;
    
    private Vector2 MoveInput;


    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }
    
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Time.fixedDeltaTime * MoveSpeed * MoveInput.normalized);
    }
}
