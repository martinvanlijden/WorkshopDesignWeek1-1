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

        if (activeCross != null && Vector3.Distance(transform.position, activeCross.transform.position) <= destroyDistance)
        {
            Destroy(activeCross);
            activeCross = null;
        }
    }

    void OnMovementClick(InputAction.CallbackContext ctx)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            Ray down = new Ray(new Vector3(hitPoint.x, 100f, hitPoint.z), Vector3.down);
            if (Physics.Raycast(down, out RaycastHit hitInfo, 200f, raycastMask))
            {
                Debug.Log("Down ray hit: " + hitInfo.collider.name + " Tag: " + hitInfo.collider.tag);

                if (hitInfo.collider.CompareTag("Walkable"))
                {
                    targetPosition = hitInfo.point;
                    hasTarget = true;
                    SpawnColoredCross(hitInfo.point, Color.yellow);
                }
                else
                {
                    SpawnColoredCross(hitInfo.point, Color.blue);
                }
            }
            else
            {
                SpawnColoredCross(hitPoint, Color.blue);
            }
        }
    }

    void SpawnColoredCross(Vector3 position, Color color)
    {
        if (!crossPrefab) return;
        if (activeCross != null) Destroy(activeCross);
        activeCross = Instantiate(crossPrefab, position + Vector3.up * 0.01f, Quaternion.identity);
        foreach (Renderer r in activeCross.GetComponentsInChildren<Renderer>())
        {
            Material mat = new Material(r.sharedMaterial);
            mat.color = color;
            r.material = mat;
        }
    }
}
