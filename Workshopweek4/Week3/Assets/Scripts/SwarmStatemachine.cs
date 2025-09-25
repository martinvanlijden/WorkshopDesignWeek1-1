using DefaultNamespace;
using UnityEngine;

public class SwarmStatemachine : MonoBehaviour
{
    public float moveSpeed = 3f;
    [SerializeField] private float bitingRange = 5f;
    public float attackCooldown = 1f;

    private SwarmState currentState;
    public SwarmState attacking;
    public SwarmState chasing;

    void Start()
    {
        attacking = new SwarmAttackingState(this);
        chasing = new SwarmingState(this);
        
        currentState = chasing;
        currentState.Enter();
    }

    void Update()
    {
        currentState.Update();
    }

    public void SwitchState(SwarmState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public bool IsNear()
    {
        float distance = Vector3.Distance(Swarmbait.LeaderPosition, transform.position);
        return distance <= bitingRange;
    }
}