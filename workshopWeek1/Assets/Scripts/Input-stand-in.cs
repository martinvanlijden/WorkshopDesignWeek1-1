using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] private int coins = 0;

    Vector2 movementInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    void Update()
    {
        Vector3 movedirection = new Vector3(movementInput.x,0, movementInput.y);

        transform.position += movedirection * speed * Time.deltaTime;


    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void collectcoin()
    {
        coins++;
    }
}