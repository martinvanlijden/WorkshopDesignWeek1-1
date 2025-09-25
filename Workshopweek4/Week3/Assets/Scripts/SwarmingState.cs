using UnityEngine;

namespace DefaultNamespace
{
    public class SwarmingState: SwarmState
    {
        public SwarmingState(SwarmStatemachine swarm) : base(swarm) {}

        public override void Enter()
        {
            Debug.Log($"{swarm.name} entered CHASING state");
        }

        public override void Update()
        {
            // Move toward leader
            Vector3 target = Swarmbait.LeaderPosition;
            swarm.transform.position = Vector3.MoveTowards(
                swarm.transform.position,
                target,
                swarm.moveSpeed * Time.deltaTime
            );

            // Check if close enough to switch
            if (swarm.IsNear())
            {
                swarm.SwitchState(swarm.attacking);
            }
        }

        public override void Exit()
        {
            Debug.Log($"{swarm.name} exiting CHASING state");
        }
    }
    
}