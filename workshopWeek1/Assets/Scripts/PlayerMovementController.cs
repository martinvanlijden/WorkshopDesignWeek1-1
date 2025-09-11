using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController3DStepBobbing : MonoBehaviour, PlayerMovement.IMoveActions
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 0.15f;
    [SerializeField] private float stepDistance = 1f;

    private CharacterController controller;
    private PlayerMovement inputActions;

    private Vector2 moveInput;
    private Vector3 velocity;

    private Vector3 lastPosition;
    private float distanceTraveled = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        lastPosition = transform.position;
        inputActions = new PlayerMovement();
        inputActions.Move.SetCallbacks(this);
    }

    private void OnEnable() => inputActions.Move.Enable();
    private void OnDisable() => inputActions.Move.Disable();

    private void Update()
    {
        HandleMovement();
        HandleStepBobbing();
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleStepBobbing()
    {
        Vector3 horizontalMove = transform.position - lastPosition;
        horizontalMove.y = 0f;
        float distance = horizontalMove.magnitude;

        if (distance > 0.001f && controller.isGrounded)
        {
            distanceTraveled += distance;

            if (distanceTraveled >= stepDistance)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                distanceTraveled = 0f;
            }
        }

        lastPosition = transform.position;
    }

    public void OnMoving(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLooking(InputAction.CallbackContext context)
    {
    }
}
