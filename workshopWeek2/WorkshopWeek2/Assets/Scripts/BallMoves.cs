using UnityEngine;
using UnityEngine.InputSystem;

public class BallMoves : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float moveSpeed;
    private Vector2 movementInput;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movement =  moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y)* movement);
        
        // float rotation = movementInput.x * zotateSpeed * Time.deltaTime;
        // transform.Rotate(Vector3.up * rotation);
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}
