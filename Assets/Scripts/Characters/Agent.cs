using UnityEngine;

public abstract class Agent : MonoBehaviour
{
    public AgentState State {  get; private set; }

    public void SetState(AgentState newState)
    {
        State = newState;
        State.Enter();
    }

    public abstract void Ability1();

    public abstract void Ability2();

    public abstract void Ability3();
}

public abstract class AgentState
{
    protected Agent _agent;

    public AgentState(Agent agent)
    {
        _agent = agent;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}