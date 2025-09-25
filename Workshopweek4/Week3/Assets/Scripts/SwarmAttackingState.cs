using UnityEngine;

namespace DefaultNamespace
{
    public class SwarmAttackingState: SwarmState
    {
        private float lastAttackTime;

        public SwarmAttackingState(SwarmStatemachine swarm) : base(swarm) {}

        public override void Enter()
        {
            Debug.Log($"{swarm.name} entered ATTACKING state");
            lastAttackTime = -Mathf.Infinity;
        }

        public override void Update()
        {
            if (!swarm.IsNear())
            {
                swarm.SwitchState(swarm.chasing);
                return;
            }

            if (Time.time >= lastAttackTime + swarm.attackCooldown)
            {
                Swarmbait.decreaseHealth();
                Debug.Log($"{swarm.name} attacks at {Time.time:F2}");
                lastAttackTime = Time.time;
            }
        }

        public override void Exit()
        {
            Debug.Log($"{swarm.name} exiting ATTACKING state");
        }
    }
}