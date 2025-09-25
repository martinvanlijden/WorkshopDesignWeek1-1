using UnityEngine;

public abstract class SwarmState
{
    protected SwarmStatemachine swarm;

    public SwarmState(SwarmStatemachine swarm)
    {
        this.swarm = swarm;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    
}