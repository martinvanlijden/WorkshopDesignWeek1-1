using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputActionAsset inputActions;
    public float moveSpeed = 5f;
    public LayerMask raycastMask = ~0;
    public GameObject crossPrefab;
    public float destroyDistance = 0.3f;

    InputAction movementAction;
    Vector3 targetPosition;
    bool hasTarget;
    GameObject activeCross;

    void Awake()
    {
        var gameplay = inputActions.FindActionMap("Gameplay");
        movementAction = gameplay.FindAction("Movement");
    }

    void OnEnable()
    {
        movementAction.performed += OnMovementClick;
        movementAction.Enable();
    }

    void OnDisable()
    {
        movementAction.performed -= OnMovementClick;
        movementAction.Disable();
    }

    void Update()
    {
        if (!hasTarget) return;

        Vector3 dir = targetPosition - transform.position;
        dir.y = 0;
        if (dir.magnitude > 0.1f)
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        else
            hasTarget = false;

        if (activeCross && Vector3.Distance(transform.position, activeCross.transform.position) <= destroyDistance)
        {
            Destroy(activeCross);
            activeCross = null;
        }
    }

    void OnMovementClick(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, 500f, raycastMask))
        {
            if (hit.collider.CompareTag("Pickup"))
            {
                Vector3 pickupCenter = hit.collider.bounds.center;
                Vector3 offset = pickupCenter - transform.position;
                offset.y = 0;

                if (offset.magnitude > 1f)
                    targetPosition = pickupCenter - offset.normalized * 3f;
                else
                    targetPosition = transform.position;

                hasTarget = true;
                SpawnColoredCross(targetPosition, Color.cyan);
                return;
            }

            if (hit.collider.CompareTag("Walkable"))
            {
                targetPosition = hit.point;
                hasTarget = true;
                SpawnColoredCross(hit.point, Color.yellow);
            }
            else
            {
                SpawnColoredCross(hit.point, Color.blue);
            }
        }
    }

    void SpawnColoredCross(Vector3 position, Color color)
    {
        if (!crossPrefab) return;
        if (activeCross) Destroy(activeCross);
        activeCross = Instantiate(crossPrefab, position + Vector3.up * 0.01f, Quaternion.identity);
        foreach (Renderer r in activeCross.GetComponentsInChildren<Renderer>())
        {
            Material mat = new Material(r.sharedMaterial);
            mat.color = color;
            r.material = mat;
        }
    }
}
