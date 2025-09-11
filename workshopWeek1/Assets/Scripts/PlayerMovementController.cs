using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController3DFixed : MonoBehaviour, PlayerMovement.IMoveActions
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private PlayerMovement inputActions;

    private Vector2 moveInput;
    private Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Instantiate the generated input wrapper
        inputActions = new PlayerMovement();
        inputActions.Move.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Move.Enable();
    }

    private void OnDisable()
    {
        inputActions.Move.Disable();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Convert 2D input to world-space movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    #region IMoveActions Implementation
    public void OnMoving(InputAction.CallbackContext context)
    {
        // Read 2D vector from composite input (WASD)
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLooking(InputAction.CallbackContext context)
    {
        // Do nothing—camera stays fixed
    }
    #endregion
}
